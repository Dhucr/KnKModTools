using KnKModTools.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace KnKModTools.DatClass.Decomplie
{
    /// <summary>
    /// 反编译器核心实现，负责将中间指令转换为高级语言代码
    /// </summary>
    public class DecompilerCore
    {
        #region 字段与构造函数

        private readonly Dictionary<int, string> _varInMap = [];
        private readonly DatScript _datScript;

        private readonly CodeGenerate _codeGenerate = new();

        private readonly List<IInstructionHandler> _handlers =
        [
            new PushHandler(),
            new PopHandler(),
            new RetrieveHandler(),
            new PushConvertIntgerHandler(),
            new AssignmentHandler(),
            new LoadStoreHandler(),
            new CallHandler(),
            new CrossScriptCallHandler(),
            new ExitHandler(),
            new BinaryOpHandler(),
            new ResultHandler(),
            new UnaryOpHandler(),
            new RunCmdHandler(),
            new PushAddressAtOtherScriptHandler(),
            new TruthyCheckHandler(),
            new LineMarkerHandler(),
            new UnknownInstructionHandler()
        ];

        /// <summary>
        /// 构造函数，初始化变量映射
        /// </summary>
        /// <param name="datScript">目标脚本数据对象</param>
        public DecompilerCore(DatScript datScript)
        {
            _datScript = datScript;
            for (var i = 0; i < _datScript.VariableInCount; i++)
            {
                _varInMap.Add(i, _datScript.VariableIns[i].Name);
            }
        }

        #endregion 字段与构造函数

        #region 公共接口

        public string DecompileDatScript()
        {
            var funcs = new List<string>();
            foreach (Function func in _datScript.Functions)
            {
                funcs.Add(DecompileFunction(func));
            }

            return _codeGenerate.Script(this, _datScript, funcs);
        }

        /// <summary>
        /// 反编译指定函数为可读代码
        /// </summary>
        /// <param name="function">要反编译的函数对象</param>
        /// <returns>生成的高级语言代码</returns>
        public string DecompileFunction(Function function)
        {
            var context = new DecompileContext
            {
                EvalStack = new Stack<ExpressionNode>(),
                LabelMap = [],
                PendingClosures = new()
            };

            IEnumerable<string> array = Enumerable.Range(0, function.InArgs.Length).Select(i => $"arg{i}");
            foreach (var item in array)
            {
                context.EvalStack.Push(new ExpressionNode() { Expression = item });
            }

            AstNode func = BuildFunction(context, function.InStructions);

            if (context.IsIrreducibleCFG)
            {
                LogHelper.Log($"{function.Name}:{Utilities.GetDisplayName("ICFGWarning")}");
                return "";
            }

            return _codeGenerate.Function(this, function, array, func.GenerateCode);
        }

        private AstNode BuildFunction(DecompileContext ctx, InStruction[] instructions)
        {
            BuildCFG(ctx, instructions);
            return BuildAstFromCFG(ctx);
        }

        #endregion 公共接口

        #region 控制流分析

        /// <summary>
        /// 构建控制流图（CFG）
        /// </summary>
        /// <param name="ctx">反编译上下文</param>
        /// <param name="instructions">指令集合</param>
        private void BuildCFG(DecompileContext ctx, InStruction[] instructions)
        {
            // 实现分为三个阶段：收集跳转目标、构建基本块、建立跳转关系
            // 阶段1：收集所有跳转目标
            var jumpTargets = new HashSet<uint>();
            foreach (InStruction? instr in instructions.Where(i => IsBranching(i.Code)))
            {
                jumpTargets.Add((uint)instr.Operands[0]);
            }

            // 阶段2：构建基本块
            BuildBaseBlocks(ctx, instructions, jumpTargets);

            // 阶段3：建立跳转关系
            BuildJumpRelationship(ctx);
        }

        private AstNode BuildAstFromCFG(DecompileContext ctx)
        {
            var rootBlock = new BlockNode();

            foreach (BasicBlock? block in ctx.Blocks.OrderBy(b => b.StartAddr))
            {
                BlockNode node = ControlFlowFactory.CreateControlFlow(ctx, this, block);

                if (ctx.IsIrreducibleCFG) break;
                if (node == null) continue;
                rootBlock.Statements.Add(node);
            }
            return rootBlock;
        }
        private void BuildBaseBlocks(DecompileContext ctx,
    InStruction[] instructions, HashSet<uint> jumpTargets)
        {
            var currentBlock = new BasicBlock();
            ctx.Blocks.Add(currentBlock);
            foreach (InStruction instr in instructions)
            {
                // 检测是否需要分割块
                bool shouldSplit = false;

                // 情况1：当前指令是跳转目标
                if (jumpTargets.Contains(instr.Offset))
                {
                    shouldSplit = true;
                }

                // 情况2：前一条指令是跳转指令
                if (currentBlock.Instructions.Count > 0 &&
                    IsBranching(currentBlock.Instructions.Last().Code))
                {
                    shouldSplit = true;
                }

                
                if (shouldSplit && currentBlock.Instructions.Count > 0)
                {
                    currentBlock = new BasicBlock();
                    ctx.Blocks.Add(currentBlock);
                }
                // 标记块起始地址（第一条指令）
                if (!currentBlock.Instructions.Any())
                {
                    currentBlock.StartAddr = instr.Offset;
                }
                currentBlock.EndAddr = instr.Offset;
                currentBlock.Instructions.Add(instr);
                ctx.AddrToBlock[instr.Offset] = currentBlock;
            }
        }

        /*
        private void BuildBaseBlocks(DecompileContext ctx,
            InStruction[] instructions, HashSet<uint> jumpTargets)
        {
            var currentBlock = new BasicBlock();
            ctx.Blocks.Add(currentBlock);

            foreach (InStruction instr in instructions)
            {
                // 检测是否需要分割块
                var shouldSplit = currentBlock.Instructions.Count > 0 &&
                    (jumpTargets.Contains(instr.Offset) ||
                    IsBranching(currentBlock.Instructions.Last().Code));
                if (shouldSplit)
                {
                    currentBlock = new BasicBlock();
                    ctx.Blocks.Add(currentBlock);
                }

                // 标记块起始地址（第一条指令）
                if (!currentBlock.Instructions.Any())
                {
                    currentBlock.StartAddr = instr.Offset;
                }

                // 始终更新块结束地址
                currentBlock.EndAddr = instr.Offset;

                currentBlock.Instructions.Add(instr);
                ctx.AddrToBlock[instr.Offset] = currentBlock;
            }
        }*/

        private void BuildJumpRelationship(DecompileContext ctx)
        {
            foreach (BasicBlock block in ctx.Blocks)
            {
                InStruction? lastInstr = block.Instructions.LastOrDefault();
                if (lastInstr == null) continue;

                switch (lastInstr.Code)
                {
                    case 11: // JUMP
                        block.Type = BlockType.Jump;
                        var jumpTarget = (uint)lastInstr.Operands[0];
                        block.TrueSuccessors.Add(GetBlock(ctx, jumpTarget));
                        break;

                    case 14: // JUMPIFTRUE（条件为真时跳转）
                        block.Type = BlockType.IFTrue;
                        /*var trueTarget = (uint)lastInstr.Operands[0];
                        block.FalseSuccessors.AddRange(
                            AddIFTrueElseBlocks(ctx, block, trueTarget)); // False分支（可能为null）
                        block.TrueSuccessors.AddRange(
                            AddIFTrueThenBlocks(ctx, block, block.FalseSuccessors, trueTarget));  // True分支
                        RemoveLastBlock(ctx, block);*/
                        IFTTest(ctx, block);
                        break;

                    case 15: // JUMPIFFALSE（条件为假时跳转）
                        block.Type = BlockType.IFFlase;
                        /*var falseTarget = (uint)lastInstr.Operands[0];
                        block.TrueSuccessors.AddRange(
                            AddIFFalseElseBlocks(ctx, block, falseTarget));  // False分支
                        block.FalseSuccessors.AddRange(
                            AddIFFalseThenBlocks(ctx, block.TrueSuccessors, falseTarget)); // True分支（可能为null）
                        RemoveLastBlock(ctx, block);*/
                        IFFTest(ctx, block);
                        break;

                    case 13:
                        block.Type = BlockType.Exit;
                        break;

                    default:
                        block.Type = BlockType.Base;
                        break;
                }
            }
        }
        
        private void RemoveLastBlock(DecompileContext ctx, BasicBlock block)
        {
            var last = ctx.Blocks.LastOrDefault();
            if(block.TrueSuccessors != null && block.TrueSuccessors.Count > 0)
            {
                block.TrueSuccessors.Remove(last);
            }
            if(block.FalseSuccessors != null && block.FalseSuccessors.Count > 0)
            {
                block.FalseSuccessors.Remove(last);
            }
        }

        private void IFFTest(DecompileContext ctx, BasicBlock block)
        {
            var trueSuccessors = new List<BasicBlock>();
            var falseSuccessors = new List<BasicBlock>();
            var endAddr = GetAddress(block);
            var trueBlock = block;
            var falseBlock = GetBlock(ctx, endAddr);
            while (true)
            {
                var nextBlock = GetNextBlock(ctx, trueBlock);
                if (nextBlock is null) break;
                if (nextBlock.StartAddr == endAddr) break;
                if (nextBlock.Equals(falseBlock)) break;

                trueSuccessors.Add(nextBlock);
                trueBlock = nextBlock;
            }

            if (trueSuccessors.Count == 0) return;
            block.TrueSuccessors.AddRange(trueSuccessors);

            var last = trueBlock.Instructions.LastOrDefault();
            if (last is null) return;
            if (!IsBranching(last.Code)) return;

            endAddr = (uint)last.Operands[0];
            if (endAddr < last.Offset) return;
            if (endAddr == falseBlock.StartAddr) return;

            var blockRange = 0u;
            while (true)
            {
                var isJumpBlock = GetCode(falseBlock) == 15;
                if(isJumpBlock || !isJumpBlock && 
                    falseBlock.StartAddr >= blockRange)
                {
                    falseSuccessors.Add(falseBlock);
                }
                
                var nextBlock = GetNextBlock(ctx, falseBlock);
                if (isJumpBlock)
                {
                    nextBlock = GetBlock(ctx, GetAddress(falseBlock));
                    var temp = GetPriviousBlock(ctx, nextBlock);
                    if(temp is not null && GetCode(temp) == 11)
                    {
                        blockRange = GetAddress(temp);
                    }
                }
                
                if (nextBlock is null) break;
                if (nextBlock.StartAddr == endAddr) break;

                falseBlock = nextBlock;
            }

            if(falseSuccessors.Count == 0) return;
            block.FalseSuccessors.AddRange(falseSuccessors);
        }

        private void IFTTest(DecompileContext ctx, BasicBlock block)
        {
            var trueSuccessors = new List<BasicBlock>();
            var falseSuccessors = new List<BasicBlock>();
            var endAddr = GetAddress(block);
            var falseBlock = block;
            var trueBlock = GetBlock(ctx, endAddr);
            while (true)
            {
                var nextBlock = GetNextBlock(ctx, falseBlock);
                if (nextBlock is null) break;
                if (nextBlock.StartAddr == endAddr) break;
                if (nextBlock.Equals(trueBlock)) break;

                falseSuccessors.Add(nextBlock);
                falseBlock = nextBlock;
                if (GetCode(nextBlock) == 11) break;
            }

            if (falseSuccessors.Count == 0) return;
            block.FalseSuccessors.AddRange(falseSuccessors);

            var last = falseSuccessors.FirstOrDefault()?.
                Instructions.LastOrDefault();
            if (last is null) return;
            if (!IsBranching(last.Code)) return;

            endAddr = (uint)last.Operands[0];
            if (endAddr < last.Offset) return;
            if (endAddr == trueBlock.StartAddr) return;

            while (true)
            {
                trueSuccessors.Add(trueBlock);
                var nextBlock = GetNextBlock(ctx, trueBlock);
                if (nextBlock is null) break;
                if (nextBlock.StartAddr == endAddr) break;

                trueBlock = nextBlock;
            }

            if(trueSuccessors.Count == 0) return;
            block.TrueSuccessors.AddRange(trueSuccessors);
        }

        private List<BasicBlock> AddIFTrueElseBlocks(DecompileContext ctx,
            BasicBlock block, uint endAddr)
        {
            return CreateElseSuccessors(ctx, block, endAddr, next =>
                next.Instructions.Count == 1);
        }

        private List<BasicBlock> AddIFFalseElseBlocks(DecompileContext ctx,
            BasicBlock block, uint endAddr)
        {
            return CreateElseSuccessors(ctx, block, endAddr, next => false);
        }

        private List<BasicBlock> CreateElseSuccessors(DecompileContext ctx,
            BasicBlock block, uint endAddr, Predicate<BasicBlock> predicate)
        {
            var successors = new List<BasicBlock>();
            var next = GetNextBlock(ctx, block);

            var addr = next.StartAddr;
            while (addr < endAddr)
            {
                successors.Add(next);

                InStruction? last = next.Instructions.LastOrDefault();
                if (last?.Code == 13 ||
                   last?.Code == 11 &&
                   (predicate(next) ||
                   (uint)last.Operands[0] > endAddr)) break;

                next = GetNextBlock(ctx, next);
                if (next == null) break;
                addr = next.StartAddr;
            }

            return successors;
        }

        private List<BasicBlock> AddIFFalseThenBlocks(DecompileContext ctx, List<BasicBlock> nextBlocks, uint targetAddr)
        {
            uint endAddr = 0;
            InStruction ins = GetLastIns(nextBlocks);
            if (ins != null && IsBranching(ins.Code))
            {
                endAddr = (uint)ins.Operands[0];
            }

            return CreateThenSuccessors(ctx, nextBlocks, targetAddr, endAddr, true);
        }

        private List<BasicBlock> AddIFTrueThenBlocks(DecompileContext ctx, BasicBlock block,
            List<BasicBlock> nextBlocks, uint targetAddr)
        {
            BasicBlock next = GetNextBlock(ctx, block);
            var endAddr = GetAddress(next);

            return CreateThenSuccessors(ctx, nextBlocks, targetAddr, endAddr, false);
        }

        private List<BasicBlock> CreateThenSuccessors(DecompileContext ctx,
            List<BasicBlock> nextBlocks, uint targetAddr, uint endAddr, bool isFalse)
        {
            var successors = new List<BasicBlock>();
            InStruction last = GetLastIns(nextBlocks);
            if (nextBlocks?.Count == 0 || !IsBranching(last.Code))
                return successors;

            BasicBlock start = GetBlock(ctx, targetAddr);
            //false <
            //true <=
            var check = targetAddr <= endAddr;
            /*if (isFalse)
            {
                check = targetAddr < endAddr;
            }*/
            while (targetAddr <= endAddr)
            {
                successors.Add(start);
                start = GetNextBlock(ctx, start);
                if (start == null) break;
                targetAddr = GetInsLength(start);
            }

            return successors;
        }

        #endregion 控制流分析

        #region 工具方法

        /// <summary>
        /// 判断指令是否引起控制流变化
        /// </summary>
        /// <param name="code">操作码</param>
        /// <returns>是否是分支指令</returns>
        public bool IsBranching(byte code) => code is 11 or 14 or 15;

        private List<AstNode> ProcessPendingJumps(DecompileContext ctx)
        {
            var list = new List<AstNode>();
            while (ctx.PendingJumps.Count > 0)
            {
                JumpInfo jump = ctx.PendingJumps.Pop();
                list.Add(new ExpressionNode() { Expression = $"{jump.EndLabel}:" });
            }

            return list;
        }

        public AstNode HandleBaseInstruction(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            IInstructionHandler? handle = _handlers.Find(h => h.CanHandle(instr.Code));
            return handle.Handle(ctx, instr, core);
        }

        public string GetVariableName(int index)
        {
            return _varInMap.TryGetValue(index, out var name)
                ? name : $"UNKNOWN_{index}";
        }

        /// <summary>
        /// 获取格式化后的值表示
        /// </summary>
        /// <param name="value">原始值</param>
        /// <returns>格式化后的字符串</returns>
        public string FormatValue(object value)
        {
            return value switch
            {
                //float f => $"{f:0.0#####}f",
                float f => $"{f:0.0#####}",
                string s => $"\"{s}\"",
                uint u => $"0x{u:X}",
                int u => u.ToString(),
                _ => throw new NotSupportedException()
            };
        }

        public string GetFunctionName(int funcId)
        {
            return _datScript.Functions[funcId].Name;
        }

        public int GetParamCountFromMetadata(int funcId)
        {
            // 此处应查询函数元数据，示例返回固定值
            return _datScript.Functions[funcId].InArgs.Length;
        }

        // 新增：动态获取指令长度
        public uint GetInsLength(BasicBlock block)
        {
            InStruction? instr = block.Instructions.LastOrDefault();
            return (uint)(block.EndAddr + instr.Code switch
            {
                0 => 6,
                1 or 9 or 10 or 39 => 2,
                12 => 3,
                2 or 3 or 4 or 5 or 6 or 7 or 8 or 11 or 14 or 15 or 37 or 40 => 5,
                34 or 35 => 10,
                36 => 4,
                _ => 1
            });
        }

        public string GetLabel(DecompileContext ctx, uint address)
        {
            if (!ctx.LabelMap.ContainsKey(address))
            {
                ctx.LabelMap[address] = $"label_{address:X6}";
            }
            return ctx.LabelMap[address];
        }

        private string GenerateUniqueLabel(DecompileContext ctx)
        {
            return $"label_{ctx.LabelCounter++:000}";
        }

        #region 控制流工具方法

        public InStruction GetLastIns(List<BasicBlock> blocks)
        {
            BasicBlock? lastBlock = blocks?.LastOrDefault();
            InStruction? lastInstruction = lastBlock?.Instructions.LastOrDefault();

            return lastInstruction;
        }

        public uint GetAddress(BasicBlock block)
        {
            InStruction? last = block?.Instructions?.LastOrDefault();
            return last == null ? 0 : (uint)last.Operands[0];
        }

        public uint GetAddress(List<BasicBlock> blocks)
        {
            InStruction? last = GetLastIns(blocks);
            return last == null ? 0 : (uint)last.Operands[0];
        }

        public uint GetOffset(BasicBlock block)
        {
            InStruction? last = block?.Instructions?.LastOrDefault();
            return last == null ? 0 : last.Offset;
        }

        public uint GetOffset(List<BasicBlock> blocks)
        {
            InStruction? last = GetLastIns(blocks);
            return last == null ? 0 : last.Offset;
        }

        public byte GetCode(BasicBlock block)
        {
            InStruction? last = block?.Instructions?.LastOrDefault();
            return last == null ? (byte)66 : last.Code;
        }

        public BasicBlock GetTrueBlock(DecompileContext ctx, BasicBlock block)
        {
            // 确定分支类型
            InStruction lastInstr = block.Instructions.Last();

            if (lastInstr.Code != 14 && lastInstr.Code != 15) return null;

            return block.TrueSuccessors.Count == 0 ? null : block.TrueSuccessors.LastOrDefault();
        }

        public BasicBlock GetFalseBlock(DecompileContext ctx, BasicBlock block)
        {
            // 确定分支类型
            InStruction lastInstr = block.Instructions.Last();

            if (lastInstr.Code != 14 && lastInstr.Code != 15) return null;

            return block.FalseSuccessors.Count == 0 ? null : block.FalseSuccessors.FirstOrDefault();
        }

        public BasicBlock GetBlock(DecompileContext ctx, uint addr)
        {
            if (!ctx.AddrToBlock.TryGetValue(addr, out BasicBlock? block))
            {
                // 创建缺失的块并记录警告
                block = new BasicBlock { StartAddr = addr };
                ctx.Blocks.Add(block);
                ctx.AddrToBlock[addr] = block;
            }
            return block;
        }

        public BasicBlock GetCurrentBlock(DecompileContext ctx, uint addr)
        {
            foreach (BasicBlock block in ctx.Blocks)
            {
                foreach (InStruction instr in block.Instructions)
                {
                    if (instr.Offset == addr)
                    {
                        return block;
                    }
                }
            }

            return null;
        }

        public InStruction? GetNextIns(DecompileContext ctx, uint addr)
        {
            foreach (BasicBlock block in ctx.Blocks)
            {
                for (var i = 0; i < block.Instructions.Count; i++)
                {
                    if (block.Instructions[i].Offset != addr)
                    {
                        continue;
                    }

                    if (i == block.Instructions.Count - 1)
                    {
                        var b = GetNextBlock(ctx, block);
                        return b?.Instructions.Count > 0 ? b.Instructions[0] : null;
                    }
                    return block.Instructions[i + 1];
                }
            }

            return null;
        }

        public BasicBlock? GetNextBlock(DecompileContext ctx, BasicBlock current)
        {
            var index = ctx.Blocks.IndexOf(current);
            return index >= 0 && index < ctx.Blocks.Count - 1
                   ? ctx.Blocks[index + 1]
                   : null;
        }

        public BasicBlock? GetPriviousBlock(DecompileContext ctx, BasicBlock current)
        {
            var index = ctx.Blocks.IndexOf(current);
            return index > 0 ? ctx.Blocks[index - 1] : null;
        }

        public BasicBlock? GetPriviousJumpBlock(DecompileContext ctx, BasicBlock current)
        {
            while (true)
            {
                var block = GetPriviousBlock(ctx, current);
                if (block != null) break;
                if (block?.Type == BlockType.Jump)
                {
                    return block;
                }
            }
            return null;
        }

        public void BuildBaseInstruction(DecompileContext ctx,
            BasicBlock block, BlockNode currentBlock)
        {
            using (ctx.CaptureNodeState(currentBlock))
            {
                foreach (InStruction? instr in block.Instructions.Where(i => !IsBranching(i.Code)))
                {
                    AstNode node = HandleBaseInstruction(ctx, instr, this);
                    if (node == null) continue;
                    currentBlock.Statements.Add(node);
                }
            }
        }

        public void BuildBaseInstruction(DecompileContext ctx, BasicBlock block)
        {
            foreach (InStruction? instr in block.Instructions.Where(i => !IsBranching(i.Code)))
            {
                AstNode node = HandleBaseInstruction(ctx, instr, this);
                if (node == null) continue;
            }
        }

        public ExpressionNode GetCondition(DecompileContext ctx, bool isTest)
        {
            ExpressionNode value = ctx.EvalStack.Pop();

            if (isTest) return value;

            CodeFormatting.SimplifyConditions(ctx, value);

            return value;
        }

        public string GetCaseMatchValue(DecompileContext ctx, BasicBlock block, string test)
        {
            var match = "NotCase";

            if (block == null || block.Visited || block.Instructions.Count == 1 ||
                block.Instructions[^2].Code != 21) return match;

            using (ctx.CaptureStackState())
            {
                BuildBaseInstruction(ctx, block);

                ExpressionNode matchValue = ctx.EvalStack.Pop();

                var cond = matchValue.Expression.Split(" == ");
                if (cond.Length != 2 || cond[0] != test) return match;

                match = cond[1];
                block.Visited = true;
            }

            return match;
        }

        public ExpressionNode GetChainConditions(DecompileContext ctx, BasicBlock block)
        {
            if (block == null || block.Visited) return null;

            ExpressionNode matchValue = null;
            using (ctx.CaptureStackState())
            {
                var currentBlock = new BlockNode();
                BuildBaseInstruction(ctx, block, currentBlock);
                matchValue = ctx.EvalStack.Pop();

                var pattern = @"Result\[(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\]";
                MatchCollection matches = Regex.Matches(matchValue.Expression, pattern);

                if (matches.Count < currentBlock.Statements.Count) return null;

                CodeFormatting.SimplifyConditions(ctx,
                    matchValue, matches, currentBlock.Statements);
                block.Visited = true;
            }

            return matchValue;
        }

        #endregion 控制流工具方法

        #endregion 工具方法
    }
}