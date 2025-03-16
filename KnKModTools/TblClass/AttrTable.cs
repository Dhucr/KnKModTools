namespace KnKModTools.TblClass
{
    public class AttrTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "AttrData")]
        public AttrData[] AttrDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class AttrData
    {
        [FieldIndexAttr(0)]
        public byte Id { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Attr", true, "AttrTypeHelpData.ID")]
        public byte ElementId1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Attr", true, "AttrTypeHelpData.ID")]
        public byte ElementId2 { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("Icon", true, "Icon.icons")]
        public int IconId { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long ElementName { get; set; }

        public override string ToString()
        {
            return TBL.GetText(AttrTable.SManager, this, "ElementName", base.ToString());
        }
    }
}