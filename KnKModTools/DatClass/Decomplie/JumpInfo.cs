namespace KnKModTools.DatClass.Decomplie
{
    /// <summary>
    /// 跳转信息记录，用于控制流分析
    /// </summary>
    public class JumpInfo
    {
        public string TargetLabel { get; }
        public string EndLabel { get; }

        public JumpInfo(string target, string end)
        {
            TargetLabel = target;
            EndLabel = end;
        }
    }
}