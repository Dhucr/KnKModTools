using Esprima.Ast;
using Esprima.Utils;

namespace KnKModTools.DatClass.Complie
{
    public class ComplieCore : AstVisitor
    {
        private readonly TempDatStruct _tempDat = new();
        private readonly Dictionary<string, int> _globalVarMap = [];
        private readonly CommentParser _parser = new();

        private IReadOnlyList<SyntaxComment>? _comments;

        private CompileContext _context;

        // 指令生成入口
        public TempDatStruct Generate(Script ast)
        {
            _comments = ast.Comments;
            Visit(ast);
            return _tempDat;
        }

        private void UpdateStackState(byte code, int value = 0)
        {
            _context.StackDepth += code switch
            {
                0 or 2 or 3 or 4 or 7 or 9 => 1,
                5 or 6 or 8 or 10 or (>= 14 and <= 30) => -1,
                37 => 5,
                1 or 12 or 34 or 35 or 39 => value,
                _ => 0
            };
        }

        private void ConvertOperandsType(InStruction ins)
        {
            if (ins.Operands == null || ins.Operands.Length == 0)
                return;

            var operands = ins.Operands;
            switch (ins.Code)
            {
                case 0 or 1 or 9 or 10 or 39:
                    operands[0] = Convert.ToByte(operands[0]);
                    break;

                case 2 or 3 or 4 or 5 or 6 or 7 or 8:
                    operands[0] = Convert.ToInt32(operands[0]);
                    break;

                case 11 or 14 or 15 or 37 or 40:
                    if (operands[0] is Label)
                        return;
                    operands[0] = Convert.ToUInt32(operands[0]);
                    break;

                case 34 or 35:
                    operands[2] = Convert.ToByte(operands[2]);
                    break;

                case 36:
                    operands[0] = Convert.ToByte(operands[0]);
                    operands[1] = Convert.ToByte(operands[1]);
                    operands[2] = Convert.ToByte(operands[2]);
                    break;
            }
        }

        private void HandlePop(int count)
        {
            if (count == 0)
                return;
            var abs = Math.Abs(count);
            Emit(1, [abs * 4], count);
        }

        private void Emit(InStruction ins, int updateValue = 0)
        {
            UpdateStackState(ins.Code, updateValue);
            ConvertOperandsType(ins);

            _context.InsList.Add(ins);
        }

        private void Emit(byte code, params object[] operands)
        {
            Emit(new InStruction { Code = code, Operands = operands });
        }

        private void Emit(byte code, object[] operands, int updateValue = 0)
        {
            var ins = new InStruction { Code = code, Operands = operands };
            Emit(ins, updateValue);
        }

        private bool ReplaceMarkerToLoadResult()
        {
            var last = _context.InsList.LastOrDefault();
            if (last != null && last.Code == 38)
            {
                last.Code = 9;
                last.Operands = [(byte)0];
                UpdateStackState(9);
                return true;
            }
            return false;
        }

        private void DeFormatting(Node node)
        {
            if (node is not CallExpression callee)
                return;

            var isMember = callee.Callee is MemberExpression;

            if (!isMember)
            {
                if (callee.Callee is not Identifier ident)
                    return;

                if (ident.Name.StartsWith("GetAddress") ||
                ident.Name.StartsWith("IsTrue"))
                    return;
            }

            if (ReplaceMarkerToLoadResult())
                return;

            Emit(9, 0);
        }

        private void HandleGlobalInit(ObjectExpression objEx)
        {
            var index = 0;
            foreach (var property in objEx.Properties)
            {
                if (property is not Property prop)
                    continue;

                if (prop.Key is Identifier ident)
                {
                    _globalVarMap.Add(ident.Name, index);
                    index++;

                    if (prop.Value is not Literal literal)
                        continue;

                    _tempDat.GlobalVarMap.Add(ident.Name,
                            Convert.ToUInt32(literal.Value));
                }
            }
        }

        protected override object? VisitVariableDeclarator(VariableDeclarator declr)
        {
            // 生成初始
            var varName = ((Identifier)declr.Id).Name;
            if (varName.Equals("Global") && declr.Init is ObjectExpression objEx)
            {
                HandleGlobalInit(objEx);
                return null;
            }

            if (varName.Equals("Result"))
                return null;

            Emit(0, 4, (uint)0);
            _context.AddLocalVar(varName);

            if (declr.Init is Literal lit && lit.Value is null)
                return null;

            Visit(declr.Init);
            DeFormatting(declr.Init);
            HandleLocalVariableAssignment(varName);
            return null;
        }

        protected override object? VisitAssignmentExpression(AssignmentExpression expr)
        {
            // 处理右侧表达式
            Visit(expr.Right);
            DeFormatting(expr.Right);
            // 处理左侧标识符
            if (expr.Left is MemberExpression member)
            {
                HandleGlobalOrResultAssignment(member);
            }
            else if (expr.Left is Identifier ident)
            {
                HandleLocalVariableAssignment(ident.Name);
            }

            return null;
        }

        private void HandleGlobalOrResultAssignment(MemberExpression member)
        {
            if (member.Object is not Identifier objIdent)
                return;

            if (!objIdent.Name.Equals("Global") &&
                !objIdent.Name.Equals("Result"))
                return;

            // 处理Global.xxx
            if (member.Property is Identifier propIdent &&
                _globalVarMap.TryGetValue(propIdent.Name, out var index)
            )
            {
                Emit(8, index);
            }
            else if (member.Property is Literal propLit)
            {
                Emit(10, propLit.Value);
            }
        }

        private void HandleLocalVariableAssignment(string varName)
        {
            if (_context.LocalScopes.TryGetValue(varName, out var index))
            {
                // 计算栈偏移量：每个变量占4字节，从栈顶向下计算
                if (index < 2)
                    return;

                int byteOffset = -((index - 1) * 4);
                Emit(5, byteOffset);
            }
        }

        protected override object? VisitBinaryExpression(BinaryExpression binaryExpr)
        {
            // 处理左操作数
            Visit(binaryExpr.Left);
            DeFormatting(binaryExpr.Left);

            // 处理右操作数
            Visit(binaryExpr.Right);
            DeFormatting(binaryExpr.Right);

            // 添加运算指令
            Emit(GetBinaryOpCode(binaryExpr.Operator));

            return null;
        }

        private byte GetBinaryOpCode(BinaryOperator op)
        {
            return op switch
            {
                BinaryOperator.Plus => 16,
                BinaryOperator.Minus => 17,
                BinaryOperator.Times => 18,
                BinaryOperator.Divide => 19,
                BinaryOperator.Modulo => 20,
                BinaryOperator.Equal => 21,
                BinaryOperator.NotEqual => 22,
                BinaryOperator.Greater => 23,
                BinaryOperator.GreaterOrEqual => 24,
                BinaryOperator.Less => 25,
                BinaryOperator.LessOrEqual => 26,
                BinaryOperator.BitwiseAnd => 27,
                BinaryOperator.BitwiseOr => 28,
                BinaryOperator.LogicalAnd => 29,
                BinaryOperator.LogicalOr => 30,
                _ => throw new NotSupportedException($"Operator {op} not supported"),
            };
        }

        private bool IsNegativeNumbers(byte code, Expression expression)
        {
            if (code == 31 && expression is Literal lit)
            {
                var ins = HandleLiteral(lit);
                switch (ins.Operands[1])
                {
                    case int iVal:
                        ins.Operands[1] = -iVal;
                        break;

                    case float fVal:
                        ins.Operands[1] = -fVal;
                        break;
                }
                Emit(ins);
                return true;
            }

            return false;
        }

        protected override object? VisitUnaryExpression(UnaryExpression unaryExpr)
        {
            var code = GetUnaryOpCode(unaryExpr.Operator);
            if (IsNegativeNumbers(code, unaryExpr.Argument))
                return null;

            Visit(unaryExpr.Argument);
            DeFormatting(unaryExpr.Argument);

            Emit(code);

            return null;
        }

        private byte GetUnaryOpCode(UnaryOperator op)
        {
            return op switch
            {
                UnaryOperator.Minus => 31,
                UnaryOperator.BitwiseNot => 33,
                _ => throw new NotSupportedException($"Operator {op} not supported"),
            };
        }

        protected override object VisitCallExpression(CallExpression expr)
        {
            if (expr.Callee is Identifier ident)
            {
                HandleIdentifierCall(ident, expr.Arguments);
            }
            else if (expr.Callee is MemberExpression member)
            {
                HandleMemberCall(member, expr.Arguments);
            }

            return null;
        }

        private void HandleIdentifierCall(Identifier callee, IEnumerable<Expression> args)
        {
            if (callee.Name.StartsWith("IsTrue"))
            {
                ProcessIsTrueCall(args);
            }
            else if (callee.Name.StartsWith("GetAddress"))
            {
                ProcessGetAddressCall(args);
            }
            else
            {
                ProcessNormalFunctionCall(callee, args);
            }
        }

        private void ProcessIsTrueCall(IEnumerable<Expression> args)
        {
            if (args.Count() != 1)
                return;

            Visit(args.FirstOrDefault());
            DeFormatting(args.FirstOrDefault());
            // 生成ISTRUE指令
            Emit(32);
        }

        private void ProcessGetAddressCall(IEnumerable<Expression> args)
        {
            if (args.Count() != 1)
                return;

            if (args.FirstOrDefault() is not Identifier ident)
                return;

            if (_context.LocalScopes.TryGetValue(ident.Name, out var index))
            {
                // 计算栈偏移量：每个变量占4字节，从栈顶向下计算
                int byteOffset = -(index * 4);
                Emit(4, byteOffset);
            }
        }

        private void ProcessNormalFunctionCall(Identifier callee, IEnumerable<Expression> args)
        {
            // 压入函数ID和返回地址
            var funcName = callee.Name;
            var label = new Label();
            Emit(0, 4, (uint)_context.FunctionId);
            Emit(0, 4, label);

            // 处理参数
            foreach (var arg in args.Reverse())
            {
                Visit(arg);
                DeFormatting(arg);
            }

            // 生成CALL指令
            Emit(12, [funcName], -(args.Count() + 2));

            // 记录返回地址位置
            Emit(label.Ins);
            label.SetLine(_context, callee);
        }

        private void HandleMemberCall(MemberExpression member, IEnumerable<Expression> args)
        {
            if (member.Object is Identifier objIdent && objIdent.Name == "Engine")
            {
                ProcessEngineCall(args);
            }
            else
            {
                ProcessCrossScriptCall(member, args);
            }
        }

        private void ProcessEngineCall(IEnumerable<Expression> args)
        {
            if (args.First() is not ArrayExpression arr)
                return;
            if (arr.Elements.Count != 2)
                return;
            if (arr.Elements[0] is not Literal lit1 ||
                arr.Elements[1] is not Literal lit2)
                return;

            var cmd1 = lit1.Value;
            var cmd2 = lit2.Value;

            // 处理其他参数
            foreach (var arg in args.Skip(1).Reverse())
            {
                Visit(arg);
                DeFormatting(arg);
            }
            var count = args.Count() - 1;
            Emit(36, cmd1, cmd2, count);

            // 弹出参数
            HandlePop(-count);
        }

        private void ProcessAsyncCrossScriptCall(string name,
            Expression property, IEnumerable<Expression> args)
        {
            var scriptName = name.Replace("sc_", "");
            if (scriptName == "All")
                scriptName = "";
            var count = args.Count();

            foreach (var arg in args.Reverse())
            {
                Visit(arg);
                DeFormatting(arg);
            }

            if (property is Identifier method)
            {
                Emit(35, [scriptName, method.Name, count], -count);
            }
        }

        private void ProcessSyncCrossScriptCall(MemberExpression member,
            IEnumerable<Expression> args)
        {
            var scriptName = "";
            var count = args.Count();

            var label = new Label();
            Emit(37, label);

            if (member.Object is ThisExpression)
                scriptName = "this";
            else if (member.Object is Identifier objIdent)
                scriptName = objIdent.Name;

            // 处理参数
            foreach (var arg in args.Reverse())
            {
                Visit(arg);
                DeFormatting(arg);
            }

            if (member.Property is Identifier method)
            {
                Emit(34, [scriptName, method.Name, count], -(count + 5));
            }

            // 记录返回地址位置
            Emit(label.Ins);
            label.SetLine(_context, member);
        }

        private void ProcessCrossScriptCall(MemberExpression member, IEnumerable<Expression> args)
        {
            if (member.Object is Identifier objIdent &&
                objIdent.Name.StartsWith("sc_"))
            {
                ProcessAsyncCrossScriptCall(objIdent.Name, member.Property, args);
            }
            else
            {
                ProcessSyncCrossScriptCall(member, args);
            }
        }

        private void TryAddInstruction(InStruction ins)
        {
            if (ins == null)
                return;
            Emit(ins);
        }

        protected override object? VisitLiteral(Literal literal)
        {
            var ins = HandleLiteral(literal);
            TryAddInstruction(ins);

            return null;
        }

        private InStruction HandleLiteral(Literal literal)
        {
            // 根据类型生成PUSH指令
            object value;
            if (literal.Value is string)
            {
                value = literal.Value;
            }
            else if (literal.Raw.Contains("0x"))
            {
                value = Convert.ToUInt32(literal.Value);
            }
            else if (literal.Raw.Contains('.'))
            {
                value = Convert.ToSingle(literal.Value);
            }
            else
            {
                value = Convert.ToInt32(literal.Value);
            }

            return new InStruction
            {
                Code = 0, // PUSH
                Operands = [4, value],
            };
        }

        protected override object? VisitIdentifier(Identifier ident)
        {
            var ins = HandleIdentifier(ident.Name);
            TryAddInstruction(ins);

            return null;
        }

        private InStruction HandleIdentifier(string name)
        {
            if (_context.LocalScopes.TryGetValue(name, out int index))
            {
                // 局部变量使用RETRIEVEELEMENTATINDEX
                return new InStruction { Code = 2, Operands = [-(index * 4)] };
            }

            return null;
        }

        protected override object? VisitMemberExpression(MemberExpression member)
        {
            var ins = HandleMemberExpression(member.Object, member.Property);
            TryAddInstruction(ins);

            return null;
        }

        private InStruction HandleMemberExpression(Expression obj, Expression prop)
        {
            if (obj is not Identifier objIdent)
                return null;
            if (objIdent.Name == "Result" && prop is Literal lit)
            {
                // Result变量
                return new InStruction { Code = 9, Operands = [lit.Value] };
            }
            else if (
                objIdent.Name == "Global"
                && prop is Identifier ident
                && _globalVarMap.TryGetValue(ident.Name, out int index)
            )
            {
                // 局部变量使用LOAD32
                return new InStruction { Code = 7, Operands = [index] };
            }

            return null;
        }

        private void PopAsMarker()
        {
            if (_context.InsList.Count >= 2 &&
                _context.InsList[^1].Code == 1 &&
                _context.InsList[^2].Code == 38)
            {
                //_context.InsList.Reverse(_context.InsList.Count - 2, 2);
                (_context.InsList[^1], _context.InsList[^2]) = (_context.InsList[^2], _context.InsList[^1]);
            }
        }

        private void VisitBranch(Node node)
        {
            _context.IsReturn = node.ChildNodes.LastOrDefault() is ReturnStatement;
            using (_context.CaptureStackState())
            {
                Visit(node);
            }
            //PopAsMarker();
        }

        private void VisitBranch(IEnumerable<Node> nodes)
        {
            _context.IsReturn = nodes.LastOrDefault() is ReturnStatement;
            using (_context.CaptureStackState())
            {
                foreach (var node in nodes)
                {
                    Visit(node);
                }
            }
            //PopAsMarker();
        }

        private bool CreateMarker(out Label label)
        {
            var last = _context.InsList.LastOrDefault();
            if (last?.Code == 38)
            {
                label = new Label(last);
                return true;
            }
            label = new Label();
            return false;
        }

        private bool VisitAlternate(IfStatement ifStmt, Label elseLabel, Label endLabel)
        {
            if (ifStmt.Alternate == null)
                return false;

            var jumpIns = new InStruction()
            {
                Code = 11,
                Operands = [endLabel]
            };
            Emit(jumpIns);

            Emit(elseLabel.Ins);
            elseLabel.SetLine(
                _context,
                ifStmt.Consequent == null ? ifStmt.Test : ifStmt.Consequent
            );
            VisitBranch(ifStmt.Alternate);

            var last = _context.InsList.LastOrDefault();
            if (last?.Code == 38)
            {
                jumpIns.Operands[0] = new Label(last);
                return true;
            }

            return false;
        }

        protected override object? VisitIfStatement(IfStatement ifStmt)
        {
            var endLabel = new Label();
            var elseLabel = ifStmt.Alternate != null ? new Label() : endLabel;
            // 处理条件表达式
            Visit(ifStmt.Test);
            DeFormatting(ifStmt.Test);

            // 生成条件跳转
            Emit(15, elseLabel);

            if (ifStmt.Consequent != null)
            {
                // 处理真分支
                VisitBranch(ifStmt.Consequent);
            }

            if (!VisitAlternate(ifStmt, elseLabel, endLabel))
            {
                Emit(endLabel.Ins);
                endLabel.SetLine(_context, ifStmt);
            }

            return null;
        }

        private InStruction GetDiscriminant(Expression node)
        {
            if (node is Literal literal)
            {
                return HandleLiteral(literal);
            }
            else if (node is Identifier ident)
            {
                return HandleIdentifier(ident.Name);
            }
            else if (node is MemberExpression member)
            {
                return HandleMemberExpression(member.Object, member.Property);
            }

            return null;
        }

        protected override object? VisitSwitchStatement(SwitchStatement switchStmt)
        {
            var endLabel = new Label();

            // 处理判别式
            var discr = GetDiscriminant(switchStmt.Discriminant);

            List<SwitchCase> cases = [.. switchStmt.Cases];
            bool hasDefault = cases.Any(c => c.Test == null);
            bool hasReturn = cases.Any(c => c.Consequent.LastOrDefault() is ReturnStatement);
            bool hasNullConsequent = cases.Any(c => c.Consequent.Count == 0);
            using (_context.DisableLoopState())
            {
                if (hasDefault || hasReturn || hasNullConsequent)
                {
                    HaveDefaultCase(cases, discr, endLabel);
                }
                else
                {
                    DontHaveDefaultCase(cases, discr, endLabel);
                }
            }
            Emit(endLabel.Ins);
            endLabel.SetLine(_context, switchStmt);

            return null;
        }

        private void DontHaveDefaultCase(List<SwitchCase> cases, InStruction discr, Label endLabel)
        {
            // 生成条件分支链
            foreach (var caseBlock in cases)
            {
                var nextCaseLabel = new Label();

                if (caseBlock.Test != null)
                {
                    // 加载判别式和测试值
                    Emit(discr);
                    Visit(caseBlock.Test);

                    // 比较操作
                    Emit(21); // ==

                    var isLastBloack = caseBlock == cases.LastOrDefault();
                    if (isLastBloack)
                    {
                        nextCaseLabel = endLabel;
                    }
                    // 条件跳转
                    Emit(15, nextCaseLabel);

                    // 处理当前case的语句
                    VisitBranch(caseBlock.Consequent);

                    // 跳转到结束
                    if (!EndsWithReturn(caseBlock.Consequent) &&
                        !isLastBloack)
                    {
                        Emit(11, endLabel);
                    }

                    Emit(nextCaseLabel.Ins);
                    nextCaseLabel.SetLine(_context, caseBlock);
                }
            }
        }

        private void HaveDefaultCase(List<SwitchCase> cases, InStruction discr, Label endLabel)
        {
            var caseLabels = new List<Label>();
            var defaultLabel = new Label();
            // 生成条件分支链
            var nextCaseLabel = new Label();
            foreach (var caseBlock in cases)
            {
                if (caseBlock.Test != null)
                {
                    // 加载判别式和测试值
                    Emit(discr);
                    Visit(caseBlock.Test);

                    // 比较操作
                    Emit(21); // ==

                    // 条件跳转
                    Emit(14, nextCaseLabel);
                }
                else // default case
                {
                    Emit(11, nextCaseLabel);
                }

                caseLabels.Add(nextCaseLabel);
                if (caseBlock.Consequent.Count > 0)
                    nextCaseLabel = new Label();
            }
            var lastLabel = caseLabels.LastOrDefault();
            if (lastLabel is not null && !lastLabel.Equals(defaultLabel))
            {
                Emit(11, endLabel);
            }

            foreach (var (caseBlock, label) in cases.Zip(caseLabels))
            {
                if (caseBlock.Consequent.Count == 0)
                    continue;

                Emit(label.Ins);
                label.SetLine(_context, caseBlock);

                // 处理当前case的语句
                VisitBranch(caseBlock.Consequent);

                // 跳转到结束
                if (EndsWithReturn(caseBlock.Consequent))
                {
                    continue;
                }
                Emit(11, endLabel);
            }
        }

        private bool EndsWithReturn(IEnumerable<Statement> statements)
        {
            var lastStmt = statements.LastOrDefault();
            return lastStmt is ReturnStatement;
        }

        private void VisitReturnArgument(Expression expression, int index = 0)
        {
            Visit(expression);
            DeFormatting(expression);

            // 保存到结果数组
            Emit(10, index);
        }

        private void VisitReturnLiteralOrNull(Literal lit)
        {
            if (lit.Value is null)
            {
                Emit(0, [4, (uint)0]);
            }
            else
            {
                Visit(lit); // 生成参数计算代码
                DeFormatting(lit);
            }
            // 保存到结果数组
            Emit(10, 0);
        }

        protected override object? VisitReturnStatement(ReturnStatement ret)
        {
            // 处理返回值
            if (ret.Argument != null)
            {
                if (ret.Argument is Literal lit)
                {
                    VisitReturnLiteralOrNull(lit);
                }
                else if (ret.Argument is SequenceExpression sequence)
                {
                    var index = 0;
                    foreach (var expr in sequence.Expressions)
                    {
                        VisitReturnArgument(expr, index);
                        index++;
                    }
                }
                else
                {
                    VisitReturnArgument(ret.Argument);
                }
            }
            // 清理操作数栈（弹出所有临时值）
            _context.StackClear();
            // 函数退出
            Emit(13); // EXIT

            return null;
        }

        protected override object? VisitWhileStatement(WhileStatement whileStmt)
        {
            var startLabel = new Label();
            var endLabel = new Label();

            // 循环开始标签
            Emit(startLabel.Ins);
            startLabel.SetLine(whileStmt.Location.Start.Line);
            // 处理条件表达式
            Visit(whileStmt.Test);
            DeFormatting(whileStmt.Test);

            // 条件跳转
            Emit(15, endLabel);
            // 处理循环体
            using (_context.CaptureLoopState())
            {
                _context.Loop.EnterLoop(startLabel, endLabel);
                VisitBranch(whileStmt.Body);
                _context.Loop.ExitLoop();
            }
            // 跳回循环开始
            Emit(11, startLabel);
            // 循环结束标签
            Emit(endLabel.Ins);
            endLabel.SetLine(_context, whileStmt);

            return null;
        }

        protected override object? VisitContinueStatement(ContinueStatement continueStatement)
        {
            Emit(11, _context.Loop.StartLabel);
            return null;
        }

        protected override object? VisitBreakStatement(BreakStatement breakStatement)
        {
            if (_context.Loop.IsInLoop)
            {
                Emit(11, _context.Loop.EndLabel);
            }
            return null;
        }

        protected override object? VisitFunctionDeclaration(FunctionDeclaration func)
        {
            _context = new() { FunctionId = _tempDat.Functions.Count, Pop = this.HandlePop };
            _tempDat.Functions.Add(_context);
            if (func.Id is Identifier ident)
            {
                _context.Func.Name = ident.Name;
            }
            if (_context.Func.Name == "AniBtlHideCharaEffect")
            {
                _context.Func.Name = "AniBtlHideCharaEffect";
            }

            _parser.Parse(_comments[_context.FunctionId].Value, _context.Func);
            _tempDat.FunctionMap.Add(_context.Func.Name, _context.FunctionId);
            foreach (var param in func.Params)
            {
                if (param is Identifier identParam)
                {
                    _context.StackDepth++;
                    _context.AddLocalVar(identParam.Name);
                }
            }

            Visit(func.Body);
            return null;
        }
    }

    public class TempDatStruct
    {
        public Dictionary<string, uint> GlobalVarMap = [];
        public Dictionary<string, int> FunctionMap = [];
        public List<CompileContext> Functions = [];
    }
}