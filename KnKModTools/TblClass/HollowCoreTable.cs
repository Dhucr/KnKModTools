namespace KnKModTools.TblClass
{
    public class HollowCoreTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreBaseParam")]
        public HollowCoreBaseParam[] HollowCoreBaseParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreLevelParam")]
        public HollowCoreLevelParam[] HollowCoreLevelParams { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreEffParam")]
        public HollowCoreEffParam[] HollowCoreEffParams { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreEffText")]
        public HollowCoreEffText[] HollowCoreEffTexts { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreConvertLevelParam")]
        public HollowCoreConvertLevelParam[] HollowCoreConvertLevelParams { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreCalcLevelParam")]
        public HollowCoreCalcLevelParam[] HollowCoreCalcLevelParams { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "HollowCoreVoice")]
        public HollowCoreVoice[] HollowCoreVoices { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class HollowCoreBaseParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public uint ItemId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("InitialLevel")]
        public uint InitialLevel { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("MaxLevel")]
        public uint MaxLevel { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(4)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(5)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("SBoostAbilityType")]
        public ulong SBoostAbilityType { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("SBoostAbilityName")]
        public long SBoostAbilityName { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Array1 { get; set; }

        [FieldIndexAttr(10)]
        public uint Count1 { get; set; }

        [FieldIndexAttr(11)]
        [UIDisplay("VoiceId")]
        public uint VoiceId { get; set; }

        [FieldIndexAttr(12)]
        public float Float1 { get; set; }

        [FieldIndexAttr(13)]
        public float Float2 { get; set; }

        [FieldIndexAttr(14)]
        public float Float3 { get; set; }

        [FieldIndexAttr(15)]
        public uint Empty2 { get; set; }

        [FieldIndexAttr(16)]
        public float Float4 { get; set; }

        [FieldIndexAttr(17)]
        public int Empty3 { get; set; }

        [FieldIndexAttr(18)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Array2 { get; set; }

        [FieldIndexAttr(19)]
        public long Count2 { get; set; }

        [FieldIndexAttr(20)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }
    }

    public class HollowCoreLevelParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public uint ItemId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Level")]
        public uint Level { get; set; }

        [FieldIndexAttr(2)]
        public int Exp { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("MagicAttack")]
        public uint MagicAttack { get; set; }

        [FieldIndexAttr(4)]
        public uint Ep { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(7)]
        [UIDisplay("Effect")]
        public HollowCoreEffect[] Effects { get; set; } // Array of 7 elements

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }
    }

    public class HollowCoreEffParam
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("IdReference")]
        public uint IdReference { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("ParamIndex")]
        public ulong Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("EffectName")]
        public long EffectName { get; set; }
    }

    public class HollowCoreEffText
    {
        [FieldIndexAttr(0)]
        public ulong Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("EffectDescription")]
        public long EffectDescription { get; set; }

        public override string ToString()
        {
            var str = TBL.GetText(HollowCoreTable.SManager, this, "EffectDescription", base.ToString());
            var inx = str.LastIndexOf('>');
            if (inx > -1)
            {
                str = str.Substring(inx + 1);
            }
            return str;
        }
    }

    public class HollowCoreConvertLevelParam
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Value { get; set; }
    }

    public class HollowCoreCalcLevelParam
    {
        [FieldIndexAttr(0)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(1)]
        public float Float1 { get; set; }

        [FieldIndexAttr(2)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(3)]
        public float Float2 { get; set; }

        [FieldIndexAttr(4)]
        public float Float3 { get; set; }
    }

    public class HollowCoreVoice
    {
        [FieldIndexAttr(0)]
        public short Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Number { get; set; }

        [FieldIndexAttr(2)]
        public ushort Empty { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        [UIDisplay("VoiceArray")]
        public long Array { get; set; }

        [FieldIndexAttr(4)]
        public ulong Count { get; set; }
    }
}