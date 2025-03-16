namespace KnKModTools.TblClass
{
    public class EventBoxTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EventBoxTableData")]
        public EventBoxTableData[] EventBoxTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class EventBoxTableData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("EventName")]
        public long EventName { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(4)]
        public long Count1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        [UIDisplay("Name")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(6)]
        public long Count2 { get; set; }
    }
}