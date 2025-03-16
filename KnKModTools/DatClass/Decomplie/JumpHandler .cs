namespace KnKModTools.DatClass.Decomplie
{
    //暂未使用
    public class JumpHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 11 or 14 or 15;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var targetAddr = (uint)instr.Operands[0];
            var label = core.GetLabel(ctx, targetAddr);

            if (instr.Code == 11) // 无条件跳转
            {
                ctx.PendingJumps.Push(new JumpInfo(label, GenerateEndLabel(ctx)));
                return new ExpressionNode { Expression = $"goto {label}" };
            }
            else // 条件跳转
            {
                ExpressionNode condition = Pop(ctx);
                var op = instr.Code == 14 ? "if" : "if (!";
                return new ExpressionNode
                {
                    Expression = $"{op}({condition.Expression}) goto {label}"
                };
            }
        }

        private string GenerateEndLabel(DecompileContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}