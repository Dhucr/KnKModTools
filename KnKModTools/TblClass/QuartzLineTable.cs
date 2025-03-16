namespace KnKModTools.TblClass
{
    public class QuartzLineTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ElementSlotRate")]
        public ElementSlotRate[] ElementSlotRates { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuartzLineParam")]
        public QuartzLineParam[] QuartzLineParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ElementSlotRate
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ElementRestrictMultiply")]
        public float ElementRestrictMultiply { get; set; }
    }

    public class QuartzLineParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public uint CharacterId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("LineType")]
        public uint LineType { get; set; }

        [FieldIndexAttr(2)]
        public int Field1 { get; set; }

        [FieldIndexAttr(3)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(Length = 3)]
        [UIDisplay("QuartzLineSlot")]
        public QuartzLineSlot[] Slots { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("SlotLineNumber")]
        public ushort SlotLineNumber { get; set; }
    }

    public class QuartzLineSlot
    {
        [FieldIndexAttr(0)]
        [UIDisplay("RestrictionId")]
        public uint RestrictionId { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public ushort Short2 { get; set; }
    }
}