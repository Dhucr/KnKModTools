namespace KnKModTools.TblClass
{
    public class HelpTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HelpTableData")]
        public HelpTableData[] HelpTableDatas { get; set; }

        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "RealTimeHelpTableData")]
        public RealTimeHelpTableData[] RealTimeHelpTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class HelpTableData
    {
        [FieldIndexAttr(0)]
        public short ID { get; set; }

        [FieldIndexAttr(1)]
        public short Short1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TabName { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long EmptyTextOffset { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(8)]
        public long Long2 { get; set; }
    }

    public class RealTimeHelpTableData
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(3)]
        public float Float { get; set; }

        [FieldIndexAttr(4)]
        public uint Uint2 { get; set; }
    }
}