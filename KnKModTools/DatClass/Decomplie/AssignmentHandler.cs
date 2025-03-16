namespace KnKModTools.DatClass.Decomplie
{
    public class AssignmentHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 5 or 6;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var elementIndex = -(int)instr.Operands[0] / 4;
            ExpressionNode value = Pop(ctx);

            return new ExpressionNode()
            {
                Expression = CodeFormatting.TryInitializeLocalVar(
                ctx, elementIndex, value.Expression)
            };
        }
    }
}