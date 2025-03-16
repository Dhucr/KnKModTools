namespace KnKModTools.TblClass
{
    public class MarkerTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MarkerTableData")]
        public MarkerTableData[] MarkerTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class MarkerTableData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(5)]
        public int Count2 { get; set; }

        [FieldIndexAttr(6)]
        public short Short1 { get; set; }

        [FieldIndexAttr(7)]
        public short Short2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(9)]
        public int Int1 { get; set; }

        [FieldIndexAttr(10)]
        public float Float1 { get; set; }

        [FieldIndexAttr(11)]
        public float Float2 { get; set; }

        [FieldIndexAttr(12)]
        public float Float3 { get; set; }

        [FieldIndexAttr(13)]
        public float Float4 { get; set; }

        [FieldIndexAttr(14)]
        public float Float5 { get; set; }

        [FieldIndexAttr(15)]
        public float Float6 { get; set; }

        [FieldIndexAttr(16)]
        public float Float7 { get; set; }

        [FieldIndexAttr(17)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }
    }
}