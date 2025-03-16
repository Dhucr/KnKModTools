namespace KnKModTools.DatClass.Decomplie
{
    public class RunCmdHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 36;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            // 参数结构：[命令码, 命令码, 参数数]
            var cmd1 = (byte)instr.Operands[0];
            var cmd2 = (byte)instr.Operands[1]; ;
            int paramCount = (byte)instr.Operands[2];

            var args = new List<ExpressionNode>();
            for (var i = 0; i < paramCount; i++)
            {
                args.Add(ctx.EvalStack.GetElementAtIndex(i + 1));
            }

            var value = paramCount == 0 ? "" : ", ";

            return new ExpressionNode()
            {
                Expression =
                $"Engine.Run([{cmd1}, {cmd2}]{value}{string.Join(", ", args)})"
            };
        }
    }
}