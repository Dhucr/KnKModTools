namespace KnKModTools.DatClass.Decomplie
{
    public class UnaryOpHandler : BaseInstructionHandler
    {
        private static readonly Dictionary<byte, string> _opMap = new()
        {
            [31] = "-",
            [33] = "~"
        };

        public override bool CanHandle(byte opCode) => _opMap.ContainsKey(opCode);

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            ExpressionNode value = Pop(ctx);
            Push(ctx, $"{_opMap[instr.Code]}{value.Expression}");

            return null;
        }
    }
}