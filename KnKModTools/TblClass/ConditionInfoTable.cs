namespace KnKModTools.TblClass
{
    public class ConditionInfoTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConditionInfoTableData")]
        public ConditionInfoTableData[] ConditionInfoTableDatas { get; set; }

        [FieldIndexAttr(4)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConditionTypeParam")]
        public ConditionTypeParam[] ConditionTypeParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class ConditionInfoTableData
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        [UIDisplay("Name")]
        public long StateName { get; set; }

        [FieldIndexAttr(3)]
        public uint Int2 { get; set; }

        [FieldIndexAttr(4)]
        public uint Int3 { get; set; }

        [FieldIndexAttr(5)]
        public uint Int4 { get; set; }

        [FieldIndexAttr(6)]
        [BinStreamAttr(3)]
        [UIDisplay("Effect")]
        public ConditionEffect[] Effects { get; set; } // Array of 3 elements

        [FieldIndexAttr(7)]
        public float Float1 { get; set; }

        [FieldIndexAttr(8)]
        public float Float2 { get; set; }

        [FieldIndexAttr(9)]
        [UIDisplay("Icon", true, "Icon.icons")]
        public int IconId { get; set; }

        [FieldIndexAttr(10)]
        public int Int5 { get; set; }

        [FieldIndexAttr(11)]
        public int Int6 { get; set; }

        [FieldIndexAttr(12)]
        public int Int7 { get; set; }

        [FieldIndexAttr(13)]
        public int Int8 { get; set; }

        [FieldIndexAttr(14)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        public override string ToString()
        {
            return TBL.GetText(ConditionInfoTable.SManager, this, "StateName", base.ToString());
        }
    }

    public class ConditionTypeParam
    {
        [FieldIndexAttr(0)]
        public uint Id { get; set; }

        [FieldIndexAttr(1)]
        public uint Value { get; set; }
    }
}