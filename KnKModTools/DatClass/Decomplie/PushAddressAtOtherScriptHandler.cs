namespace KnKModTools.DatClass.Decomplie
{
    public class PushAddressAtOtherScriptHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 37;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            for (var i = 0; i < 5; i++) Push(ctx, $"{instr.Operands[0]}");

            return null;
        }
    }
}