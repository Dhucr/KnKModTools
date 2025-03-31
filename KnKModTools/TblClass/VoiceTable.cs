namespace KnKModTools.TblClass
{
    public class VoiceTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "VoiceTableData")]
        public VoiceTableData[] VoiceTableDatas { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class VoiceTableData
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Filename")]
        public long Filename { get; set; }

        [FieldIndexAttr(3)]
        public int Int2 { get; set; }

        [FieldIndexAttr(4)]
        public float Float1 { get; set; }

        [FieldIndexAttr(5)]
        public int Int3 { get; set; }

        [FieldIndexAttr(6)]
        public float Float2 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }
}