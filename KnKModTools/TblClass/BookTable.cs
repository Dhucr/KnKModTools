namespace KnKModTools.TblClass
{
    public class BookTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BooksTitle")]
        public BooksTitle[] BooksTitles { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BooksText")]
        public BooksText[] BooksTexts { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BooksTitleReplace")]
        public BooksTitleReplace[] BooksTitleReplaces { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class BooksTitle
    {
        [FieldIndexAttr(0)]
        public short Id { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Title")]
        public long Title { get; set; } // "toffset" 被映射为字符串类型

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName { get; set; } // "toffset" 被映射为字符串类型

        [FieldIndexAttr(5)]
        public long Long2 { get; set; }

        public override string ToString()
        {
            return TBL.GetText(BookTable.SManager, this, "Title", base.ToString());
        }
    }

    public class BooksText
    {
        [FieldIndexAttr(0)]
        [UIDisplay("BookId", true, "BooksTitle.Id")]
        public short BookId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("PageId")]
        public int PageId { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("BookText")]
        public long BookText { get; set; } // "toffset" 被映射为字符串类型

        [FieldIndexAttr(4)]
        public long Long1 { get; set; }
    }

    public class BooksTitleReplace
    {
        [FieldIndexAttr(0)]
        public ushort Id { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Short1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long BookName { get; set; } // "toffset" 被映射为字符串类型

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long FileName { get; set; } // "toffset" 被映射为字符串类型

        [FieldIndexAttr(5)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(6)]
        public ushort Short3 { get; set; }

        [FieldIndexAttr(7)]
        public int Empty { get; set; }
    }
}