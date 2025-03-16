namespace KnKModTools.DatClass.Decomplie
{
    public interface IInstructionHandler
    {
        bool CanHandle(byte opCode);

        AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core);
    }
}