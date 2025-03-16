namespace KnKModTools.TblClass
{
    public class FreeDungeonTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonData")]
        public FreeDungeonData[] FreeDungeonDatas { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonFloorCompositionData")]
        public FreeDungeonFloorCompositionData[] FreeDungeonFloorCompositionDatas { get; set; }

        [FieldIndexAttr(5)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonNullSquaresPattren")]
        public FreeDungeonNullSquaresPattren[] FreeDungeonNullSquaresPattrens { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonSquaresPattrenData")]
        public FreeDungeonSquaresPattrenData[] FreeDungeonSquaresPattrenDatas { get; set; }

        [FieldIndexAttr(7)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonSection")]
        public FreeDungeonSection[] FreeDungeonSections { get; set; }

        [FieldIndexAttr(8)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonSwapMass")]
        public FreeDungeonSwapMass[] FreeDungeonSwapMasses { get; set; }

        [FieldIndexAttr(9)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonReward")]
        public FreeDungeonReward[] FreeDungeonRewards { get; set; }

        [FieldIndexAttr(10)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonSupportPos")]
        public FreeDungeonSupportPos[] FreeDungeonSupportPoss { get; set; }

        [FieldIndexAttr(11)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonAvatarChara")]
        public FreeDungeonAvatarChara[] FreeDungeonAvatarCharas { get; set; }

        [FieldIndexAttr(12)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonMainMenuList")]
        public FreeDungeonMainMenuList[] FreeDungeonMainMenuLists { get; set; }

        [FieldIndexAttr(13)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonShopListItemData")]
        public FreeDungeonShopListItemData[] FreeDungeonShopListItemDatas { get; set; }

        [FieldIndexAttr(14)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonShopTypeList")]
        public FreeDungeonShopTypeList[] FreeDungeonShopTypeLists { get; set; }

        [FieldIndexAttr(15)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonTBox")]
        public FreeDungeonTBox[] FreeDungeonTBoxes { get; set; }

        [FieldIndexAttr(16)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonBreakObj")]
        public FreeDungeonBreakObj[] FreeDungeonBreakObjs { get; set; }

        [FieldIndexAttr(17)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonRandomMass")]
        public FreeDungeonRandomMass[] FreeDungeonRandomMasses { get; set; }

        [FieldIndexAttr(18)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonClearTargetText")]
        public FreeDungeonClearTargetText[] FreeDungeonClearTargetTexts { get; set; }

        [FieldIndexAttr(19)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonMassEffectText")]
        public FreeDungeonMassEffectText[] FreeDungeonMassEffectTexts { get; set; }

        [FieldIndexAttr(20)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonTBoxMassContents")]
        public FreeDungeonTBoxMassContents[] FreeDungeonTBoxMassContents { get; set; }

        [FieldIndexAttr(21)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonCraftBuildItemList")]
        public FreeDungeonCraftBuildItemList[] FreeDungeonCraftBuildItemLists { get; set; }

        [FieldIndexAttr(22)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "FreeDungeonFactoryExpansionList")]
        public FreeDungeonFactoryExpansionList[] FreeDungeonFactoryExpansionLists { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class FreeDungeonData
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public long Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(4)]
        public int Count1 { get; set; }

        [FieldIndexAttr(5)]
        public short Short1 { get; set; }

        [FieldIndexAttr(6)]
        public short Short2 { get; set; }

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
        public short Short3 { get; set; }

        [FieldIndexAttr(12)]
        public short Short4 { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count4")]
        public long Arr4 { get; set; }

        [FieldIndexAttr(14)]
        public long Count4 { get; set; }

        [FieldIndexAttr(15)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count5")]
        public long Arr5 { get; set; }

        [FieldIndexAttr(16)]
        public long Count5 { get; set; }

        [FieldIndexAttr(17)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(18)]
        public float Float1 { get; set; }

        [FieldIndexAttr(19)]
        public float Float2 { get; set; }

        [FieldIndexAttr(20)]
        public int Int2 { get; set; }

        [FieldIndexAttr(21)]
        public int Int3 { get; set; }

        [FieldIndexAttr(22)]
        public int Int4 { get; set; }

        [FieldIndexAttr(23)]
        public int Int5 { get; set; }

        [FieldIndexAttr(24)]
        public int Int6 { get; set; }

        [FieldIndexAttr(25)]
        public int Int7 { get; set; }

        [FieldIndexAttr(26)]
        public int Int8 { get; set; }

        [FieldIndexAttr(27)]
        public int Int9 { get; set; }

        [FieldIndexAttr(28)]
        public int Int10 { get; set; }

        [FieldIndexAttr(29)]
        public long Long2 { get; set; }

        [FieldIndexAttr(30)]
        public int Int11 { get; set; }

        [FieldIndexAttr(31)]
        public int Int12 { get; set; }

        [FieldIndexAttr(32)]
        public int Int13 { get; set; }

        [FieldIndexAttr(33)]
        public int Int14 { get; set; }

        [FieldIndexAttr(34)]
        public long Long3 { get; set; }

        [FieldIndexAttr(35)]
        public int Int15 { get; set; }

        [FieldIndexAttr(36)]
        public long Long4 { get; set; }

        [FieldIndexAttr(37)]
        public int Int16 { get; set; }

        [FieldIndexAttr(38)]
        public int Int17 { get; set; }

        [FieldIndexAttr(39)]
        public int Int18 { get; set; }

        [FieldIndexAttr(40)]
        public int Int19 { get; set; }

        [FieldIndexAttr(41)]
        public int Int20 { get; set; }

        [FieldIndexAttr(42)]
        public int Int21 { get; set; }

        [FieldIndexAttr(43)]
        public int Int22 { get; set; }

        [FieldIndexAttr(44)]
        public int Int23 { get; set; }

        [FieldIndexAttr(45)]
        public int Int24 { get; set; }

        [FieldIndexAttr(46)]
        public int Int25 { get; set; }

        [FieldIndexAttr(47)]
        public int Int26 { get; set; }

        [FieldIndexAttr(48)]
        public int Int27 { get; set; }
    }

    public class FreeDungeonFloorCompositionData
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(2)]
        public long Count1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(4)]
        public long Count2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(6)]
        public long Count3 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count4")]
        public long Arr4 { get; set; }

        [FieldIndexAttr(8)]
        public long Count4 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count5")]
        public long Arr5 { get; set; }

        [FieldIndexAttr(10)]
        public long Count5 { get; set; }

        [FieldIndexAttr(11)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count6")]
        public long Arr6 { get; set; }

        [FieldIndexAttr(12)]
        public long Count6 { get; set; }

        [FieldIndexAttr(13)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count7")]
        public long Arr7 { get; set; }

        [FieldIndexAttr(14)]
        public long Count7 { get; set; }

        [FieldIndexAttr(15)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count8")]
        public long Arr8 { get; set; }

        [FieldIndexAttr(16)]
        public long Count8 { get; set; }

        [FieldIndexAttr(17)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count9")]
        public long Arr9 { get; set; }

        [FieldIndexAttr(18)]
        public long Count9 { get; set; }

        [FieldIndexAttr(19)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count10")]
        public long Arr10 { get; set; }

        [FieldIndexAttr(20)]
        public long Count10 { get; set; }

        [FieldIndexAttr(21)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count11")]
        public long Arr11 { get; set; }

        [FieldIndexAttr(22)]
        public long Count11 { get; set; }

        [FieldIndexAttr(23)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count12")]
        public long Arr12 { get; set; }

        [FieldIndexAttr(24)]
        public long Count12 { get; set; }
    }

    public class FreeDungeonNullSquaresPattren
    {
        [FieldIndexAttr(0)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(1)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(2)]
        public uint Int3 { get; set; }
    }

    public class FreeDungeonSquaresPattrenData
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public long Long1 { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(4)]
        public int Count { get; set; }

        [FieldIndexAttr(5)]
        public int Int2 { get; set; }

        [FieldIndexAttr(6)]
        public int Int3 { get; set; }

        [FieldIndexAttr(7)]
        public int Int4 { get; set; }

        [FieldIndexAttr(8)]
        public long Long2 { get; set; }

        [FieldIndexAttr(9)]
        public long Long3 { get; set; }
    }

    public class FreeDungeonSection
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(5)]
        public long Count2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(7)]
        public long Count3 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count4")]
        public long Arr4 { get; set; }

        [FieldIndexAttr(9)]
        public long Count4 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count5")]
        public long Arr5 { get; set; }

        [FieldIndexAttr(11)]
        public long Count5 { get; set; }

        [FieldIndexAttr(12)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count6")]
        public long Arr6 { get; set; }

        [FieldIndexAttr(13)]
        public long Count6 { get; set; }

        [FieldIndexAttr(14)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count7")]
        public long Arr7 { get; set; }

        [FieldIndexAttr(15)]
        public long Count7 { get; set; }

        [FieldIndexAttr(16)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count8")]
        public long Arr8 { get; set; }

        [FieldIndexAttr(17)]
        public long Count8 { get; set; }
    }

    public class FreeDungeonSwapMass
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(5)]
        public long Count2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(7)]
        public long Count3 { get; set; }
    }

    public class FreeDungeonReward
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(3)]
        public long Count { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class FreeDungeonSupportPos
    {
        [FieldIndexAttr(0)]
        public int Int1 { get; set; }

        [FieldIndexAttr(1)]
        public float Float1 { get; set; }

        [FieldIndexAttr(2)]
        public float Float2 { get; set; }

        [FieldIndexAttr(3)]
        public float Float3 { get; set; }

        [FieldIndexAttr(4)]
        public float Float4 { get; set; }

        [FieldIndexAttr(5)]
        public float Float5 { get; set; }

        [FieldIndexAttr(6)]
        public float Float6 { get; set; }

        [FieldIndexAttr(7)]
        public float Float7 { get; set; }

        [FieldIndexAttr(8)]
        public float Float8 { get; set; }

        [FieldIndexAttr(9)]
        public float Float9 { get; set; }

        [FieldIndexAttr(10)]
        public float Float10 { get; set; }
    }

    public class FreeDungeonAvatarChara
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        public float Float1 { get; set; }

        [FieldIndexAttr(2)]
        public int Empty1 { get; set; }

        [FieldIndexAttr(3)]
        public float Float2 { get; set; }

        [FieldIndexAttr(4)]
        public int Empty2 { get; set; }

        [FieldIndexAttr(5)]
        public float Float3 { get; set; }

        [FieldIndexAttr(6)]
        public int Empty3 { get; set; }
    }

    public class FreeDungeonMainMenuList
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(5)]
        public long Count2 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(7)]
        public long Count3 { get; set; }
    }

    public class FreeDungeonShopListItemData
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(3)]
        public long Count1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(5)]
        public long Long1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(7)]
        public long Count2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(9)]
        public long Count3 { get; set; }

        [FieldIndexAttr(10)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class FreeDungeonShopTypeList
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(2)]
        public long Count { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text3 { get; set; }

        [FieldIndexAttr(6)]
        public long Long1 { get; set; }
    }

    public class FreeDungeonTBox
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.BaseArray, typeof(ushort), "Count")]
        public long Arr { get; set; }

        [FieldIndexAttr(2)]
        public long Count { get; set; }

        [FieldIndexAttr(3)]
        public long Long1 { get; set; }
    }

    public class FreeDungeonBreakObj
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }

        [FieldIndexAttr(2)]
        public short Short1 { get; set; }

        [FieldIndexAttr(3)]
        public short Short2 { get; set; }

        [FieldIndexAttr(4)]
        public short Short3 { get; set; }

        [FieldIndexAttr(5)]
        public short Short4 { get; set; }
    }

    public class FreeDungeonRandomMass
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }

        [FieldIndexAttr(3)]
        public int Int3 { get; set; }

        [FieldIndexAttr(4)]
        public int Int4 { get; set; }

        [FieldIndexAttr(5)]
        public int Int5 { get; set; }

        [FieldIndexAttr(6)]
        public int Int6 { get; set; }

        [FieldIndexAttr(7)]
        public int Int7 { get; set; }
    }

    public class FreeDungeonClearTargetText
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class FreeDungeonMassEffectText
    {
        [FieldIndexAttr(0)]
        public long ID { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text2 { get; set; }
    }

    public class FreeDungeonTBoxMassContents
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public float Float1 { get; set; }

        [FieldIndexAttr(2)]
        public float Float2 { get; set; }

        [FieldIndexAttr(3)]
        public float Float3 { get; set; }
    }

    public class FreeDungeonCraftBuildItemList
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }
    }

    public class FreeDungeonFactoryExpansionList
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        public int Int2 { get; set; }

        [FieldIndexAttr(3)]
        public int Int3 { get; set; }

        [FieldIndexAttr(4)]
        public int Int4 { get; set; }
    }
}