namespace KnKModTools.TblClass
{
    public class SkillTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillParam")]
        public SkillParam[] SkillParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillPowerIcon")]
        public SkillPowerIcon[] SkillPowerIcons { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillGetParam")]
        public SkillGetParam[] SkillGetParams { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillChangeParam")]
        public SkillChangeParam[] SkillChangeParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class SkillParam
    {
        [FieldIndexAttr(0)]
        public ushort Id { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("CharLimit", true, "NameTableData.CharacterId")]
        public int CharacterRestriction { get; set; }

        [FieldIndexAttr(2)]
        public short Short { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("Type", true, "SkillTypeHelpData.ID")]
        public byte Category { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("Attr", true, "AttrData.Id")]
        public byte Element { get; set; }

        [FieldIndexAttr(6)]
        public int Empty1 { get; set; }

        [FieldIndexAttr(7)]
        public short Empty2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag2 { get; set; }

        [FieldIndexAttr(11)]
        [UIDisplay("RangeType", true, "RangeTypeTable.Id")]
        public int RangeType { get; set; }

        [FieldIndexAttr(12)]
        [UIDisplay("RangeMove")]
        public float RangeMove { get; set; }

        [FieldIndexAttr(13)]
        [UIDisplay("RangeAttack")]
        public float RangeAttack { get; set; }

        [FieldIndexAttr(14)]
        [UIDisplay("RangeAngle")]
        public float RangeAngle { get; set; }

        [FieldIndexAttr(15)]
        [BinStreamAttr(Length = 5)]
        [UIDisplay("Effect")]
        public Effect[] Effects { get; set; } // Assuming Effect is a custom type

        [FieldIndexAttr(16)]
        [UIDisplay("StunChance")]
        public float StunChance { get; set; }

        [FieldIndexAttr(17)]
        [UIDisplay("CastDelay")]
        public ushort CastDelay { get; set; }

        [FieldIndexAttr(18)]
        [UIDisplay("RecoveryDelay")]
        public ushort RecoveryDelay { get; set; }

        [FieldIndexAttr(19)]
        [UIDisplay("Cost")]
        public ushort Cost { get; set; }

        [FieldIndexAttr(20)]
        [UIDisplay("LevelLearn")]
        public short LevelLearn { get; set; }

        [FieldIndexAttr(21)]
        public ushort SortId { get; set; }

        [FieldIndexAttr(22)]
        public short Data { get; set; }

        [FieldIndexAttr(23)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Animation")]
        public long Animation { get; set; }

        [FieldIndexAttr(24)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(25)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description1 { get; set; }

        [FieldIndexAttr(26)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description2 { get; set; }

        public override string ToString()
        {
            return TBL.GetText(SkillTable.SManager, this, "Name", base.ToString());
        }
    }

    public class RangeTypeTable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RangeTypeTable(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SkillPowerIcon
    {
        [FieldIndexAttr(0)]
        [UIDisplay("Percentage")]
        public int SkillPower { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Icon", true, "Icon.icons")]
        public int IconId { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Text")]
        public long PowerText { get; set; }
    }

    public class SkillGetParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public ushort CharacterId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId2 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId3 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId4 { get; set; }
    }

    public class SkillChangeParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public ushort CharacterId { get; set; }

        [FieldIndexAttr(1)]
        public short Data { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId1 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId2 { get; set; }
    }
}