namespace KnKModTools.DatClass.Decomplie
{
    public class ResultHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 9 or 10;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            int index = (byte)instr.Operands[0];

            if (instr.Code == 9) // LOADRESULT
            {
                ctx.ResultMap.Remove(index);
                Push(ctx, $"Result[{index}]");
            }
            else // SAVERESULT
            {
                ExpressionNode value = Pop(ctx);
                ctx.ResultMap[index] = value.Expression;
                var str = value.Expression == "0x0" ? "null" : value.Expression;
                if (ctx.CurrentNode.Statements.LastOrDefault()
                    is ExpressionNode ex && str == "Result[0]")
                {
                    str = ex.Expression;
                    ctx.CurrentNode.Statements.Remove(ex);
                }
                return new ExpressionNode
                {
                    Expression = $"Result[{index}] = {str}"
                };
            }
            return null;
        }
    }
}