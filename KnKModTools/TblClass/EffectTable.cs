namespace KnKModTools.TblClass
{
    public class EffectTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EffectTableData")]
        public EffectTableData[] EffectTableDatas { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "EffectTableDataChr")]
        public EffectTableDataChr[] EffectTableDataChrs { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class EffectTableData
    {
        [FieldIndexAttr(0)]
        public long Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FilePath")]
        public long FilePath { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class EffectTableDataChr
    {
    }
}