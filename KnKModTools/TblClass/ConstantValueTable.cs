namespace KnKModTools.TblClass
{
    public class ConstantValueTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConstantValue")]
        public ConstantValue[] ConstantValues { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ConstantValue
    {
        [FieldIndexAttr(0)]
        public uint ID { get; set; }

        [FieldIndexAttr(1)]
        public uint Value1 { get; set; }

        [FieldIndexAttr(2)]
        public uint Value2 { get; set; }
    }
}