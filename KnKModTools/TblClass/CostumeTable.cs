namespace KnKModTools.TblClass
{
    public class CostumeTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "CostumeParam")]
        public CostumeParam[] CostumeParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "CostumeAttachOffset")]
        public CostumeAttachOffset[] CostumeAttachOffsets { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class CostumeParam
    {
        [FieldIndexAttr(0)]
        [UIDisplay("CharacterId", true, "NameTableData.CharacterId")]
        public short CharacterID { get; set; }

        [FieldIndexAttr(1)]
        public short Shrt1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public short ItemID { get; set; }

        [FieldIndexAttr(3)]
        public short Shrt3 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(6)]
        public uint Int3 { get; set; }

        [FieldIndexAttr(7)]
        public uint Int4 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("AttachName")]
        public long AttachName { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text4 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text5 { get; set; }
    }

    public class CostumeAttachOffset
    {
        [FieldIndexAttr(0)]
        public uint Int0 { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(3)]
        public float Float0 { get; set; }

        [FieldIndexAttr(4)]
        public float Float1 { get; set; }

        [FieldIndexAttr(5)]
        public float Float2 { get; set; }

        [FieldIndexAttr(6)]
        public float Float3 { get; set; }

        [FieldIndexAttr(7)]
        public float Float4 { get; set; }

        [FieldIndexAttr(8)]
        public float Float5 { get; set; }

        [FieldIndexAttr(9)]
        public float Float6 { get; set; }

        [FieldIndexAttr(10)]
        public float Float7 { get; set; }

        [FieldIndexAttr(11)]
        public float Float8 { get; set; }

        [FieldIndexAttr(12)]
        public float Float9 { get; set; }
    }
}