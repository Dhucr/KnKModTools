namespace KnKModTools.DatClass.Decomplie
{
    /// <summary>
    /// 基本块定义，表示连续执行的指令序列
    /// </summary>
    public class BasicBlock
    {
        public uint StartAddr;
        public uint EndAddr;
        public BlockType Type;
        public List<InStruction> Instructions = [];
        public List<BasicBlock> TrueSuccessors = [];
        public List<BasicBlock> FalseSuccessors = [];
        public bool Visited;
        public bool IsLoopHeader;
    }

    public enum BlockType
    { Base, IFFlase, IFTrue, Jump, Exit }
}