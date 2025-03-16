namespace KnKModTools.TblClass
{
    public class TBoxTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TBoxParam")]
        public TBoxParam[] TBoxParams { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class TBoxParam
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Scena")]
        public long Scena { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(3)]
        public long HackBoxID { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        [UIDisplay("ItemList")]
        public long ItemList { get; set; }

        [FieldIndexAttr(5)]
        public long Count1 { get; set; }

        [FieldIndexAttr(6)]
        public long HackBoxThing1 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long HackBoxThing2 { get; set; }

        [FieldIndexAttr(8)]
        public long Count2 { get; set; }
    }
}