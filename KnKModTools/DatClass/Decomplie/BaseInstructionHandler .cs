namespace KnKModTools.DatClass.Decomplie
{
    public abstract class BaseInstructionHandler : IInstructionHandler
    {
        public abstract bool CanHandle(byte opCode);

        public abstract AstNode Handle(DecompileContext ctx, InStruction instr, DecompilerCore core);

        protected void Push(DecompileContext ctx, string expression) =>
            ctx.EvalStack.Push(new ExpressionNode { Expression = expression });

        protected void Push(DecompileContext ctx, ExpressionNode node) =>
            ctx.EvalStack.Push(node);

        protected ExpressionNode Pop(DecompileContext ctx) => ctx.EvalStack.Pop();
    }
}