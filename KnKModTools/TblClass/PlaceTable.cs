namespace KnKModTools.TblClass
{
    public class PlaceTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "PlaceTableData")]
        public PlaceTableData[] PlaceTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class PlaceTableData
    {
        [FieldIndexAttr(0)]
        public ulong ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(3)]
        public float Float1 { get; set; }

        [FieldIndexAttr(4)]
        public float Float2 { get; set; }

        [FieldIndexAttr(5)]
        public float Float3 { get; set; }

        [FieldIndexAttr(6)]
        public uint Empty1 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long ScenaFlagArr1 { get; set; }

        [FieldIndexAttr(8)]
        public long Count1 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long ScenaFlagArr2 { get; set; }

        [FieldIndexAttr(10)]
        public int Count2 { get; set; }

        [FieldIndexAttr(11)]
        public float Float6 { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(14)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text5 { get; set; }

        [FieldIndexAttr(15)]
        public float Float7 { get; set; }

        [FieldIndexAttr(16)]
        public float Float8 { get; set; }

        [FieldIndexAttr(17)]
        public float Float9 { get; set; }

        [FieldIndexAttr(18)]
        public uint Empty2 { get; set; }
    }
}