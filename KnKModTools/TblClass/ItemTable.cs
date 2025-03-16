namespace KnKModTools.TblClass
{
    public class ItemTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ItemTableData")]
        public ItemTableData[] Items { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ItemKindParam2")]
        public ItemKindParam2[] KindParams { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ItemTabType")]
        public ItemTabType[] TabTypes { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuartzParam")]
        public QuartzParam[] QuartzParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ItemTableData
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("CharLimit", true, "NameTableData.CharacterId")]
        public uint CharLimit { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOff1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOff2 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("ItemKind")]
        public byte ItemKind { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("ItemSubKind", true, "ItemKindParam2.ID")]
        public byte SubItemKind { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("ItemIcon", true, "Icon.icons")]
        public ushort ItemIcon { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("EffectIcon", true, "Icon.icons")]
        public ushort EffectIcon { get; set; }

        [FieldIndexAttr(8)]
        [UIDisplay("Attr", true, "AttrData.Id")]
        public ushort Attr { get; set; }

        [FieldIndexAttr(9)]
        public ushort Unknown { get; set; }

        [FieldIndexAttr(10)]
        public ushort Unknown1 { get; set; }

        [FieldIndexAttr(11)]
        public float Unknown2 { get; set; }

        [FieldIndexAttr(12)]
        public float Unknown3 { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(Length = 5)]
        [UIDisplay("Effect")]
        public Effect[] Effects { get; set; }

        [FieldIndexAttr(14)]
        public float Unknown4 { get; set; }

        [FieldIndexAttr(15)]
        public uint HP { get; set; }

        [FieldIndexAttr(16)]
        public uint EP { get; set; }

        [FieldIndexAttr(17)]
        [UIDisplay("PhysicalAttack")]
        public uint PhysicalAttack { get; set; }

        [FieldIndexAttr(18)]
        [UIDisplay("PhysicalDefense")]
        public uint PhysicalDefense { get; set; }

        [FieldIndexAttr(19)]
        [UIDisplay("MagicAttack")]
        public uint MagicAttack { get; set; }

        [FieldIndexAttr(20)]
        [UIDisplay("MagicDefense")]
        public uint MagicDefense { get; set; }

        [FieldIndexAttr(21)]
        public uint STR { get; set; }

        [FieldIndexAttr(22)]
        public uint DEF { get; set; }

        [FieldIndexAttr(23)]
        public uint AST { get; set; }

        [FieldIndexAttr(24)]
        public uint ADF { get; set; }

        [FieldIndexAttr(25)]
        public uint AGL { get; set; }

        [FieldIndexAttr(26)]
        public uint DEX { get; set; }

        [FieldIndexAttr(27)]
        [UIDisplay("Accuracy")]
        public uint Accuracy { get; set; }

        [FieldIndexAttr(28)]
        [UIDisplay("Dodge")]
        public uint Dodge { get; set; }

        [FieldIndexAttr(29)]
        [UIDisplay("MagicDodge")]
        public uint MagicDodge { get; set; }

        [FieldIndexAttr(30)]
        [UIDisplay("Critical")]
        public uint Critical { get; set; }

        [FieldIndexAttr(31)]
        public uint SPD { get; set; }

        [FieldIndexAttr(32)]
        public uint MOV { get; set; }

        [FieldIndexAttr(33)]
        [UIDisplay("UpperLimit")]
        public uint UpperLimit { get; set; }

        [FieldIndexAttr(34)]
        [UIDisplay("Price")]
        public uint Price { get; set; }

        [FieldIndexAttr(35)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Animation")]
        public long AnimationOff { get; set; }

        [FieldIndexAttr(36)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOff { get; set; }

        [FieldIndexAttr(37)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long DescriptionOff { get; set; }

        [FieldIndexAttr(38)]
        public uint Unknown5 { get; set; }

        [FieldIndexAttr(39)]
        public uint Unknown6 { get; set; }

        [FieldIndexAttr(40)]
        public uint Unknown7 { get; set; }

        [FieldIndexAttr(41)]
        public uint Unknown8 { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemTable.SManager, this, "NameOff", base.ToString());
        }
    }

    public class ItemTableEx : ItemTableData
    {
        public string DisplayName { get; set; }

        public ItemTableEx(uint id, string name)
        {
            ID = id;
            DisplayName = name;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public class ItemKindParam2
    {
        [FieldIndexAttr(0)]
        public ulong ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("KindName")]
        public long KindTextOff { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemTable.SManager, this, "KindTextOff", base.ToString());
        }
    }

    public class ItemTabType
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Unknown2 { get; set; }

        [FieldIndexAttr(2)]
        public int Unknown3 { get; set; }
    }

    public class QuartzParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public ushort ItemID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("EarthCost")]
        public ushort EarthCost { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("WaterCost")]
        public ushort WaterCost { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("FireCost")]
        public ushort FireCost { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("WindCost")]
        public ushort WindCost { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("TimeCost")]
        public ushort TimeCost { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("SpaceCost")]
        public ushort SpaceCost { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("MirageCost")]
        public ushort MirageCost { get; set; }

        [FieldIndexAttr(8)]
        [UIDisplay("EarthAmount")]
        public byte EarthAmount { get; set; }

        [FieldIndexAttr(9)]
        [UIDisplay("WaterAmount")]
        public byte WaterAmount { get; set; }

        [FieldIndexAttr(10)]
        [UIDisplay("FireAmount")]
        public byte FireAmount { get; set; }

        [FieldIndexAttr(11)]
        [UIDisplay("WindAmount")]
        public byte WindAmount { get; set; }

        [FieldIndexAttr(12)]
        [UIDisplay("TimeAmount")]
        public byte TimeAmount { get; set; }

        [FieldIndexAttr(13)]
        [UIDisplay("SpaceAmount")]
        public byte SpaceAmount { get; set; }

        [FieldIndexAttr(14)]
        [UIDisplay("MirageAmount")]
        public byte MirageAmount { get; set; }

        [FieldIndexAttr(15)]
        public byte Unknown1 { get; set; }

        [FieldIndexAttr(16)]
        public uint Unknown2 { get; set; }
    }
}