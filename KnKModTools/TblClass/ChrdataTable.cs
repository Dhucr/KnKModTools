namespace KnKModTools.TblClass
{
    public class ChrdataTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ChrDataParam")]
        public ChrDataParam[] ChrDataParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TalkChrData")]
        public TalkChrData[] TalkChrDatas { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "PortraitChrData")]
        public PortraitChrData[] PortraitChrDatas { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class ChrDataParam
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName { get; set; }

        [FieldIndexAttr(2)]
        public float Float1 { get; set; }

        [FieldIndexAttr(3)]
        public float Float2 { get; set; }

        [FieldIndexAttr(4)]
        public float Float3 { get; set; }

        [FieldIndexAttr(5)]
        public float Float4 { get; set; }

        [FieldIndexAttr(6)]
        public float Float5 { get; set; }

        [FieldIndexAttr(7)]
        public float Float6 { get; set; }

        [FieldIndexAttr(8)]
        public float Float7 { get; set; }

        [FieldIndexAttr(9)]
        public float Float8 { get; set; }

        [FieldIndexAttr(10)]
        public float Float9 { get; set; }

        [FieldIndexAttr(11)]
        public float Float10 { get; set; }

        [FieldIndexAttr(12)]
        public float Float11 { get; set; }

        [FieldIndexAttr(13)]
        public float Float12 { get; set; }

        [FieldIndexAttr(14)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(15)]
        public float Float13 { get; set; }

        [FieldIndexAttr(16)]
        public float Float14 { get; set; }

        [FieldIndexAttr(17)]
        public float Float15 { get; set; }

        [FieldIndexAttr(18)]
        public float Float16 { get; set; }

        [FieldIndexAttr(19)]
        public int Int1 { get; set; }

        [FieldIndexAttr(20)]
        public float Float17 { get; set; }

        [FieldIndexAttr(21)]
        public float Float18 { get; set; }

        [FieldIndexAttr(22)]
        public int Int2 { get; set; }
    }

    public class TalkChrData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Array1 { get; set; }

        [FieldIndexAttr(2)]
        public long Count1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Array2 { get; set; }

        [FieldIndexAttr(4)]
        public int Count2 { get; set; }

        [FieldIndexAttr(5)]
        public int Int1 { get; set; }
    }

    public class PortraitChrData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("FileName")]
        public long FileName { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(3)]
        public float Float1 { get; set; }

        [FieldIndexAttr(4)]
        public int Empty { get; set; }
    }
}