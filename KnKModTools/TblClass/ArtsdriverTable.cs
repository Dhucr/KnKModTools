namespace KnKModTools.TblClass
{
    public class ArtsdriverTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SlotOpenRate")]
        public SlotOpenRate[] SlotOpenRates { get; set; }

        [FieldIndexAttr(4)]
        public DriverTableData[] DriverTableDatas { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class DriverTableData
    {
        [FieldIndexAttr(0)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "DriverBaseTableData")]
        public DriverBaseTableData[] DriverBaseTableDatas { get; set; }

        [FieldIndexAttr(1)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "DriverArtsTableData")]
        public DriverArtsTableData[] DriverArtsTableDatas { get; set; }
    }

    public class SlotOpenRate
    {
        [FieldIndexAttr(0)]
        public ushort Rate1 { get; set; }

        [FieldIndexAttr(1)]
        public ushort Rate2 { get; set; }

        [FieldIndexAttr(2)]
        public ushort Rate3 { get; set; }

        [FieldIndexAttr(3)]
        public ushort Rate4 { get; set; }

        [FieldIndexAttr(4)]
        public ushort Rate5 { get; set; }

        [FieldIndexAttr(5)]
        public ushort Rate6 { get; set; }
    }

    public class DriverBaseTableData
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public uint ItemID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Attr", true, "AttrData.Id")]
        public ushort Attr1 { get; set; }

        [FieldIndexAttr(2)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Attr", true, "AttrData.Id")]
        public ushort Attr2 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("CustomSolt")]
        public ushort CustomSolt { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("FixedSolt")]
        public ushort FixedSolt { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("SumSolt")]
        public ushort SumSolt { get; set; }
    }

    public class DriverArtsTableData
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public uint ItemID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("SoltInx")]
        public ushort SoltInx { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("UnLockLevel")]
        public ushort UnLockLevel { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public uint SkillId { get; set; }
    }
}