namespace KnKModTools.TblClass
{
    public class ConnectTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConnectChrParam")]
        public ConnectChrParam[] ConnectChrParams { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConnectBonusParam")]
        public ConnectBonusParam[] ConnectBonusParams { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConnectTopicData")]
        public ConnectTopicData[] ConnectTopicDatas { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConnectEventData")]
        public ConnectEventData[] ConnectEventDatas { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConnectItemData")]
        public ConnectItemData[] ConnectItemDatas { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "LGCParam")]
        public LGCParam[] LGCParams { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class ConnectChrParam
    {
        [FieldIndexAttr(0)]
        public ushort ID1 { get; set; }

        [FieldIndexAttr(1)]
        public ushort ID2 { get; set; }

        [FieldIndexAttr(2)]
        public int Empty { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("CharFullName")]
        public long CharFullName { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("CharFirstName")]
        public long CharFirstName { get; set; }

        [FieldIndexAttr(5)]
        public long Long1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ScenaFile")]
        public long ScenaFile { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Affiliation")]
        public long Affiliation { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Occupation")]
        public long Occupation { get; set; }

        [FieldIndexAttr(9)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(10)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(11)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(12)]
        public byte Byte4 { get; set; }

        [FieldIndexAttr(13)]
        public byte Byte5 { get; set; }

        [FieldIndexAttr(14)]
        public byte Byte6 { get; set; }

        [FieldIndexAttr(15)]
        public byte Byte7 { get; set; }

        [FieldIndexAttr(16)]
        public byte Byte8 { get; set; }
    }

    public class ConnectBonusParam
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(2)]
        public long Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(4)]
        public int Count1 { get; set; }

        [FieldIndexAttr(5)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(6)]
        public long Long2 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(8)]
        public int Count2 { get; set; }

        [FieldIndexAttr(9)]
        public uint Int3 { get; set; }

        [FieldIndexAttr(10)]
        public long Long3 { get; set; }

        [FieldIndexAttr(11)]
        public long Empty { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long Description { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Bonus")]
        public long Bonus { get; set; }
    }

    public class ConnectTopicData
    {
        [FieldIndexAttr(0)]
        public short ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(2)]
        public short Empty1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long ConnectName { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Description")]
        public long ConnectDescription { get; set; }
    }

    public class ConnectEventData
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(1)]
        public int Count1 { get; set; }

        [FieldIndexAttr(2)]
        public long Long1 { get; set; }

        [FieldIndexAttr(3)]
        public int Empty1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(6)]
        public long Long2 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(8)]
        public long Count2 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(10)]
        public int Count3 { get; set; }

        [FieldIndexAttr(11)]
        public int Int1 { get; set; }

        [FieldIndexAttr(12)]
        public int Int2 { get; set; }

        [FieldIndexAttr(13)]
        public int Int3 { get; set; }
    }

    public class ConnectItemData
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(2)]
        public ulong Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ItemName")]
        public long ItemName { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ItemDescription")]
        public long ItemDescription { get; set; }

        [FieldIndexAttr(5)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(6)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(7)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(8)]
        public byte Byte4 { get; set; }

        [FieldIndexAttr(9)]
        public uint Empty1 { get; set; }
    }

    public class LGCParam
    {
        [FieldIndexAttr(0)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(1)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(2)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte4 { get; set; }
    }
}