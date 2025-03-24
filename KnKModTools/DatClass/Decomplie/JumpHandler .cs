namespace KnKModTools.DatClass.Decomplie
{
    //暂未使用
    public class JumpHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 11;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            if(!ctx.Loop.IsInLoop) return null;

            var targetAddr = (uint)instr.Operands[0];
            if(targetAddr > ctx.Loop.BreakAddr)
            {
                return new ExpressionNode()
                {
                    Expression = "break"
                };
            }
            else if(instr.Offset < ctx.Loop.EndAddr && targetAddr == ctx.Loop.WhileAddr)
            {
                return new ExpressionNode()
                {
                    Expression = "continue"
                };
            }
            else
            {
                return null;
            }
        }

        private string GenerateEndLabel(DecompileContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}