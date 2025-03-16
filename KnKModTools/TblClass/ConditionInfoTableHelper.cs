using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ConditionInfoTableHelper
    {
        public static ConditionInfoTable DeSerialize(BinaryReader br)
        {
            ConditionInfoTable obj = TBLHelper.DeSerialize<ConditionInfoTable>(br);
            // 处理SubHeader关联数组: ConditionInfoTableDatas
            SubHeader? subHeader_ConditionInfoTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionInfoTableData");
            if (subHeader_ConditionInfoTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_ConditionInfoTableDatas.DataOffset, SeekOrigin.Begin);
                obj.ConditionInfoTableDatas = new ConditionInfoTableData[subHeader_ConditionInfoTableDatas.NodeCount];
                for (var i = 0; i < subHeader_ConditionInfoTableDatas.NodeCount; i++)
                {
                    obj.ConditionInfoTableDatas[i] = ConditionInfoTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConditionInfoTableDatas = Array.Empty<ConditionInfoTableData>();
            }
            obj.NodeDatas.Add(subHeader_ConditionInfoTableDatas, obj.ConditionInfoTableDatas);
            TBL.SubHeaderMap.Add("ConditionInfoTableData", obj.ConditionInfoTableDatas);
            // 处理SubHeader关联数组: ConditionTypeParams
            SubHeader? subHeader_ConditionTypeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionTypeParam");
            if (subHeader_ConditionTypeParams != null)
            {
                br.BaseStream.Seek(subHeader_ConditionTypeParams.DataOffset, SeekOrigin.Begin);
                obj.ConditionTypeParams = new ConditionTypeParam[subHeader_ConditionTypeParams.NodeCount];
                for (var i = 0; i < subHeader_ConditionTypeParams.NodeCount; i++)
                {
                    obj.ConditionTypeParams[i] = ConditionTypeParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConditionTypeParams = Array.Empty<ConditionTypeParam>();
            }
            obj.NodeDatas.Add(subHeader_ConditionTypeParams, obj.ConditionTypeParams);

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
            ConditionInfoTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ConditionInfoTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ConditionInfoTableDatas
            SubHeader? subHeader_ConditionInfoTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionInfoTableData");
            if (subHeader_ConditionInfoTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_ConditionInfoTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConditionInfoTableDatas.NodeCount; i++)
                {
                    ConditionInfoTableDataHelper.Serialize(bw, obj.ConditionInfoTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ConditionTypeParams
            SubHeader? subHeader_ConditionTypeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionTypeParam");
            if (subHeader_ConditionTypeParams != null)
            {
                bw.BaseStream.Seek(subHeader_ConditionTypeParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConditionTypeParams.NodeCount; i++)
                {
                    ConditionTypeParamHelper.Serialize(bw, obj.ConditionTypeParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ConditionInfoTableDataHelper
    {
        public static ConditionInfoTableData DeSerialize(BinaryReader br)
        {
            var obj = new ConditionInfoTableData
            {
                Id = br.ReadUInt32(),
                Int1 = br.ReadUInt32(),
                StateName = br.ReadInt64(),
                Int2 = br.ReadUInt32(),
                Int3 = br.ReadUInt32(),
                Int4 = br.ReadUInt32(),
                Effects = new ConditionEffect[3]
            };
            for (var i = 0; i < 3; i++)
            {
                obj.Effects[i] = ConditionEffectHelper.DeSerialize(br);
            }
            obj.Float1 = br.ReadSingle();
            obj.Float2 = br.ReadSingle();
            obj.IconId = br.ReadInt32();
            obj.Int5 = br.ReadInt32();
            obj.Int6 = br.ReadInt32();
            obj.Int7 = br.ReadInt32();
            obj.Int8 = br.ReadInt32();
            obj.Flag = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConditionInfoTableData obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.StateName);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            for (var i = 0; i < 3; i++)
            {
                ConditionEffectHelper.Serialize(bw, obj.Effects[i]);
            }
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.IconId);
            bw.Write(obj.Int5);
            bw.Write(obj.Int6);
            bw.Write(obj.Int7);
            bw.Write(obj.Int8);
            bw.Write(obj.Flag);
        }
    }

    public static class ConditionTypeParamHelper
    {
        public static ConditionTypeParam DeSerialize(BinaryReader br)
        {
            var obj = new ConditionTypeParam
            {
                Id = br.ReadUInt32(),
                Value = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConditionTypeParam obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Value);
        }
    }

    public static class ConditionEffectHelper
    {
        public static ConditionEffect DeSerialize(BinaryReader br)
        {
            var obj = new ConditionEffect
            {
                Small = br.ReadInt32(),
                Medium = br.ReadInt32(),
                Large = br.ReadInt32(),
                Undefined = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConditionEffect obj)
        {
            bw.Write(obj.Small);
            bw.Write(obj.Medium);
            bw.Write(obj.Large);
            bw.Write(obj.Undefined);
        }
    }
}