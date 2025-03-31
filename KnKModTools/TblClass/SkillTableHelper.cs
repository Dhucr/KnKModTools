using KnKModTools.Localization;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class SkillTableHelper
    {
        public static SkillTable DeSerialize(BinaryReader br)
        {
            SkillTable obj = TBLHelper.DeSerialize<SkillTable>(br);
            // 处理SubHeader关联数组: SkillParams
            SubHeader? subHeader_SkillParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillParam");
            if (subHeader_SkillParams != null)
            {
                br.BaseStream.Seek(subHeader_SkillParams.DataOffset, SeekOrigin.Begin);
                obj.SkillParams = new SkillParam[subHeader_SkillParams.NodeCount];
                for (var i = 0; i < subHeader_SkillParams.NodeCount; i++)
                {
                    obj.SkillParams[i] = SkillParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillParams = Array.Empty<SkillParam>();
            }
            obj.NodeDatas.Add(subHeader_SkillParams, obj.SkillParams);
            TBL.SubHeaderMap.Add("SkillParam", obj.SkillParams);
            // 处理SubHeader关联数组: SkillPowerIcons
            SubHeader? subHeader_SkillPowerIcons = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillPowerIcon");
            if (subHeader_SkillPowerIcons != null)
            {
                br.BaseStream.Seek(subHeader_SkillPowerIcons.DataOffset, SeekOrigin.Begin);
                obj.SkillPowerIcons = new SkillPowerIcon[subHeader_SkillPowerIcons.NodeCount];
                for (var i = 0; i < subHeader_SkillPowerIcons.NodeCount; i++)
                {
                    obj.SkillPowerIcons[i] = SkillPowerIconHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillPowerIcons = Array.Empty<SkillPowerIcon>();
            }
            obj.NodeDatas.Add(subHeader_SkillPowerIcons, obj.SkillPowerIcons);
            TBL.SubHeaderMap.Add("SkillPowerIcon", obj.SkillPowerIcons);
            // 处理SubHeader关联数组: SkillGetParams
            SubHeader? subHeader_SkillGetParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillGetParam");
            if (subHeader_SkillGetParams != null)
            {
                br.BaseStream.Seek(subHeader_SkillGetParams.DataOffset, SeekOrigin.Begin);
                obj.SkillGetParams = new SkillGetParam[subHeader_SkillGetParams.NodeCount];
                for (var i = 0; i < subHeader_SkillGetParams.NodeCount; i++)
                {
                    obj.SkillGetParams[i] = SkillGetParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillGetParams = Array.Empty<SkillGetParam>();
            }
            obj.NodeDatas.Add(subHeader_SkillGetParams, obj.SkillGetParams);
            TBL.SubHeaderMap.Add("SkillGetParam", obj.SkillGetParams);
            // 处理SubHeader关联数组: SkillChangeParams
            SubHeader? subHeader_SkillChangeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillChangeParam");
            if (subHeader_SkillChangeParams != null)
            {
                br.BaseStream.Seek(subHeader_SkillChangeParams.DataOffset, SeekOrigin.Begin);
                obj.SkillChangeParams = new SkillChangeParam[subHeader_SkillChangeParams.NodeCount];
                for (var i = 0; i < subHeader_SkillChangeParams.NodeCount; i++)
                {
                    obj.SkillChangeParams[i] = SkillChangeParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillChangeParams = Array.Empty<SkillChangeParam>();
            }
            obj.NodeDatas.Add(subHeader_SkillChangeParams, obj.SkillChangeParams);
            TBL.SubHeaderMap.Add("SkillChangeParam", obj.SkillChangeParams);

            var rangeArray = new RangeTypeTable[]
            {
                new RangeTypeTable(0, LanguageManager.GetString("Null")),
                new RangeTypeTable(257, LanguageManager.GetString("Monomer")),
                new RangeTypeTable(265, LanguageManager.GetString("MoveMonomer")),
                new RangeTypeTable(277, LanguageManager.GetString("GoalLine")),
                new RangeTypeTable(278, LanguageManager.GetString("PointLine")),
                new RangeTypeTable(286, LanguageManager.GetString("MovePointLine")),
                new RangeTypeTable(293, LanguageManager.GetString("Circle")),
                new RangeTypeTable(4388, LanguageManager.GetString("Sector")),
                new RangeTypeTable(166, LanguageManager.GetString("All")),
                new RangeTypeTable(301, LanguageManager.GetString("Unknown"))
            };
            TBL.SubHeaderMap.Add("RangeTypeTable", rangeArray);

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
            SkillTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not SkillTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: SkillParams
            SubHeader? subHeader_SkillParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillParam");
            if (subHeader_SkillParams != null)
            {
                bw.BaseStream.Seek(subHeader_SkillParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillParams.NodeCount; i++)
                {
                    SkillParamHelper.Serialize(bw, obj.SkillParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillPowerIcons
            SubHeader? subHeader_SkillPowerIcons = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillPowerIcon");
            if (subHeader_SkillPowerIcons != null)
            {
                bw.BaseStream.Seek(subHeader_SkillPowerIcons.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillPowerIcons.NodeCount; i++)
                {
                    SkillPowerIconHelper.Serialize(bw, obj.SkillPowerIcons[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillGetParams
            SubHeader? subHeader_SkillGetParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillGetParam");
            if (subHeader_SkillGetParams != null)
            {
                bw.BaseStream.Seek(subHeader_SkillGetParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillGetParams.NodeCount; i++)
                {
                    SkillGetParamHelper.Serialize(bw, obj.SkillGetParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillChangeParams
            SubHeader? subHeader_SkillChangeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillChangeParam");
            if (subHeader_SkillChangeParams != null)
            {
                bw.BaseStream.Seek(subHeader_SkillChangeParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillChangeParams.NodeCount; i++)
                {
                    SkillChangeParamHelper.Serialize(bw, obj.SkillChangeParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class SkillParamHelper
    {
        public static SkillParam DeSerialize(BinaryReader br)
        {
            var obj = new SkillParam
            {
                Id = br.ReadUInt16(),
                CharacterRestriction = br.ReadInt32(),
                Short = br.ReadInt16(),
                Flag = br.ReadInt64(),
                Category = br.ReadByte(),
                Element = br.ReadByte(),
                Empty1 = br.ReadInt32(),
                Empty2 = br.ReadInt16(),
                Flag2 = br.ReadInt64(),
                RangeType = br.ReadInt32(),
                RangeMove = br.ReadSingle(),
                RangeAttack = br.ReadSingle(),
                RangeAngle = br.ReadSingle(),
                Effects = new Effect[5]
            };
            for (var i = 0; i < 5; i++)
            {
                obj.Effects[i] = EffectHelper.DeSerialize(br);
            }
            obj.StunChance = br.ReadSingle();
            obj.CastDelay = br.ReadUInt16();
            obj.RecoveryDelay = br.ReadUInt16();
            obj.Cost = br.ReadUInt16();
            obj.LevelLearn = br.ReadInt16();
            obj.SortId = br.ReadUInt16();
            obj.Data = br.ReadInt16();
            obj.Animation = br.ReadInt64();
            obj.Name = br.ReadInt64();
            obj.Description1 = br.ReadInt64();
            obj.Description2 = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillParam obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.CharacterRestriction);
            bw.Write(obj.Short);
            bw.Write(obj.Flag);
            bw.Write(obj.Category);
            bw.Write(obj.Element);
            bw.Write(obj.Empty1);
            bw.Write(obj.Empty2);
            bw.Write(obj.Flag2);
            bw.Write(obj.RangeType);
            bw.Write(obj.RangeMove);
            bw.Write(obj.RangeAttack);
            bw.Write(obj.RangeAngle);
            for (var i = 0; i < 5; i++)
            {
                EffectHelper.Serialize(bw, obj.Effects[i]);
            }
            bw.Write(obj.StunChance);
            bw.Write(obj.CastDelay);
            bw.Write(obj.RecoveryDelay);
            bw.Write(obj.Cost);
            bw.Write(obj.LevelLearn);
            bw.Write(obj.SortId);
            bw.Write(obj.Data);
            bw.Write(obj.Animation);
            bw.Write(obj.Name);
            bw.Write(obj.Description1);
            bw.Write(obj.Description2);
        }
    }

    public static class SkillPowerIconHelper
    {
        public static SkillPowerIcon DeSerialize(BinaryReader br)
        {
            var obj = new SkillPowerIcon
            {
                SkillPower = br.ReadInt32(),
                IconId = br.ReadInt32(),
                PowerText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillPowerIcon obj)
        {
            bw.Write(obj.SkillPower);
            bw.Write(obj.IconId);
            bw.Write(obj.PowerText);
        }
    }

    public static class SkillGetParamHelper
    {
        public static SkillGetParam DeSerialize(BinaryReader br)
        {
            var obj = new SkillGetParam
            {
                CharacterId = br.ReadUInt16(),
                SkillId1 = br.ReadUInt16(),
                SkillId2 = br.ReadUInt16(),
                SkillId3 = br.ReadUInt16(),
                SkillId4 = br.ReadUInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillGetParam obj)
        {
            bw.Write(obj.CharacterId);
            bw.Write(obj.SkillId1);
            bw.Write(obj.SkillId2);
            bw.Write(obj.SkillId3);
            bw.Write(obj.SkillId4);
        }
    }

    public static class SkillChangeParamHelper
    {
        public static SkillChangeParam DeSerialize(BinaryReader br)
        {
            var obj = new SkillChangeParam
            {
                CharacterId = br.ReadUInt16(),
                Data = br.ReadInt16(),
                SkillId1 = br.ReadUInt16(),
                SkillId2 = br.ReadUInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillChangeParam obj)
        {
            bw.Write(obj.CharacterId);
            bw.Write(obj.Data);
            bw.Write(obj.SkillId1);
            bw.Write(obj.SkillId2);
        }
    }
}