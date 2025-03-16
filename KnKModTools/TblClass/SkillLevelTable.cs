namespace KnKModTools.TblClass
{
    public class SkillLevelTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelConstant")]
        public SkillLevelConstant[] SkillLevelConstants { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelExpData")]
        public SkillLevelExpData[] SkillLevelExpDatas { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelExpCorrectData")]
        public SkillLevelExpCorrectData[] SkillLevelExpCorrectDatas { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelParam")]
        public SkillLevelParam[] SkillLevelParams { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelSkillSetting")]
        public SkillLevelSkillSetting[] SkillLevelSkillSettings { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillLevelFactorDefine")]
        public SkillLevelFactorDefine[] SkillLevelFactorDefines { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class SkillLevelConstant
    {
        [FieldIndexAttr(0)]
        public int Int1 { get; set; }
    }

    public class SkillLevelExpData
    {
        [FieldIndexAttr(0)]
        public int Int1 { get; set; }

        [FieldIndexAttr(1)]
        public int Int2 { get; set; }
    }

    public class SkillLevelExpCorrectData
    {
        [FieldIndexAttr(0)]
        public int Int1 { get; set; }

        [FieldIndexAttr(1)]
        public int Int2 { get; set; }

        [FieldIndexAttr(2)]
        public int Int3 { get; set; }
    }

    public class SkillLevelParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public ushort ChrId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId { get; set; }

        [FieldIndexAttr(2)]
        public ushort UpgradeTypeId { get; set; }

        [FieldIndexAttr(3)]
        public short Lv1Param { get; set; }

        [FieldIndexAttr(4)]
        public short Lvl2Param { get; set; }

        [FieldIndexAttr(5)]
        public short Lvl3Param { get; set; }

        [FieldIndexAttr(6)]
        public short Lvl4Param { get; set; }

        [FieldIndexAttr(7)]
        public short Lvl5Param { get; set; }

        [FieldIndexAttr(8)]
        public short Lvl6Param { get; set; }

        [FieldIndexAttr(9)]
        public short Lvl7Param { get; set; }

        [FieldIndexAttr(10)]
        public short Lvl8Param { get; set; }

        [FieldIndexAttr(11)]
        public short Lvl9Param { get; set; }

        [FieldIndexAttr(12)]
        public short Lvl10Param { get; set; }
    }

    public class SkillLevelSkillSetting
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public ushort ChrId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("SkillId", true, "SkillParam.Id")]
        public ushort SkillId { get; set; }

        [FieldIndexAttr(2)]
        public ushort Empty1 { get; set; }

        [FieldIndexAttr(3)]
        public ushort Empty2 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset2 { get; set; }
    }

    public class SkillLevelFactorDefine
    {
        [FieldIndexAttr(0)]
        public ulong Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Int1")]
        public long Array1 { get; set; }

        [FieldIndexAttr(6)]
        public int Int1 { get; set; }

        [FieldIndexAttr(7)]
        public int Int2 { get; set; }

        [FieldIndexAttr(8)]
        public long Long2 { get; set; }
    }
}