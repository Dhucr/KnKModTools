namespace KnKModTools.DatClass.Decomplie
{
    public class CallHandler : BaseInstructionHandler
    {
        public override bool CanHandle(byte opCode) => opCode == 12;

        public override AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core)
        {
            var funcId = Convert.ToInt32(instr.Operands[0]);
            var paramCount = core.GetParamCountFromMetadata(funcId);

            var args = new List<ExpressionNode>();
            for (var i = 0; i < paramCount; i++)
                args.Add(Pop(ctx));

            Pop(ctx); // 返回地址
            CodeFormatting.TempVar(ctx, Pop(ctx));

            var ex = $"{core.GetFunctionName(funcId)}({string.Join(", ", args)})";
            ex = CodeFormatting.SimplifyConditions(ctx, ex);
            return new ExpressionNode
            {
                Expression = ex
            };
        }
    }
}