namespace KnKModTools.DatClass.Decomplie
{
    public class LoadStoreHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 7 or 8;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var index = (int)instr.Operands[0];
            var varName = core.GetVariableName(index);

            if (instr.Code == 7) // LOAD32
            {
                Push(ctx, $"Global.{varName}");
                return null;
            }
            else // STORE32
            {
                ExpressionNode value = Pop(ctx);
                return new ExpressionNode
                {
                    Expression = $"Global.{varName} = {value.Expression}"
                };
            }
        }
    }
}