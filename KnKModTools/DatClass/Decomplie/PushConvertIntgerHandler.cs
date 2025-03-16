namespace KnKModTools.DatClass.Decomplie
{
    public class PushConvertIntgerHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 4;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var elementIndex = -(int)instr.Operands[0] / 4;

            Push(ctx, new ExpressionNode()
            {
                Expression =
                $"GetAddress({ctx.EvalStack.GetElementAtIndex(elementIndex)})"
            });

            return null;
        }
    }
}