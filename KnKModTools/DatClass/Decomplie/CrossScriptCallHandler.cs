namespace KnKModTools.DatClass.Decomplie
{
    public class CrossScriptCallHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode is 34 or 35;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var dat = (string)instr.Operands[0];
            var script = dat is "" ? "All" : dat;
            var func = (string)instr.Operands[1];
            int paramCount = (byte)instr.Operands[2];

            var args = new List<ExpressionNode>();
            for (var i = 0; i < paramCount; i++)
                args.Add(Pop(ctx));

            if (instr.Code == 34) // 有返回值的跨脚本调用
            {
                for (var i = 0; i < 5; i++) Pop(ctx); // 清理调用协议
            }

            if (instr.Code == 35)
            {
                script = "sc_" + script;
            }

            return new ExpressionNode
            {
                Expression = $"{script}.{func}({string.Join(", ", args)})"
            };
        }
    }
}