using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class HollowCoreTableHelper
    {
        public static HollowCoreTable DeSerialize(BinaryReader br)
        {
            HollowCoreTable obj = TBLHelper.DeSerialize<HollowCoreTable>(br);
            // 处理SubHeader关联数组: HollowCoreBaseParams
            SubHeader? subHeader_HollowCoreBaseParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreBaseParam");
            if (subHeader_HollowCoreBaseParams != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreBaseParams.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreBaseParams = new HollowCoreBaseParam[subHeader_HollowCoreBaseParams.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreBaseParams.NodeCount; i++)
                {
                    obj.HollowCoreBaseParams[i] = HollowCoreBaseParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreBaseParams = Array.Empty<HollowCoreBaseParam>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreBaseParams, obj.HollowCoreBaseParams);
            // 处理SubHeader关联数组: HollowCoreLevelParams
            SubHeader? subHeader_HollowCoreLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreLevelParam");
            if (subHeader_HollowCoreLevelParams != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreLevelParams.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreLevelParams = new HollowCoreLevelParam[subHeader_HollowCoreLevelParams.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreLevelParams.NodeCount; i++)
                {
                    obj.HollowCoreLevelParams[i] = HollowCoreLevelParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreLevelParams = Array.Empty<HollowCoreLevelParam>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreLevelParams, obj.HollowCoreLevelParams);
            // 处理SubHeader关联数组: HollowCoreEffParams
            SubHeader? subHeader_HollowCoreEffParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreEffParam");
            if (subHeader_HollowCoreEffParams != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreEffParams.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreEffParams = new HollowCoreEffParam[subHeader_HollowCoreEffParams.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreEffParams.NodeCount; i++)
                {
                    obj.HollowCoreEffParams[i] = HollowCoreEffParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreEffParams = Array.Empty<HollowCoreEffParam>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreEffParams, obj.HollowCoreEffParams);
            // 处理SubHeader关联数组: HollowCoreEffTexts
            SubHeader? subHeader_HollowCoreEffTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreEffText");
            if (subHeader_HollowCoreEffTexts != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreEffTexts.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreEffTexts = new HollowCoreEffText[subHeader_HollowCoreEffTexts.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreEffTexts.NodeCount; i++)
                {
                    obj.HollowCoreEffTexts[i] = HollowCoreEffTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreEffTexts = Array.Empty<HollowCoreEffText>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreEffTexts, obj.HollowCoreEffTexts);
            TBL.SubHeaderMap.Add("HollowCoreEffText", obj.HollowCoreEffTexts);
            // 处理SubHeader关联数组: HollowCoreConvertLevelParams
            SubHeader? subHeader_HollowCoreConvertLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreConvertLevelParam");
            if (subHeader_HollowCoreConvertLevelParams != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreConvertLevelParams.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreConvertLevelParams = new HollowCoreConvertLevelParam[subHeader_HollowCoreConvertLevelParams.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreConvertLevelParams.NodeCount; i++)
                {
                    obj.HollowCoreConvertLevelParams[i] = HollowCoreConvertLevelParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreConvertLevelParams = Array.Empty<HollowCoreConvertLevelParam>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreConvertLevelParams, obj.HollowCoreConvertLevelParams);
            // 处理SubHeader关联数组: HollowCoreCalcLevelParams
            SubHeader? subHeader_HollowCoreCalcLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreCalcLevelParam");
            if (subHeader_HollowCoreCalcLevelParams != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreCalcLevelParams.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreCalcLevelParams = new HollowCoreCalcLevelParam[subHeader_HollowCoreCalcLevelParams.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreCalcLevelParams.NodeCount; i++)
                {
                    obj.HollowCoreCalcLevelParams[i] = HollowCoreCalcLevelParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreCalcLevelParams = Array.Empty<HollowCoreCalcLevelParam>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreCalcLevelParams, obj.HollowCoreCalcLevelParams);
            // 处理SubHeader关联数组: HollowCoreVoices
            SubHeader? subHeader_HollowCoreVoices = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreVoice");
            if (subHeader_HollowCoreVoices != null)
            {
                br.BaseStream.Seek(subHeader_HollowCoreVoices.DataOffset, SeekOrigin.Begin);
                obj.HollowCoreVoices = new HollowCoreVoice[subHeader_HollowCoreVoices.NodeCount];
                for (var i = 0; i < subHeader_HollowCoreVoices.NodeCount; i++)
                {
                    obj.HollowCoreVoices[i] = HollowCoreVoiceHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HollowCoreVoices = Array.Empty<HollowCoreVoice>();
            }
            obj.NodeDatas.Add(subHeader_HollowCoreVoices, obj.HollowCoreVoices);

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
            HollowCoreTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not HollowCoreTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: HollowCoreBaseParams
            SubHeader? subHeader_HollowCoreBaseParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreBaseParam");
            if (subHeader_HollowCoreBaseParams != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreBaseParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreBaseParams.NodeCount; i++)
                {
                    HollowCoreBaseParamHelper.Serialize(bw, obj.HollowCoreBaseParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreLevelParams
            SubHeader? subHeader_HollowCoreLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreLevelParam");
            if (subHeader_HollowCoreLevelParams != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreLevelParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreLevelParams.NodeCount; i++)
                {
                    HollowCoreLevelParamHelper.Serialize(bw, obj.HollowCoreLevelParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreEffParams
            SubHeader? subHeader_HollowCoreEffParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreEffParam");
            if (subHeader_HollowCoreEffParams != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreEffParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreEffParams.NodeCount; i++)
                {
                    HollowCoreEffParamHelper.Serialize(bw, obj.HollowCoreEffParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreEffTexts
            SubHeader? subHeader_HollowCoreEffTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreEffText");
            if (subHeader_HollowCoreEffTexts != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreEffTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreEffTexts.NodeCount; i++)
                {
                    HollowCoreEffTextHelper.Serialize(bw, obj.HollowCoreEffTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreConvertLevelParams
            SubHeader? subHeader_HollowCoreConvertLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreConvertLevelParam");
            if (subHeader_HollowCoreConvertLevelParams != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreConvertLevelParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreConvertLevelParams.NodeCount; i++)
                {
                    HollowCoreConvertLevelParamHelper.Serialize(bw, obj.HollowCoreConvertLevelParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreCalcLevelParams
            SubHeader? subHeader_HollowCoreCalcLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreCalcLevelParam");
            if (subHeader_HollowCoreCalcLevelParams != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreCalcLevelParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreCalcLevelParams.NodeCount; i++)
                {
                    HollowCoreCalcLevelParamHelper.Serialize(bw, obj.HollowCoreCalcLevelParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: HollowCoreVoices
            SubHeader? subHeader_HollowCoreVoices = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HollowCoreVoice");
            if (subHeader_HollowCoreVoices != null)
            {
                bw.BaseStream.Seek(subHeader_HollowCoreVoices.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HollowCoreVoices.NodeCount; i++)
                {
                    HollowCoreVoiceHelper.Serialize(bw, obj.HollowCoreVoices[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class HollowCoreBaseParamHelper
    {
        public static HollowCoreBaseParam DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreBaseParam
            {
                ItemId = br.ReadUInt32(),
                InitialLevel = br.ReadUInt32(),
                MaxLevel = br.ReadUInt32(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Empty1 = br.ReadInt16(),
                Flag = br.ReadInt64(),
                SBoostAbilityType = br.ReadUInt64(),
                SBoostAbilityName = br.ReadInt64(),
                Array1 = br.ReadInt64(),
                Count1 = br.ReadUInt32(),
                VoiceId = br.ReadUInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Empty2 = br.ReadUInt32(),
                Float4 = br.ReadSingle(),
                Empty3 = br.ReadInt32(),
                Array2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Name = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreBaseParam obj)
        {
            bw.Write(obj.ItemId);
            bw.Write(obj.InitialLevel);
            bw.Write(obj.MaxLevel);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Empty1);
            bw.Write(obj.Flag);
            bw.Write(obj.SBoostAbilityType);
            bw.Write(obj.SBoostAbilityName);
            bw.Write(obj.Array1);
            bw.Write(obj.Count1);
            bw.Write(obj.VoiceId);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Empty2);
            bw.Write(obj.Float4);
            bw.Write(obj.Empty3);
            bw.Write(obj.Array2);
            bw.Write(obj.Count2);
            bw.Write(obj.Name);
        }
    }

    public static class HollowCoreLevelParamHelper
    {
        public static HollowCoreLevelParam DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreLevelParam
            {
                ItemId = br.ReadUInt32(),
                Level = br.ReadUInt32(),
                Exp = br.ReadInt32(),
                MagicAttack = br.ReadUInt32(),
                Ep = br.ReadUInt32(),
                Effects = new HollowCoreEffect[7]
            };
            for (var i = 0; i < 7; i++)
            {
                obj.Effects[i] = HollowCoreEffectHelper.DeSerialize(br);
            }
            obj.Description = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreLevelParam obj)
        {
            bw.Write(obj.ItemId);
            bw.Write(obj.Level);
            bw.Write(obj.Exp);
            bw.Write(obj.MagicAttack);
            bw.Write(obj.Ep);
            for (var i = 0; i < 7; i++)
            {
                HollowCoreEffectHelper.Serialize(bw, obj.Effects[i]);
            }
            bw.Write(obj.Description);
        }
    }

    public static class HollowCoreEffParamHelper
    {
        public static HollowCoreEffParam DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreEffParam
            {
                Id = br.ReadUInt32(),
                IdReference = br.ReadUInt32(),
                Long1 = br.ReadUInt64(),
                EffectName = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreEffParam obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.IdReference);
            bw.Write(obj.Long1);
            bw.Write(obj.EffectName);
        }
    }

    public static class HollowCoreEffTextHelper
    {
        public static HollowCoreEffText DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreEffText
            {
                Id = br.ReadUInt64(),
                EffectDescription = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreEffText obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.EffectDescription);
        }
    }

    public static class HollowCoreConvertLevelParamHelper
    {
        public static HollowCoreConvertLevelParam DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreConvertLevelParam
            {
                Id = br.ReadUInt32(),
                Value = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreConvertLevelParam obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Value);
        }
    }

    public static class HollowCoreCalcLevelParamHelper
    {
        public static HollowCoreCalcLevelParam DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreCalcLevelParam
            {
                Int1 = br.ReadUInt32(),
                Float1 = br.ReadSingle(),
                Int2 = br.ReadUInt32(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreCalcLevelParam obj)
        {
            bw.Write(obj.Int1);
            bw.Write(obj.Float1);
            bw.Write(obj.Int2);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
        }
    }

    public static class HollowCoreVoiceHelper
    {
        public static HollowCoreVoice DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreVoice
            {
                Id = br.ReadInt16(),
                Number = br.ReadUInt32(),
                Empty = br.ReadUInt16(),
                Array = br.ReadInt64(),
                Count = br.ReadUInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreVoice obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Number);
            bw.Write(obj.Empty);
            bw.Write(obj.Array);
            bw.Write(obj.Count);
        }
    }

    public static class HollowCoreEffectHelper
    {
        public static HollowCoreEffect DeSerialize(BinaryReader br)
        {
            var obj = new HollowCoreEffect
            {
                ID = br.ReadInt32(),
                Param1 = br.ReadInt32(),
                Param2 = br.ReadInt32(),
                Param3 = br.ReadInt32(),
                Param4 = br.ReadInt32(),
                Param5 = br.ReadInt32(),
                Param6 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HollowCoreEffect obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.Param1);
            bw.Write(obj.Param2);
            bw.Write(obj.Param3);
            bw.Write(obj.Param4);
            bw.Write(obj.Param5);
            bw.Write(obj.Param6);
        }
    }
}