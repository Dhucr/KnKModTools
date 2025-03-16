namespace KnKModTools.TblClass
{
    public class ChapterTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ChapterParam")]
        public ChapterParam[] ChapterParams { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class ChapterParam
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
        public long Long1 { get; set; }

        [FieldIndexAttr(5)]
        public long Long2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Title")]
        public long Text1 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text4 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text5 { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text6 { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text7 { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text8 { get; set; }

        [FieldIndexAttr(14)]
        public long Long3 { get; set; }

        [FieldIndexAttr(15)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Empty1")]
        public long Arr { get; set; }

        [FieldIndexAttr(16)]
        public long Empty1 { get; set; }

        [FieldIndexAttr(17)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Empty2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(18)]
        public long Empty2 { get; set; }

        [FieldIndexAttr(19)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text9 { get; set; }

        [FieldIndexAttr(20)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text10 { get; set; }
    }
}