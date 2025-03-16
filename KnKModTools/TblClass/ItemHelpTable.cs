namespace KnKModTools.TblClass
{
    public class ItemHelpTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConditionHelpData")]
        public ConditionHelpData[] ConditionHelpList { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillEffectTypeHelpData")]
        public SkillEffectTypeHelpData[] SkillEffectTypeList { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillEffectHelpData")]
        public SkillEffectHelpData[] SkillEffectList { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "AttrTypeHelpData")]
        public AttrTypeHelpData[] AttrTypeList { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillTypeHelpData")]
        public SkillTypeHelpData[] SkillTypeList { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillRangeHelpData")]
        public SkillRangeHelpData[] SkillRangeList { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ItemKindHelpData")]
        public ItemKindHelpData[] ItemKindList { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillItemStatusData")]
        public SkillItemStatusData[] SkillItemStatusList { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EquipLineHelpData")]
        public EquipLineHelpData[] EquipLineList { get; set; }

        [FieldIndexAttr(12)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillConnectListData")]
        public SkillConnectListData[] SkillConnectList { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillTextArrayData")]
        public SkillTextArrayData[] SkillTextArray { get; set; }

        [FieldIndexAttr(14)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillRangeData")]
        public SkillRangeData[] SkillRangeData { get; set; }

        [FieldIndexAttr(15)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelHelpData")]
        public SkillLevelHelpData[] SkillLevelList { get; set; }

        public static DataPoolManager SManager;
    }

    public class ConditionHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ItemId")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset2 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset3 { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class SkillEffectTypeHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class SkillEffectHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        [UIDisplay("UnknownArray")]
        public long UnknownOffset { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Count")]
        public long Count { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Color")]
        public long ColorOffset { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset3 { get; set; }

        [FieldIndexAttr(8)]
        public uint Unknown2 { get; set; }

        [FieldIndexAttr(9)]
        public uint Unknown3 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("SubName")]
        public long TextOffset4 { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset5 { get; set; }

        public override string ToString()
        {
            var n = TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
            if (n == "" || n == base.ToString())
            {
                n = TBL.GetText(ItemHelpTable.SManager, this, "TextOffset4", base.ToString());
            }
            return n;
        }
    }

    public class SkillEffectHelpDataAdd : SkillEffectHelpData
    {
        public string DisPlayName { get; set; }

        public SkillEffectHelpDataAdd(long id, string name)
        {
            ID = id;
            DisPlayName = name;
        }

        public override string ToString()
        {
            return DisPlayName;
        }
    }

    public class AttrTypeHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Icon", true, "Icon.icons")]
        public long IconId { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class SkillTypeHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class SkillRangeHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("RangeType", true, "Icon.icons")]
        public long RangeType { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Color")]
        public long ColorOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class ItemKindHelpData
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("SubId")]
        public ushort SubId1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("SubId")]
        public uint SubId2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("TextShowType")]
        public long TextShowType { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "Name", base.ToString());
        }
    }

    public class SkillItemStatusData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset2 { get; set; }

        [FieldIndexAttr(4)]
        public long Unknown { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Color")]
        public long ColorOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class EquipLineHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset2 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Color")]
        public long ColorOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    public class SkillConnectListData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        /// <summary>
        /// Item的偏移地址，类型为ushort
        /// </summary>
        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count")]
        [UIDisplay("UnknownArray")]
        public long ItemOffset { get; set; }

        /// <summary>
        /// Item的数量
        /// </summary>
        [FieldIndexAttr(2)]
        [UIDisplay("Count")]
        public long Count { get; set; }
    }

    public class SkillTextArrayData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Color")]
        public long ColorOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }

    /// <summary>
    /// 技能范围数据,对于4个Unkmown属性，它们联合起来构成三种不同的技能范围，分别是扇形、圆形、直线
    /// </summary>
    public class SkillRangeData
    {
        [FieldIndexAttr(0)]
        [UIDisplay("SkillRangeId")]
        public uint RangeId { get; set; }

        [FieldIndexAttr(1)]
        public float Unkmown1 { get; set; }

        [FieldIndexAttr(2)]
        public float Unkmown2 { get; set; }

        [FieldIndexAttr(3)]
        public float Unkmown3 { get; set; }

        [FieldIndexAttr(4)]
        public float Unkmown4 { get; set; }
    }

    public class SkillLevelHelpData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long NameOffset { get; set; }

        /// <summary>
        /// 动态字符串值，d%或+d%等
        /// </summary>
        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TextOffset { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ItemHelpTable.SManager, this, "NameOffset", base.ToString());
        }
    }
}