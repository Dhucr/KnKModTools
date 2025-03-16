namespace KnKModTools.TblClass
{
    public class TipsTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TipsTableData")]
        public TipsTableData[] TipsTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class TipsTableData
    {
        [FieldIndexAttr(0)]
        public long Long1 { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(2)]
        public long Count { get; set; }

        [FieldIndexAttr(3)]
        public long ID { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }
}