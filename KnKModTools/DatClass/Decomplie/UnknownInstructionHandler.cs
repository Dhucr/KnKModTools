namespace KnKModTools.DatClass.Decomplie
{
    public class UnknownInstructionHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode > 39;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            return new ExpressionNode() { Expression = $"// 未处理指令: {instr.Code}" };
        }
    }
}