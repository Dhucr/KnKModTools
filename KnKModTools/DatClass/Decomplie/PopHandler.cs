namespace KnKModTools.DatClass.Decomplie
{
    public class PopHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 1 or 39;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var elements = (byte)instr.Operands[0] / 4;
            if (instr.Code == 39)
                elements = (byte)instr.Operands[0];
            for (var i = 0; i < elements; i++)
            {
                if (ctx.EvalStack.Count > 0)
                    Pop(ctx);
            }
            return null;
        }
    }
}