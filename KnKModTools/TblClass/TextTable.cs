namespace KnKModTools.TblClass
{
    public class TextTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TextTableData")]
        public TextTableData[] TextTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class TextTableData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Key { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Value { get; set; }
    }
}