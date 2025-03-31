namespace KnKModTools.TblClass
{
    public class BGMTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BGMTableData")]
        public BGMTableData[] BGMTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class BGMTableData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(2)]
        public float Float1 { get; set; }

        [FieldIndexAttr(3)]
        public int Int1 { get; set; }
    }
}