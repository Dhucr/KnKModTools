namespace KnKModTools.TblClass
{
    public class AchievementTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "AchievementTableData")]
        public AchievementTableData[] AchievementTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class AchievementTableData
    {
        [FieldIndexAttr(0)]
        [UIDisplay("AchievementCategory")]
        public uint AchievementCategory { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("AchievementId")]
        public uint AchievementId { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("AchievementObjective1")]
        public uint AchievementObjective1 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("AchievementObjectiveParam1")]
        public uint AchievementObjectiveParam1 { get; set; }

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Arr1Count")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(6)]
        public int Arr1Count { get; set; }

        [FieldIndexAttr(7)]
        public short Short1 { get; set; }

        [FieldIndexAttr(8)]
        public short Short2 { get; set; }

        [FieldIndexAttr(9)]
        public int Int1 { get; set; }

        [FieldIndexAttr(10)]
        public int Int2 { get; set; }

        [FieldIndexAttr(11)]
        public long Long2 { get; set; }

        [FieldIndexAttr(12)]
        public long Empty { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(14)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long AchievementName { get; set; }

        [FieldIndexAttr(15)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long AchievementDescription { get; set; }

        [FieldIndexAttr(16)]
        public long Long3 { get; set; }
    }
}