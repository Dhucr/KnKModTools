namespace KnKModTools.TblClass
{
    public class ArchiveTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "PreStoryList")]
        public PreStoryList[] PreStoryLists { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TimeLineList")]
        public TimeLineList[] TimeLineLists { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SeriesList")]
        public SeriesList[] SeriesLists { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SeriesContents")]
        public SeriesContents[] SeriesContentsLists { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "LogList")]
        public LogList[] LogLists { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "LogContents")]
        public LogContents[] LogContentss { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GlossarySelect")]
        public GlossarySelect[] GlossarySelects { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GlossaryList")]
        public GlossaryList[] GlossaryLists { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "GlossaryContents")]
        public GlossaryContents[] GlossaryContentss { get; set; }

        [FieldIndexAttr(12)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "CharactorSelect")]
        public CharactorSelect[] CharactorSelects { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "CharactorList")]
        public CharactorList[] CharactorLists { get; set; }

        [FieldIndexAttr(14)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "CharactorContents")]
        public CharactorContents[] CharactorContentss { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class PreStoryList
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Game { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(5)]
        public long Long1 { get; set; }

        [FieldIndexAttr(6)]
        public long Long2 { get; set; }

        [FieldIndexAttr(7)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(8)]
        public uint Uint3 { get; set; }
    }

    public class TimeLineList
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Title { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class SeriesList
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint UInt1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class SeriesContents
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }
    }

    public class LogList
    {
        [FieldIndexAttr(0)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(3)]
        public long Long1 { get; set; }

        [FieldIndexAttr(4)]
        public long Long2 { get; set; }
    }

    public class LogContents
    {
        [FieldIndexAttr(0)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(2)]
        public long Long2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class GlossarySelect
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint UInt1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class GlossaryList
    {
        [FieldIndexAttr(0)]
        public ushort Short { get; set; }

        [FieldIndexAttr(1)]
        public ushort ID { get; set; }

        [FieldIndexAttr(2)]
        public uint UInt1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(4)]
        public uint UInt2 { get; set; }

        [FieldIndexAttr(5)]
        public uint UInt3 { get; set; }
    }

    public class GlossaryContents
    {
        [FieldIndexAttr(0)]
        public ushort Short { get; set; }

        [FieldIndexAttr(1)]
        public ushort ID { get; set; }

        [FieldIndexAttr(2)]
        public uint UInt1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }
    }

    public class CharactorSelect
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint UInt1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class CharactorList
    {
        [FieldIndexAttr(0)]
        public long Field1 { get; set; }

        [FieldIndexAttr(1)]
        public long Field3 { get; set; }

        [FieldIndexAttr(2)]
        public long Field4 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class CharactorContents
    {
        [FieldIndexAttr(0)]
        public uint Uint1 { get; set; }

        [FieldIndexAttr(1)]
        public uint Uint2 { get; set; }

        [FieldIndexAttr(2)]
        public uint Uint3 { get; set; }

        [FieldIndexAttr(3)]
        public uint Uint4 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }
}