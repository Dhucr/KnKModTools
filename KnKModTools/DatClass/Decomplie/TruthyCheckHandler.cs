namespace KnKModTools.DatClass.Decomplie
{
    public class TruthyCheckHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 32;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            ExpressionNode value = Pop(ctx);
            Push(ctx, $"IsTrue({value.Expression})");

            return null;
        }
    }
}