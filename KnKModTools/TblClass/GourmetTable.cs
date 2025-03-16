namespace KnKModTools.TblClass
{
    public class GourmetTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GourmetParam")]
        public GourmetParam[] GourmetParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GourmetRankParam")]
        public GourmetRankParam[] GourmetRankParams { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GourmetChr")]
        public GourmetChr[] GourmetChrs { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class GourmetParam
    {
        [FieldIndexAttr(0)]
        public short ID { get; set; }

        [FieldIndexAttr(1)]
        public short Short { get; set; }

        [FieldIndexAttr(2)]
        public int Int1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Description { get; set; }

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(6)]
        public int Int2 { get; set; }

        [FieldIndexAttr(7)]
        public int Int3 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(10)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(11)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(12)]
        public long Long2 { get; set; }
    }

    public class GourmetRankParam
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }

        [FieldIndexAttr(3)]
        public int Int3 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long RankUpText { get; set; }
    }

    public class GourmetChr
    {
        [FieldIndexAttr(0)]
        public short Value { get; set; }
    }
}