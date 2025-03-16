namespace KnKModTools.TblClass
{
    public class NameTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "NameTableData")]
        public NameTableData[] NameTableDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class NameTableData
    {
        [FieldIndexAttr(0)]
        public ulong CharacterId { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Texture")]
        public long Texture { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Face")]
        public long Face { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Model")]
        public long Model { get; set; }

        [FieldIndexAttr(5)]
        public ulong Long1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(7)]
        public long Long2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(9)]
        public ulong Long3 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FullName")]
        public long FullName { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("EngName")]
        public long EngName { get; set; }

        public override string ToString()
        {
            return TBL.GetText(NameTable.SManager, this, "Name", base.ToString());
        }
    }

    //未来改为多继承(NameTableEx : NameTableData,TableDataEx)
    public class NameTableEx : NameTableData
    {
        public string DisplayName { get; set; }

        public NameTableEx(ulong id, string name)
        {
            CharacterId = id;
            DisplayName = name;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}