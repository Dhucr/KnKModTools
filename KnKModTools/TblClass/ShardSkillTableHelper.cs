using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ShardSkillTableHelper
    {
        public static ShardSkillTable DeSerialize(BinaryReader br)
        {
            ShardSkillTable obj = TBLHelper.DeSerialize<ShardSkillTable>(br);
            var type = new ShardSkillType[]
            {
                new ShardSkillType() { Id = 0, Name = "武器" },
                new ShardSkillType() { Id = 1, Name = "护盾" },
                new ShardSkillType() { Id = 2, Name = "驱动" },
                new ShardSkillType() { Id = 3, Name = "EXTRA" }
            };
            TBL.SubHeaderMap.Add("ShardSkillType", type);
            // 处理SubHeader关联数组: ShardSkillParams
            SubHeader? subHeader_ShardSkillParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShardSkillParam");
            if (subHeader_ShardSkillParams != null)
            {
                br.BaseStream.Seek(subHeader_ShardSkillParams.DataOffset, SeekOrigin.Begin);
                obj.ShardSkillParams = new ShardSkillParam[subHeader_ShardSkillParams.NodeCount];
                for (var i = 0; i < subHeader_ShardSkillParams.NodeCount; i++)
                {
                    obj.ShardSkillParams[i] = ShardSkillParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ShardSkillParams = Array.Empty<ShardSkillParam>();
            }
            obj.NodeDatas.Add(subHeader_ShardSkillParams, obj.ShardSkillParams);
            TBL.SubHeaderMap.Add("ShardSkillParam", obj.ShardSkillParams);

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
            ShardSkillTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ShardSkillTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ShardSkillParams
            SubHeader? subHeader_ShardSkillParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShardSkillParam");
            if (subHeader_ShardSkillParams != null)
            {
                bw.BaseStream.Seek(subHeader_ShardSkillParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ShardSkillParams.NodeCount; i++)
                {
                    ShardSkillParamHelper.Serialize(bw, obj.ShardSkillParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ShardSkillParamHelper
    {
        public static ShardSkillParam DeSerialize(BinaryReader br)
        {
            var obj = new ShardSkillParam
            {
                Id = br.ReadUInt16(),
                EffectType = br.ReadByte(),
                Byte2 = br.ReadByte(),
                ElementEarth = br.ReadByte(),
                ElementWater = br.ReadByte(),
                ElementFire = br.ReadByte(),
                ElementWind = br.ReadByte(),
                ElementTime = br.ReadByte(),
                ElementSpace = br.ReadByte(),
                ElementMirage = br.ReadByte(),
                ActivationChance = br.ReadByte(),
                SclmActivationChance = br.ReadByte(),
                SboostActivationChance = br.ReadByte(),
                SboostSclmActivationChance = br.ReadByte(),
                FullBoostActivationChance = br.ReadByte(),
                Empty = br.ReadByte(),
                Byte3 = br.ReadByte(),
                UpgradeId = br.ReadUInt16(),
                Empty2 = br.ReadUInt32(),
                ArrayFlag = br.ReadInt64(),
                Count = br.ReadInt64(),
                Flag2 = br.ReadInt64(),
                ActivationCondition1 = br.ReadUInt32(),
                ActivationCondition2 = br.ReadUInt32(),
                Int1 = br.ReadUInt32(),
                Empty4 = br.ReadUInt32(),
                Short1 = br.ReadUInt16(),
                SkillSubstituteId = br.ReadUInt16(),
                Effects = new ShardSkillEffect[2]
            };
            for (var i = 0; i < 2; i++)
            {
                obj.Effects[i] = ShardSkillEffectHelper.DeSerialize(br);
            }
            obj.Int2 = br.ReadUInt32();
            obj.Animation = br.ReadInt64();
            obj.IconId = br.ReadUInt64();
            obj.Name = br.ReadInt64();
            obj.Description = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShardSkillParam obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.EffectType);
            bw.Write(obj.Byte2);
            bw.Write(obj.ElementEarth);
            bw.Write(obj.ElementWater);
            bw.Write(obj.ElementFire);
            bw.Write(obj.ElementWind);
            bw.Write(obj.ElementTime);
            bw.Write(obj.ElementSpace);
            bw.Write(obj.ElementMirage);
            bw.Write(obj.ActivationChance);
            bw.Write(obj.SclmActivationChance);
            bw.Write(obj.SboostActivationChance);
            bw.Write(obj.SboostSclmActivationChance);
            bw.Write(obj.FullBoostActivationChance);
            bw.Write(obj.Empty);
            bw.Write(obj.Byte3);
            bw.Write(obj.UpgradeId);
            bw.Write(obj.Empty2);
            bw.Write(obj.ArrayFlag);
            bw.Write(obj.Count);
            bw.Write(obj.Flag2);
            bw.Write(obj.ActivationCondition1);
            bw.Write(obj.ActivationCondition2);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty4);
            bw.Write(obj.Short1);
            bw.Write(obj.SkillSubstituteId);
            for (var i = 0; i < 2; i++)
            {
                ShardSkillEffectHelper.Serialize(bw, obj.Effects[i]);
            }
            bw.Write(obj.Int2);
            bw.Write(obj.Animation);
            bw.Write(obj.IconId);
            bw.Write(obj.Name);
            bw.Write(obj.Description);
        }
    }

    public static class ShardSkillEffectHelper
    {
        public static ShardSkillEffect DeSerialize(BinaryReader br)
        {
            var obj = new ShardSkillEffect
            {
                ID = br.ReadInt32(),
                Param1 = br.ReadInt32(),
                Param2 = br.ReadInt32(),
                Param3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShardSkillEffect obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.Param1);
            bw.Write(obj.Param2);
            bw.Write(obj.Param3);
        }
    }
}