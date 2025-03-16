namespace KnKModTools.DatClass.Decomplie
{
    public class RetrieveHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 2 or 3;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var byteOffset = (int)instr.Operands[0];
            var elementIndex = -byteOffset / 4;
            Push(ctx, ctx.EvalStack.GetElementAtIndex(elementIndex));

            return null;
        }
    }
}