namespace KnKModTools.TblClass
{
    public class MapJumpTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpWorldData")]
        public MapJumpWorldData[] MapJumpWorldDatas { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpRegionData")]
        public MapJumpRegionData[] MapJumpRegionDatas { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpAreaData")]
        public MapJumpAreaData[] MapJumpAreaDatas { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData1s { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData2s { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData3s { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData4s { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData5s { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData6s { get; set; }

        [FieldIndexAttr(12)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData7s { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "MapJumpSpotData")]
        public MapJumpSpotData[] MapJumpSpotData8s { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class MapJumpWorldData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

    public class MapJumpRegionData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long Name { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(3)]
        public float Float1 { get; set; }

        [FieldIndexAttr(4)]
        public float Float2 { get; set; }
    }

    public class MapJumpAreaData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("PlaceName")]
        public long PlaceName { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("PlaceNameHighlight")]
        public long PlaceNameHighlight { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ScenaFile")]
        public long ScenaFile { get; set; }

        [FieldIndexAttr(4)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(5)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(6)]
        public float Float1 { get; set; }

        [FieldIndexAttr(7)]
        public float Float2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Place")]
        public long Place { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("JumpFile")]
        public long JumpFile { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }

        [FieldIndexAttr(11)]
        public long Long1 { get; set; }
    }

    public class MapJumpSpotData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("PlaceName")]
        public long PlaceName { get; set; }

        [FieldIndexAttr(2)]
        public long Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ScenaFile")]
        public long ScenaFile { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset1 { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("XCoordinate")]
        public float XCoordinate { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("YCoordinate")]
        public float YCoordinate { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("ZCoordinate")]
        public float ZCoordinate { get; set; }

        [FieldIndexAttr(8)]
        public float Float4 { get; set; }

        [FieldIndexAttr(9)]
        public long Long2 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("JumpFile")]
        public long JumpFile { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Toffset2 { get; set; }
    }
}