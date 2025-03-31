using Esprima.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.DatClass.Complie
{
    public class CompileContext
    {
        public List<InStruction> InsList = [];

        public Dictionary<string, int> LocalScopes = [];

        public int GetLine(Node node) => node.Location.End.Line;

        public int FunctionId = 0;

        public Function Func = new();

        public Action<int> Pop;
        public bool IsReturn = false;
        private int _stackDepth;
        public int StackDepth
        {
            get => _stackDepth;
            set
            {
                var diff = value - _stackDepth;
                _stackDepth = value;
                foreach (var k in LocalScopes.Keys)
                {
                    LocalScopes[k] += diff;
                    if (LocalScopes[k] <= 0)
                    {
                        LocalScopes.Remove(k);
                    }
                }
            }
        }

        public void SetStackDepthAndPop(int depth)
        {
            var diff = depth - _stackDepth;
            if (diff == 0)
                return;
            Pop(diff);
            //_stackDepth = depth;
        }

        public void SetStackDepth(int depth)
        {
            _stackDepth = depth;
        }

        public void AddLocalVar(string name)
        {
            LocalScopes.Add(name, 1);
        }

        public void StackClear()
        {
            Pop(-StackDepth);
        }

        public LoopContext Loop = new();

        public class LoopContext
        {
            public bool IsInLoop = false;
            public Label StartLabel;
            public Label EndLabel;

            public void EnterLoop(Label startLabel, Label endLabel, bool isInLoop = true)
            {
                IsInLoop = isInLoop;
                StartLabel = startLabel;
                EndLabel = endLabel;
            }

            public void ExitLoop()
            {
                IsInLoop = false;
            }
        }

        public IDisposable DisableLoopState()
        {
            return new LoopStateDisable(this);
        }

        private class LoopStateDisable : IDisposable
        {
            private readonly CompileContext _ctx;
            private readonly bool _loop;

            public LoopStateDisable(CompileContext ctx)
            {
                _ctx = ctx;
                _loop = _ctx.Loop.IsInLoop;
                _ctx.Loop.IsInLoop = false;
            }

            public void Dispose()
            {
                _ctx.Loop.IsInLoop = _loop;
            }
        }

        public IDisposable CaptureLoopState()
        {
            return new LoopStateCapturer(this);
        }

        private class LoopStateCapturer : IDisposable
        {
            private readonly CompileContext _ctx;
            private readonly LoopContext _loop;

            public LoopStateCapturer(CompileContext ctx)
            {
                _ctx = ctx;
                _loop = new LoopContext();
                _loop.EnterLoop(_ctx.Loop.StartLabel, _ctx.Loop.EndLabel, _ctx.Loop.IsInLoop);
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
            private readonly CompileContext _ctx;
            private readonly Dictionary<string, int> _snapshot;
            private readonly int _stackDepth;
            private readonly bool _isReturn;

            public StackStateCapturer(CompileContext ctx)
            {
                _ctx = ctx;
                _isReturn = _ctx.IsReturn;
                _stackDepth = _ctx.StackDepth;
                _snapshot = new Dictionary<string, int>(_ctx.LocalScopes);
            }

            public void Dispose()
            {
                if (_isReturn)
                {
                    _ctx.SetStackDepth(_stackDepth);
                }
                else
                {
                    _ctx.SetStackDepthAndPop(_stackDepth);
                }
                _ctx.IsReturn = _isReturn;
                _ctx.LocalScopes = new Dictionary<string, int>(_snapshot);
            }
        }
    }
}
