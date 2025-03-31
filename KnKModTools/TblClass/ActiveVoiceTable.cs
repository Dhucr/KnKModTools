namespace KnKModTools.TblClass
{
    public class ActiveVoiceTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ActiveVoiceTableData")]
        public ActiveVoiceTableData[] ActiveVoiceTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class ActiveVoiceTableData
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public short Short1 { get; set; }

        [FieldIndexAttr(2)]
        public short Short2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset1 { get; set; }

        [FieldIndexAttr(4)]
        public long Empty1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset2 { get; set; }

        [FieldIndexAttr(6)]
        public long Long1 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "FlagCount")]
        public long ScenaFlagArray { get; set; }

        [FieldIndexAttr(8)]
        public long FlagCount { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(10)]
        public int Count { get; set; }

        [FieldIndexAttr(11)]
        public int Int1 { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long VoiceSubtitle { get; set; }

        [FieldIndexAttr(13)]
        public long Empty2 { get; set; }
    }
}