namespace KnKModTools.DatClass.Decomplie
{
    public class ExitHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 13;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var value = "";
            if (ctx.CurrentNode.Statements.LastOrDefault() is ExpressionNode ex)
            {
                value = ex.Expression.Contains("Result[0] = ") ?
                    ex.Expression.Replace("Result[0] =", "") : value;
                ctx.CurrentNode.Statements.Remove(ex);
            }

            return new ExpressionNode
            {
                Expression = $"return{value}"
            };
        }
    }
}