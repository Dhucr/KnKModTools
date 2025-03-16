namespace KnKModTools.DatClass.Decomplie
{
    public class LineMarkerHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 38;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            return null;
        }
    }
}