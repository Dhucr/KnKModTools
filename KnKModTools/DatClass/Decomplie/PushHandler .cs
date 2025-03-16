namespace KnKModTools.DatClass.Decomplie
{
    public class PushHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 0;

        public override AstNode? Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var value = core.FormatValue(instr.Operands[1]);
            InStruction ins = core.GetNextIns(ctx, instr.Offset);
            if (value.Equals("0x0") && ins != null && ins.Code != 10)
            {
                value = $"_temp{ctx.TempVarCounter++}";
            }
            Push(ctx, value);

            return value.Contains("_temp") ?
                new ExpressionNode() { Expression = $"let {value} = null" } : null;
        }
    }
}