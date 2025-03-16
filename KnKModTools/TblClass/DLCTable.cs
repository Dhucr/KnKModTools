namespace KnKModTools.TblClass
{
    public class DLCTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "DLCTableData")]
        public DLCTableData[] DLCTableDatas { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class DLCTableData
    {
        [FieldIndexAttr(0)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(1)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(5)]
        public long Count2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }
}