namespace KnKModTools.TblClass
{
    public class ShopTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ShopInfo")]
        public ShopInfo[] ShopInfos { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ShopItem")]
        public ShopItem[] ShopItems { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ShopTypeDesc")]
        public ShopTypeDesc[] ShopTypeDescs { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ShopConv")]
        public ShopConv[] ShopConvs { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "TradeItem")]
        public TradeItem[] TradeItems { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BargainItem")]
        public BargainItem[] BargainItems { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ShopInfo
    {
        [FieldIndexAttr(0)]
        public ulong Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("ShopName")]
        public long ShopName { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("ShopType")]
        public long ShopType { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(4)]
        public ushort Empty { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("PricePercent")]
        public short ShopPricePercent { get; set; }

        [FieldIndexAttr(6)]
        public float ShopCamPosX { get; set; }

        [FieldIndexAttr(7)]
        public float ShopCamPosY { get; set; }

        [FieldIndexAttr(8)]
        public float ShopCamPosZ { get; set; }

        [FieldIndexAttr(9)]
        public float ShopCamRotation1 { get; set; }

        [FieldIndexAttr(10)]
        public float ShopCamRotation2 { get; set; }

        [FieldIndexAttr(11)]
        public float ShopCamRotation3 { get; set; }

        [FieldIndexAttr(12)]
        public float ShopCamRotation4 { get; set; }

        [FieldIndexAttr(13)]
        public int Int1 { get; set; }

        [FieldIndexAttr(14)]
        public int Int2 { get; set; }

        [FieldIndexAttr(15)]
        public int Int3 { get; set; }

        [FieldIndexAttr(16)]
        public int Int4 { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ShopTable.SManager, this, "ShopName", base.ToString());
        }
    }

    public class ShopItem
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ShopId", true, "ShopInfo.Id")]
        public ushort ShopId { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public short ItemId { get; set; }

        [FieldIndexAttr(2)]
        public int Unknown { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long StartScenaFlags { get; set; }

        [FieldIndexAttr(4)]
        public long Count1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long EndScenaFlags { get; set; }

        [FieldIndexAttr(6)]
        public long Count2 { get; set; }
    }

    public class ShopTypeDesc
    {
        [FieldIndexAttr(0)]
        public ulong Id { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(2)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(3)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(4)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(5)]
        public byte Byte4 { get; set; }

        [FieldIndexAttr(6)]
        public byte Byte5 { get; set; }

        [FieldIndexAttr(7)]
        public byte Byte6 { get; set; }

        [FieldIndexAttr(8)]
        public byte Byte7 { get; set; }

        [FieldIndexAttr(9)]
        public byte Byte8 { get; set; }
    }

    public class ShopConv
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("EarthSepithExchangeRate")]
        public float EarthSepithExchangeRate { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("WaterSepithExchangeRate")]
        public float WaterSepithExchangeRate { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("FireSepithExchangeRate")]
        public float FireSepithExchangeRate { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("WindSepithExchangeRate")]
        public float WindSepithExchangeRate { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("TimeSepithExchangeRate")]
        public float TimeSepithExchangeRate { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("SpaceSepithExchangeRate")]
        public float SpaceSepithExchangeRate { get; set; }

        [FieldIndexAttr(7)]
        [UIDisplay("MirageSepithExchangeRate")]
        public float MirageSepithExchangeRate { get; set; }

        [FieldIndexAttr(8)]
        [UIDisplay("SepithMassExchangeRate")]
        public float SepithMassExchangeRate { get; set; }
    }

    public class TradeItem
    {
        [FieldIndexAttr(0)]
        [UIDisplay("ItemId", true, "ItemTableData.ID")]
        public uint OfferedItemId { get; set; }

        [FieldIndexAttr(1)]
        [BinStreamAttr(Length = 6)]
        [UIDisplay("TradeMaterial")]
        public ShopEffect[] Effects { get; set; } // Assuming ShopEffect is a custom type
    }

    public class BargainItem
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public short Short0 { get; set; }

        [FieldIndexAttr(2)]
        public short Short1 { get; set; }

        [FieldIndexAttr(3)]
        public long Long0 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count0")]
        public long Arr0 { get; set; }

        [FieldIndexAttr(5)]
        public long Count0 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(7)]
        public long Count1 { get; set; }
    }
}