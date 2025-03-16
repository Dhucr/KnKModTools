namespace KnKModTools.TblClass
{
    public class QuestTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestRank")]
        public QuestRank[] QuestRanks { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestChapterRank")]
        public QuestChapterRank[] QuestChapterRanks { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestTitle")]
        public QuestTitle[] QuestTitles { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestText")]
        public QuestText[] QuestTexts { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestSubText")]
        public QuestSubText[] QuestSubTexts { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestReportMessage")]
        public QuestReportMessage[] QuestReportMessages { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestCheckMessage")]
        public QuestCheckMessage[] QuestCheckMessages { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "RecruitmentMember")]
        public RecruitmentMember[] RecruitmentMembers { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "QuestReportBackTexture")]
        public QuestReportBackTexture[] QuestReportBackTextures { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "StampCharaList")]
        public StampCharaList[] StampCharaLists { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class QuestRank
    {
        [FieldIndexAttr(0)]
        public ulong Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("RankName")]
        public long RankName { get; set; }

        [FieldIndexAttr(2)]
        public int Int1 { get; set; }

        [FieldIndexAttr(3)]
        public int Int2 { get; set; }
    }

    public class QuestChapterRank
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }

        [FieldIndexAttr(3)]
        public int Int3 { get; set; }

        [FieldIndexAttr(4)]
        public int Int4 { get; set; }
    }

    public class QuestTitle
    {
        [FieldIndexAttr(0)]
        public ushort Id { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("QuestDescription")]
        public long QuestDescription { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("QuestGiver")]
        public long QuestGiver { get; set; }

        [FieldIndexAttr(5)]
        public short Short1 { get; set; }

        [FieldIndexAttr(6)]
        public int Int2 { get; set; }

        [FieldIndexAttr(7)]
        public short Empty2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(10)]
        public int Int3 { get; set; }

        [FieldIndexAttr(11)]
        public int Int4 { get; set; }

        [FieldIndexAttr(12)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(13)]
        public ushort Short3 { get; set; }

        [FieldIndexAttr(14)]
        public ushort Short4 { get; set; }

        [FieldIndexAttr(15)]
        public ushort Empty3 { get; set; }

        [FieldIndexAttr(16)]
        public int Int5 { get; set; }

        [FieldIndexAttr(17)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(18)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(19)]
        public ushort Short5 { get; set; }

        [FieldIndexAttr(20)]
        public long Long1 { get; set; }
    }

    public class QuestText
    {
        [FieldIndexAttr(0)]
        public ushort QuestId { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("QuestDescription")]
        public long QuestDescription { get; set; }

        // array_flag1 数组及其长度字段
        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long ArrayFlag1 { get; set; }

        [FieldIndexAttr(5)]
        public long Count1 { get; set; } // 作为 ArrayFlag1 的长度

        // array_flag2 数组及其长度字段
        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long ArrayFlag2 { get; set; }

        [FieldIndexAttr(7)]
        public long Count2 { get; set; } // 作为 ArrayFlag2 的长度

        [FieldIndexAttr(8)]
        public long Empty5 { get; set; }
    }

    public class QuestSubText
    {
        [FieldIndexAttr(0)]
        public short Short1 { get; set; }

        [FieldIndexAttr(1)]
        public short Short2 { get; set; }

        [FieldIndexAttr(2)]
        public short Short3 { get; set; }

        [FieldIndexAttr(3)]
        public short Short4 { get; set; }

        [FieldIndexAttr(4)]
        public short Short5 { get; set; }

        [FieldIndexAttr(5)]
        public short Short6 { get; set; }

        [FieldIndexAttr(6)]
        public short Short7 { get; set; }

        [FieldIndexAttr(7)]
        public short Short8 { get; set; }

        [FieldIndexAttr(8)]
        public short Short9 { get; set; }

        [FieldIndexAttr(9)]
        public short Short10 { get; set; }

        [FieldIndexAttr(10)]
        public short Short11 { get; set; }

        [FieldIndexAttr(11)]
        public short Short12 { get; set; }

        [FieldIndexAttr(12)]
        public short Short13 { get; set; }

        [FieldIndexAttr(13)]
        public short Short14 { get; set; }

        [FieldIndexAttr(14)]
        public short Short15 { get; set; }

        [FieldIndexAttr(15)]
        public short Short16 { get; set; }

        [FieldIndexAttr(16)]
        public short Short17 { get; set; }

        [FieldIndexAttr(17)]
        public short Short18 { get; set; }

        [FieldIndexAttr(18)]
        public short Short19 { get; set; }

        [FieldIndexAttr(19)]
        public short Short20 { get; set; }

        [FieldIndexAttr(20)]
        public short Short21 { get; set; }

        [FieldIndexAttr(21)]
        public short Short22 { get; set; }

        [FieldIndexAttr(22)]
        public short Short23 { get; set; }

        [FieldIndexAttr(23)]
        public short Short24 { get; set; }
    }

    public class QuestReportMessage
    {
        [FieldIndexAttr(0)]
        public long Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class QuestCheckMessage
    {
        [FieldIndexAttr(0)]
        public long Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class RecruitmentMember
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Recruitment { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long CharName { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Age { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Home { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Occupation { get; set; }

        [FieldIndexAttr(7)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(8)]
        public uint Int3 { get; set; }
    }

    public class QuestReportBackTexture
    {
        [FieldIndexAttr(0)]
        public long Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Texture1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Texture2 { get; set; }
    }

    public class StampCharaList
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public float Float1 { get; set; }

        [FieldIndexAttr(2)]
        public float Float2 { get; set; }

        [FieldIndexAttr(3)]
        public float Int1 { get; set; }
    }
}