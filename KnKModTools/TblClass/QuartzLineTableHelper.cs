using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class QuartzLineTableHelper
    {
        public static QuartzLineTable DeSerialize(BinaryReader br)
        {
            QuartzLineTable obj = TBLHelper.DeSerialize<QuartzLineTable>(br);
            // 处理SubHeader关联数组: ElementSlotRates
            SubHeader? subHeader_ElementSlotRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ElementSlotRate");
            if (subHeader_ElementSlotRates != null)
            {
                br.BaseStream.Seek(subHeader_ElementSlotRates.DataOffset, SeekOrigin.Begin);
                obj.ElementSlotRates = new ElementSlotRate[subHeader_ElementSlotRates.NodeCount];
                for (var i = 0; i < subHeader_ElementSlotRates.NodeCount; i++)
                {
                    obj.ElementSlotRates[i] = ElementSlotRateHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ElementSlotRates = Array.Empty<ElementSlotRate>();
            }
            obj.NodeDatas.Add(subHeader_ElementSlotRates, obj.ElementSlotRates);
            // 处理SubHeader关联数组: QuartzLineParams
            SubHeader? subHeader_QuartzLineParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuartzLineParam");
            if (subHeader_QuartzLineParams != null)
            {
                br.BaseStream.Seek(subHeader_QuartzLineParams.DataOffset, SeekOrigin.Begin);
                obj.QuartzLineParams = new QuartzLineParam[subHeader_QuartzLineParams.NodeCount];
                for (var i = 0; i < subHeader_QuartzLineParams.NodeCount; i++)
                {
                    obj.QuartzLineParams[i] = QuartzLineParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuartzLineParams = Array.Empty<QuartzLineParam>();
            }
            obj.NodeDatas.Add(subHeader_QuartzLineParams, obj.QuartzLineParams);

            var list = new List<IDataPointer>();
            obj.Pointers = [];
            obj.Manager = new DataPoolManager();
            obj.Handler = new DataPoolHandler(obj.Manager, br, obj, obj.Pointers);
            RuntimeHelper.TraverseObjects(obj, o =>
            {
                list.AddRange(obj.Handler.ProcessObject(o, false));
            });
            obj.Manager.RefreshOffsetDic(obj.Pointers);
            list.Clear();
            QuartzLineTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not QuartzLineTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ElementSlotRates
            SubHeader? subHeader_ElementSlotRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ElementSlotRate");
            if (subHeader_ElementSlotRates != null)
            {
                bw.BaseStream.Seek(subHeader_ElementSlotRates.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ElementSlotRates.NodeCount; i++)
                {
                    ElementSlotRateHelper.Serialize(bw, obj.ElementSlotRates[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuartzLineParams
            SubHeader? subHeader_QuartzLineParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuartzLineParam");
            if (subHeader_QuartzLineParams != null)
            {
                bw.BaseStream.Seek(subHeader_QuartzLineParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuartzLineParams.NodeCount; i++)
                {
                    QuartzLineParamHelper.Serialize(bw, obj.QuartzLineParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ElementSlotRateHelper
    {
        public static ElementSlotRate DeSerialize(BinaryReader br)
        {
            var obj = new ElementSlotRate
            {
                ElementRestrictMultiply = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ElementSlotRate tbl)
        {
            if (tbl is not ElementSlotRate obj) return;
            bw.Write(obj.ElementRestrictMultiply);
        }
    }

    public static class QuartzLineParamHelper
    {
        public static QuartzLineParam DeSerialize(BinaryReader br)
        {
            var obj = new QuartzLineParam
            {
                CharacterId = br.ReadUInt32(),
                LineType = br.ReadUInt32(),
                Field1 = br.ReadInt32(),
                Short1 = br.ReadUInt16(),
                Slots = new QuartzLineSlot[3]
            };
            for (var i = 0; i < 3; i++)
            {
                obj.Slots[i] = QuartzLineSlotHelper.DeSerialize(br);
            }
            obj.SlotLineNumber = br.ReadUInt16();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuartzLineParam tbl)
        {
            if (tbl is not QuartzLineParam obj) return;
            bw.Write(obj.CharacterId);
            bw.Write(obj.LineType);
            bw.Write(obj.Field1);
            bw.Write(obj.Short1);
            for (var i = 0; i < 3; i++)
            {
                QuartzLineSlotHelper.Serialize(bw, obj.Slots[i]);
            }
            bw.Write(obj.SlotLineNumber);
        }
    }

    public static class QuartzLineSlotHelper
    {
        public static QuartzLineSlot DeSerialize(BinaryReader br)
        {
            var obj = new QuartzLineSlot
            {
                RestrictionId = br.ReadUInt32(),
                Short1 = br.ReadUInt16(),
                Short2 = br.ReadUInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuartzLineSlot tbl)
        {
            if (tbl is not QuartzLineSlot obj) return;
            bw.Write(obj.RestrictionId);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
        }
    }
}