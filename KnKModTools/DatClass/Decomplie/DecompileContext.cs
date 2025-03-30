using KnKModTools.Helper;

namespace KnKModTools.DatClass.Decomplie
{
    #region 上下文与结构定义

    /// <summary>
    /// 反编译上下文，包含代码生成过程中的各种状态
    /// </summary>
    public class DecompileContext
    {
        public Stack<ExpressionNode> EvalStack;
        public Dictionary<uint, string> LabelMap;
        public Stack<string> PendingClosures;
        public int IndentLevel;
        public int TempVarCounter;
        public Stack<JumpInfo> PendingJumps = new();
        public int LabelCounter;
        public Dictionary<int, string> ResultMap = [];
        public Dictionary<uint, BasicBlock> AddrToBlock = [];
        public List<BasicBlock> Blocks = [];
        public Dictionary<BasicBlock, Stack<ExpressionNode>> BlockStacks = [];
        public BlockNode CurrentNode;

        public LoopContext Loop = new();
        public class LoopContext
        {
            public bool IsInLoop = false;
            public uint WhileAddr;
            public uint BreakAddr;
            public uint EndAddr;

            public void EnterLoop(uint whileAddr, uint breakAddr, 
                uint endAddr, bool isInLoop = true)
            {
                IsInLoop = isInLoop;
                WhileAddr = whileAddr;
                BreakAddr = breakAddr;
                EndAddr = endAddr;
            }

            public void ExitLoop()
            {
                IsInLoop = false;
            }
        }

        public IDisposable CaptureLoopState()
        {
            return new LoopStateCapturer(this);
        }

        private class LoopStateCapturer : IDisposable
        {
            private readonly DecompileContext _ctx;
            private readonly LoopContext _loop;

            public LoopStateCapturer(DecompileContext ctx)
            {
                _ctx = ctx;
                _loop = new LoopContext();
                _loop.EnterLoop(_ctx.Loop.WhileAddr, 
                    _ctx.Loop.BreakAddr, _ctx.Loop.EndAddr, 
                    _ctx.Loop.IsInLoop);
            }

            public void Dispose()
            {
                _ctx.Loop = _loop;
            }
        }

        public IDisposable CaptureStackState()
        {
            return new StackStateCapturer(this);
        }

        private class StackStateCapturer : IDisposable
        {
            private readonly DecompileContext _ctx;
            private readonly Stack<ExpressionNode> _snapshot;

            public StackStateCapturer(DecompileContext ctx)
            {
                _ctx = ctx;
                _snapshot = new Stack<ExpressionNode>(_ctx.EvalStack.Reverse());
            }

            public void Dispose()
            {
                _ctx.EvalStack = new Stack<ExpressionNode>(_snapshot.Reverse());
            }
        }

        public IDisposable CaptureNodeState(BlockNode node)
        {
            return new NodeStateCapturer(this, node);
        }

        private class NodeStateCapturer : IDisposable
        {
            private readonly DecompileContext _ctx;
            private readonly BlockNode _oldNode;

            public NodeStateCapturer(DecompileContext ctx, BlockNode node)
            {
                _ctx = ctx;
                _oldNode = _ctx.CurrentNode;
                _ctx.CurrentNode = node;
            }

            public void Dispose()
            {
                _ctx.CurrentNode = _oldNode;
            }
        }
    }

    #endregion 上下文与结构定义
}