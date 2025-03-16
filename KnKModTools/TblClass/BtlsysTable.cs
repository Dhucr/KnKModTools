namespace KnKModTools.TblClass
{
    public class BtlsysTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ATBonusParam")]
        public ATBonusParam[] ATBonusParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ATBonusSet")]
        public ATBonusSet[] ATBonusSets { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BattleLevelField")]
        public BattleLevelField[] BattleLevelFields { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BattleLevelTurn")]
        public BattleLevelTurn[] BattleLevelTurns { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BattleEnemyLevelStatusAdjust")]
        public BattleEnemyLevelStatusAdjust[] BattleEnemyLevelStatusAdjusts { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BattleEnemyLevelAdjust")]
        public BattleEnemyLevelAdjust[] BattleEnemyLevelAdjusts { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TacticalBonus")]
        public TacticalBonus[] TacticalBonuses { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ATBonusParam
    {
        [FieldIndexAttr(0)]
        public byte ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Unknown")]
        public byte ReferenceID { get; set; }

        [FieldIndexAttr(2)]
        public int Int1 { get; set; }

        [FieldIndexAttr(3)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(5)]
        public int Int2 { get; set; }

        [FieldIndexAttr(6)]
        public int Int3 { get; set; }

        [FieldIndexAttr(7)]
        public int Int4 { get; set; }

        [FieldIndexAttr(8)]
        public int Int5 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("AtBonusName")]
        public long AtBonusName { get; set; }
    }

    public class ATBonusSet
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("PlayerOrFoeValue")]
        public byte PlayerOrFoeValue { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("GeneralAtBonusChance")]
        public byte GeneralAtBonusChance { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("HpHealSBonusChance")]
        public byte HpHealSBonusChance { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("EpHealSBonusChance")]
        public byte EpHealSBonusChance { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("CpHealSBonusChance")]
        public byte CpHealSBonusChance { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("HpHealLBonusChance")]
        public byte HpHealLBonusChance { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("EpHealLBonusChance")]
        public byte EpHealLBonusChance { get; set; }

        [FieldIndexAttr(8)]
        [UIDisplay("CpHealLBonusChance")]
        public byte CpHealLBonusChance { get; set; }

        [FieldIndexAttr(9)]
        [UIDisplay("UnknownBonusChance1")]
        public byte UnknownBonusChance1 { get; set; }

        [FieldIndexAttr(10)]
        [UIDisplay("UnknownBonusChance2")]
        public byte UnknownBonusChance2 { get; set; }

        [FieldIndexAttr(11)]
        [UIDisplay("UnknownBonusChance3")]
        public byte UnknownBonusChance3 { get; set; }

        [FieldIndexAttr(12)]
        [UIDisplay("UnknownBonusChance4")]
        public byte UnknownBonusChance4 { get; set; }

        [FieldIndexAttr(13)]
        [UIDisplay("UnknownBonusChance5")]
        public byte UnknownBonusChance5 { get; set; }

        [FieldIndexAttr(14)]
        [UIDisplay("UnknownBonusChance6")]
        public byte UnknownBonusChance6 { get; set; }

        [FieldIndexAttr(15)]
        [UIDisplay("UnknownBonusChance7")]
        public byte UnknownBonusChance7 { get; set; }

        [FieldIndexAttr(16)]
        [UIDisplay("UnknownBonusChance8")]
        public byte UnknownBonusChance8 { get; set; }

        [FieldIndexAttr(17)]
        [UIDisplay("UnknownBonusChance9")]
        public byte UnknownBonusChance9 { get; set; }

        [FieldIndexAttr(18)]
        [UIDisplay("UnknownBonusChance10")]
        public byte UnknownBonusChance10 { get; set; }

        [FieldIndexAttr(19)]
        [UIDisplay("UnknownBonusChance11")]
        public byte UnknownBonusChance11 { get; set; }

        [FieldIndexAttr(20)]
        public byte Byte1 { get; set; }
    }

    public class BattleLevelField
    {
        [FieldIndexAttr(0)]
        [UIDisplay("DifficultyID")]
        public uint DifficultyID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("PlayerDamageMultiplier")]
        public uint PlayerDamageMultiplier { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("EnemyDamageMultiplier")]
        public uint EnemyDamageMultiplier { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("PlayerStunDamageMultiplier")]
        public float PlayerStunDamageMultiplier { get; set; }
    }

    public class BattleLevelTurn
    {
        [FieldIndexAttr(0)]
        [UIDisplay("DifficultyID")]
        public uint DifficultyID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("PlayerDamageMultiplier")]
        public uint PlayerDamageMultiplier { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("EnemyDamageMultiplier")]
        public uint EnemyDamageMultiplier { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("HpOrUnknownMulti1")]
        public uint HpOrUnknownMulti1 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("HpOrUnknownMulti2")]
        public uint HpOrUnknownMulti2 { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("EnemySpdMultiplier")]
        public uint EnemySpdMultiplier { get; set; }

        [FieldIndexAttr(6)]
        public uint Int1 { get; set; }
    }

    public class BattleEnemyLevelStatusAdjust
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }

        [FieldIndexAttr(3)]
        public int Int3 { get; set; }

        [FieldIndexAttr(4)]
        public int Int4 { get; set; }
    }

    public class BattleEnemyLevelAdjust
    {
        [FieldIndexAttr(0)]
        public long Long1 { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }
    }

    public class TacticalBonus
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("ExpBonus")]
        public float ExpBonus { get; set; }

        [FieldIndexAttr(2)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(4)]
        public int Empty1 { get; set; }

        [FieldIndexAttr(5)]
        public short Empty2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }
}