namespace KnKModTools.TblClass
{
    public class EvTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EventGroupData")]
        public EventGroupData[] EventGroupDatas { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EventSubGroupData")]
        public EventSubGroupData[] EventSubGroupDatas { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EventTableData")]
        public EventTableData[] EventTableDatas { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class EventGroupData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class EventSubGroupData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class EventTableData
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
        public long Long1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text4 { get; set; }

        [FieldIndexAttr(6)]
        public long Long2 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text5 { get; set; }

        [FieldIndexAttr(8)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(9)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text6 { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        public long Array { get; set; }

        [FieldIndexAttr(12)]
        public long Count { get; set; }

        [FieldIndexAttr(13)]
        public long Long3 { get; set; }

        [FieldIndexAttr(14)]
        public long Long4 { get; set; }

        [FieldIndexAttr(15)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(16)]
        public long Long5 { get; set; }
    }
}