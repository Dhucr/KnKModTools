namespace KnKModTools.TblClass
{
    public class NoteMenuTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteCategoryTab")]
        public NoteCategoryTab[] NoteCategoryTabs { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteChapterMenu")]
        public NoteChapterMenu[] NoteChapterMenus { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteMainChara")]
        public NoteMainChara[] NoteMainCharas { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteConnectMenu")]
        public NoteConnectMenu[] NoteConnectMenus { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteMonsAreaMenu")]
        public NoteMonsAreaMenu[] NoteMonsAreaMenus { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteMonsSpotMenu")]
        public NoteMonsSpotMenu[] NoteMonsSpotMenus { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteFishingMenu")]
        public NoteFishingMenu[] NoteFishingMenus { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteGourmetMenu")]
        public NoteGourmetMenu[] NoteGourmetMenus { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteGourmetArea")]
        public NoteGourmetArea[] NoteGourmetAreas { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteBooksMenu")]
        public NoteBooksMenu[] NoteBooksMenus { get; set; }

        [FieldIndexAttr(12)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteHelpMenu")]
        public NoteHelpMenu[] NoteHelpMenus { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteMonsRecordData")]
        public NoteMonsRecordData[] NoteMonsRecordDatas { get; set; }

        [FieldIndexAttr(14)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NoteGenesisData")]
        public NoteGenesisData[] NoteGenesisDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class NoteCategoryTab
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteChapterMenu
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(4)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(5)]
        public byte Byte4 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteMainChara
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText3 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText4 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText5 { get; set; }

        [FieldIndexAttr(6)]
        public float Float1 { get; set; }

        [FieldIndexAttr(7)]
        public float Float2 { get; set; }

        [FieldIndexAttr(8)]
        public float Float3 { get; set; }

        [FieldIndexAttr(9)]
        public float Float4 { get; set; }
    }

    public class NoteConnectMenu
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteMonsAreaMenu
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(2)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteMonsSpotMenu
    {
        [FieldIndexAttr(0)]
        public long Long1 { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteFishingMenu
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteGourmetMenu
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteGourmetArea
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long Text { get; set; }
    }

    public class NoteBooksMenu
    {
        [FieldIndexAttr(0)]
        public ushort ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }
    }

    public class NoteHelpMenu
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }

    public class NoteMonsRecordData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(2)]
        public int ID { get; set; }

        [FieldIndexAttr(3)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(4)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }
    }

    public class NoteGenesisData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(1)]
        public long Count1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(3)]
        public long Count2 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("NoteText")]
        public long NoteText { get; set; }
    }
}