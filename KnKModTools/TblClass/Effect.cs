namespace KnKModTools.TblClass
{
    public class Effect
    {
        [FieldIndexAttr(0)]
        [UIDisplay("EffectId", true, "SkillEffectHelpData+SkillEffectHelpDataAdd.ID")]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Param1")]
        public int Param1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Param2")]
        public int Param2 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Param3")]
        public int Param3 { get; set; }
    }

    public class ShardSkillEffect
    {
        [FieldIndexAttr(0)]
        [UIDisplay("EffectId")]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Param1")]
        public int Param1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Param2")]
        public int Param2 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Param3")]
        public int Param3 { get; set; }
    }

    public class ConditionEffect
    {
        [FieldIndexAttr(0)]
        [UIDisplay("Small")]
        public int Small { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Medium")]
        public int Medium { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Large")]
        public int Large { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Undefined")]
        public int Undefined { get; set; }
    }

    public class HollowCoreEffect
    {
        [FieldIndexAttr(0)]
        [UIDisplay("Effect", true, "HollowCoreEffText.Id")]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("Param1")]
        public int Param1 { get; set; }

        [FieldIndexAttr(2)]
        [UIDisplay("Param2")]
        public int Param2 { get; set; }

        [FieldIndexAttr(3)]
        [UIDisplay("Param3")]
        public int Param3 { get; set; }

        [FieldIndexAttr(4)]
        [UIDisplay("Param4")]
        public int Param4 { get; set; }

        [FieldIndexAttr(5)]
        [UIDisplay("Param5")]
        public int Param5 { get; set; }

        [FieldIndexAttr(6)]
        [UIDisplay("Param6")]
        public int Param6 { get; set; }
    }

    public class ShopEffect
    {
        [FieldIndexAttr(0)]
        [UIDisplay("TradeMaterialItemID", true, "ItemTableData.ID")]
        public int TradeMaterialItemID { get; set; }

        [FieldIndexAttr(1)]
        [UIDisplay("RequirAmount")]
        public int RequirAmount { get; set; }
    }
}