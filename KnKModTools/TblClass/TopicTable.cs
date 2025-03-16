namespace KnKModTools.TblClass
{
    public class TopicTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TopicTableData")]
        public TopicTableData[] TopicTableDatas { get; set; }

        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TopicGetCond")]
        public TopicGetCond[] TopicGetConds { get; set; }

        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TopicUseCond")]
        public TopicUseCond[] TopicUseConds { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class TopicTableData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }
    }

    public class TopicGetCond
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(3)]
        public ushort Short3 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(5)]
        public long Count1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(7)]
        public long Count2 { get; set; }
    }

    public class TopicUseCond
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public int Empty { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(6)]
        public long Count1 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(8)]
        public long Count2 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(10)]
        public long Count3 { get; set; }
    }
}