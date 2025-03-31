namespace KnKModTools.TblClass
{
    public class BGMSetTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapBGM")]
        public MapBGM[] MapBGMs { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BattleBGM")]
        public BattleBGM[] BattleBGMs { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ReplaceBGMParam")]
        public ReplaceBGMParam[] ReplaceBGMParams { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class MapBGM
    {
        [FieldIndexAttr(0)]
        public long Long1 { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(2)]
        public long Count1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(4)]
        public int Count2 { get; set; }

        [FieldIndexAttr(5)]
        public int Int1 { get; set; }
    }

    public class BattleBGM
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(2)]
        public long Count1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(4)]
        public long Count2 { get; set; }

        [FieldIndexAttr(5)]
        public int Int1 { get; set; }

        [FieldIndexAttr(6)]
        public int Int2 { get; set; }

        [FieldIndexAttr(7)]
        public int Int3 { get; set; }

        [FieldIndexAttr(8)]
        public int Int4 { get; set; }
    }

    public class ReplaceBGMParam
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TrackName1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long TrackName2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Ost { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long SoundFile { get; set; }

        [FieldIndexAttr(7)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(8)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(9)]
        public float Float { get; set; }
    }
}