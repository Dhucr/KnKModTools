namespace KnKModTools.DatClass.Decomplie
{
    public class BinaryOpHandler : BaseInstructionHandler
    {
        private static readonly Dictionary<byte, (string Symbol, int Precedence)> _opMap = new()
        {
            [16] = ("+", 12),
            [17] = ("-", 12),
            [18] = ("*", 13),
            [19] = ("/", 13),
            [20] = ("%", 13),
            [21] = ("==", 8),
            [22] = ("!=", 8),
            [23] = (">", 9),
            [24] = (">=", 9),
            [25] = ("<", 9),
            [26] = ("<=", 9),
            [27] = ("&", 6),
            [28] = ("|", 5),
            [29] = ("&&", 4),
            [30] = ("||", 3)
        };

        public override bool CanHandle(byte opCode) => _opMap.ContainsKey(opCode);

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            (string symbol, int precedence) = _opMap[instr.Code];
            ExpressionNode right = Pop(ctx);
            ExpressionNode left = Pop(ctx);

            var isRightAssociative = false;

            var leftExpr = FormatSubExpression(left, precedence, isRightAssociative, isLeft: true);
            var rightExpr = FormatSubExpression(right, precedence, isRightAssociative, isLeft: false);

            rightExpr = CodeFormatting.SimplifyConditions(ctx, rightExpr);
            leftExpr = CodeFormatting.SimplifyConditions(ctx, leftExpr);

            Push(ctx, new ExpressionNode()
            {
                Expression = $"{leftExpr} {symbol} {rightExpr}",
                Operator = symbol,
                Precedence = precedence
            });
            return null;
        }

        private string FormatSubExpression(ExpressionNode node, int parentPrecedence, bool parentIsRightAssociative, bool isLeft)
        {
            var needsParenthesis = CheckNeedParenthesis(node, parentPrecedence, parentIsRightAssociative, isLeft);
            return needsParenthesis ? $"({node.Expression})" : node.Expression;
        }

        private bool CheckNeedParenthesis(ExpressionNode child, int parentPrecedence, bool parentIsRightAssociative, bool isLeft)
        {
            // 如果子节点不是二元操作符（如常量、变量），则无需括号
            if (string.IsNullOrEmpty(child.Operator))
                return false;

            var childPrecedence = child.Precedence;

            if (childPrecedence < parentPrecedence)
            {
                return true;
            }
            else if (childPrecedence == parentPrecedence)
            {
                // 左子节点需括号当父操作符右结合；右子节点需括号当父操作符左结合
                return isLeft ? parentIsRightAssociative : !parentIsRightAssociative;
            }
            else
            {
                return false;
            }
        }
    }
}