namespace KnKModTools.TblClass
{
    public class LookPointTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "LookPointTableData")]
        public LookPointTableData[] LookPointTableDatas { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class LookPointTableData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(3)]
        public long Long1 { get; set; }

        [FieldIndexAttr(4)]
        public long Long2 { get; set; }

        [FieldIndexAttr(5)]
        public long Long3 { get; set; }

        [FieldIndexAttr(6)]
        public long Long4 { get; set; }

        [FieldIndexAttr(7)]
        public long Long5 { get; set; }
    }
}