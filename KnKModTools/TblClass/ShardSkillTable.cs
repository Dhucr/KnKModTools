namespace KnKModTools.TblClass
{
    public class ShardSkillTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ShardSkillParam")]
        public ShardSkillParam[] ShardSkillParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ShardSkillParam
    {
        [FieldIndexAttr(0)]
        public ushort Id { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Type", true, "ShardSkillType.Id")]
        public byte EffectType { get; set; }

        [FieldIndexAttr(2)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("EarthRequirement")]
        public byte ElementEarth { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("WaterRequirement")]
        public byte ElementWater { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("FireRequirement")]
        public byte ElementFire { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("WindRequirement")]
        public byte ElementWind { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("TimeRequirement")]
        public byte ElementTime { get; set; }

        [FieldIndexAttr(8)]
        [UIDisplay("SpaceRequirement")]
        public byte ElementSpace { get; set; }

        [FieldIndexAttr(9)]
        [UIDisplay("MirageRequirement")]
        public byte ElementMirage { get; set; }

        [FieldIndexAttr(10)]
        [UIDisplay("BaseChance")]
        public byte ActivationChance { get; set; }

        [FieldIndexAttr(11)]
        [UIDisplay("SclmBaseChance")]
        public byte SclmActivationChance { get; set; }

        [FieldIndexAttr(12)]
        [UIDisplay("SboostChance")]
        public byte SboostActivationChance { get; set; }

        [FieldIndexAttr(13)]
        [UIDisplay("SclmSboostChance")]
        public byte SboostSclmActivationChance { get; set; }

        [FieldIndexAttr(14)]
        [UIDisplay("FullBoostChance")]
        public byte FullBoostActivationChance { get; set; }

        [FieldIndexAttr(15)]
        public byte Empty { get; set; }

        [FieldIndexAttr(16)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(17)]
        [UIDisplay("UpgradeId", true, "ShardSkillParam.Id")]
        public ushort UpgradeId { get; set; }

        [FieldIndexAttr(18)]
        public uint Empty2 { get; set; }

        [FieldIndexAttr(19)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count")]
        public long ArrayFlag { get; set; }

        [FieldIndexAttr(20)]
        public long Count { get; set; }

        [FieldIndexAttr(21)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag2 { get; set; }

        [FieldIndexAttr(22)]
        [UIDisplay("Condition")]
        public uint ActivationCondition1 { get; set; }

        [FieldIndexAttr(23)]
        [UIDisplay("Condition")]
        public uint ActivationCondition2 { get; set; }

        [FieldIndexAttr(24)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(25)]
        public uint Empty4 { get; set; }

        [FieldIndexAttr(26)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(27)]
        [UIDisplay("SkillSubstituteId", true, "SkillParam.Id")]
        public ushort SkillSubstituteId { get; set; }

        [FieldIndexAttr(28)]
        [BinStreamAttr(Length = 2)]
        [UIDisplay("Effect")]
        public ShardSkillEffect[] Effects { get; set; } // Assuming ShardSkillEffect is a custom type

        [FieldIndexAttr(29)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(31)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Animation")]
        public long Animation { get; set; }

        [FieldIndexAttr(32)]
        [UIDisplay("Icon", true, "Icon.sicon00")]
        public ulong IconId { get; set; }

        [FieldIndexAttr(33)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(34)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ShardSkillTable.SManager, this, "Name", base.ToString());
        }
    }

    public class ShardSkillType
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}