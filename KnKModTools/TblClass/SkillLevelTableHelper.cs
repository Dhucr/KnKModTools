using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class SkillLevelTableHelper
    {
        public static SkillLevelTable DeSerialize(BinaryReader br)
        {
            SkillLevelTable obj = TBLHelper.DeSerialize<SkillLevelTable>(br);
            // 处理SubHeader关联数组: SkillLevelConstants
            SubHeader? subHeader_SkillLevelConstants = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelConstant");
            if (subHeader_SkillLevelConstants != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelConstants.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelConstants = new SkillLevelConstant[subHeader_SkillLevelConstants.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelConstants.NodeCount; i++)
                {
                    obj.SkillLevelConstants[i] = SkillLevelConstantHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelConstants = Array.Empty<SkillLevelConstant>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelConstants, obj.SkillLevelConstants);
            // 处理SubHeader关联数组: SkillLevelExpDatas
            SubHeader? subHeader_SkillLevelExpDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelExpData");
            if (subHeader_SkillLevelExpDatas != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelExpDatas.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelExpDatas = new SkillLevelExpData[subHeader_SkillLevelExpDatas.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelExpDatas.NodeCount; i++)
                {
                    obj.SkillLevelExpDatas[i] = SkillLevelExpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelExpDatas = Array.Empty<SkillLevelExpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelExpDatas, obj.SkillLevelExpDatas);
            // 处理SubHeader关联数组: SkillLevelExpCorrectDatas
            SubHeader? subHeader_SkillLevelExpCorrectDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelExpCorrectData");
            if (subHeader_SkillLevelExpCorrectDatas != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelExpCorrectDatas.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelExpCorrectDatas = new SkillLevelExpCorrectData[subHeader_SkillLevelExpCorrectDatas.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelExpCorrectDatas.NodeCount; i++)
                {
                    obj.SkillLevelExpCorrectDatas[i] = SkillLevelExpCorrectDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelExpCorrectDatas = Array.Empty<SkillLevelExpCorrectData>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelExpCorrectDatas, obj.SkillLevelExpCorrectDatas);
            // 处理SubHeader关联数组: SkillLevelParams
            SubHeader? subHeader_SkillLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelParam");
            if (subHeader_SkillLevelParams != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelParams.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelParams = new SkillLevelParam[subHeader_SkillLevelParams.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelParams.NodeCount; i++)
                {
                    obj.SkillLevelParams[i] = SkillLevelParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelParams = Array.Empty<SkillLevelParam>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelParams, obj.SkillLevelParams);
            // 处理SubHeader关联数组: SkillLevelSkillSettings
            SubHeader? subHeader_SkillLevelSkillSettings = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelSkillSetting");
            if (subHeader_SkillLevelSkillSettings != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelSkillSettings.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelSkillSettings = new SkillLevelSkillSetting[subHeader_SkillLevelSkillSettings.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelSkillSettings.NodeCount; i++)
                {
                    obj.SkillLevelSkillSettings[i] = SkillLevelSkillSettingHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelSkillSettings = Array.Empty<SkillLevelSkillSetting>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelSkillSettings, obj.SkillLevelSkillSettings);
            // 处理SubHeader关联数组: SkillLevelFactorDefines
            SubHeader? subHeader_SkillLevelFactorDefines = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelFactorDefine");
            if (subHeader_SkillLevelFactorDefines != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelFactorDefines.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelFactorDefines = new SkillLevelFactorDefine[subHeader_SkillLevelFactorDefines.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelFactorDefines.NodeCount; i++)
                {
                    obj.SkillLevelFactorDefines[i] = SkillLevelFactorDefineHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelFactorDefines = Array.Empty<SkillLevelFactorDefine>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelFactorDefines, obj.SkillLevelFactorDefines);

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
            SkillLevelTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not SkillLevelTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: SkillLevelConstants
            SubHeader? subHeader_SkillLevelConstants = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelConstant");
            if (subHeader_SkillLevelConstants != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelConstants.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelConstants.NodeCount; i++)
                {
                    SkillLevelConstantHelper.Serialize(bw, obj.SkillLevelConstants[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelExpDatas
            SubHeader? subHeader_SkillLevelExpDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelExpData");
            if (subHeader_SkillLevelExpDatas != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelExpDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelExpDatas.NodeCount; i++)
                {
                    SkillLevelExpDataHelper.Serialize(bw, obj.SkillLevelExpDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelExpCorrectDatas
            SubHeader? subHeader_SkillLevelExpCorrectDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelExpCorrectData");
            if (subHeader_SkillLevelExpCorrectDatas != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelExpCorrectDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelExpCorrectDatas.NodeCount; i++)
                {
                    SkillLevelExpCorrectDataHelper.Serialize(bw, obj.SkillLevelExpCorrectDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelParams
            SubHeader? subHeader_SkillLevelParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelParam");
            if (subHeader_SkillLevelParams != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelParams.NodeCount; i++)
                {
                    SkillLevelParamHelper.Serialize(bw, obj.SkillLevelParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelSkillSettings
            SubHeader? subHeader_SkillLevelSkillSettings = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelSkillSetting");
            if (subHeader_SkillLevelSkillSettings != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelSkillSettings.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelSkillSettings.NodeCount; i++)
                {
                    SkillLevelSkillSettingHelper.Serialize(bw, obj.SkillLevelSkillSettings[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelFactorDefines
            SubHeader? subHeader_SkillLevelFactorDefines = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelFactorDefine");
            if (subHeader_SkillLevelFactorDefines != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelFactorDefines.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelFactorDefines.NodeCount; i++)
                {
                    SkillLevelFactorDefineHelper.Serialize(bw, obj.SkillLevelFactorDefines[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class SkillLevelConstantHelper
    {
        public static SkillLevelConstant DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelConstant
            {
                Int1 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelConstant obj)
        {
            bw.Write(obj.Int1);
        }
    }

    public static class SkillLevelExpDataHelper
    {
        public static SkillLevelExpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelExpData
            {
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelExpData obj)
        {
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
        }
    }

    public static class SkillLevelExpCorrectDataHelper
    {
        public static SkillLevelExpCorrectData DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelExpCorrectData
            {
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelExpCorrectData obj)
        {
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
        }
    }

    public static class SkillLevelParamHelper
    {
        public static SkillLevelParam DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelParam
            {
                ChrId = br.ReadUInt16(),
                SkillId = br.ReadUInt16(),
                UpgradeTypeId = br.ReadUInt16(),
                Lv1Param = br.ReadInt16(),
                Lvl2Param = br.ReadInt16(),
                Lvl3Param = br.ReadInt16(),
                Lvl4Param = br.ReadInt16(),
                Lvl5Param = br.ReadInt16(),
                Lvl6Param = br.ReadInt16(),
                Lvl7Param = br.ReadInt16(),
                Lvl8Param = br.ReadInt16(),
                Lvl9Param = br.ReadInt16(),
                Lvl10Param = br.ReadInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelParam obj)
        {
            bw.Write(obj.ChrId);
            bw.Write(obj.SkillId);
            bw.Write(obj.UpgradeTypeId);
            bw.Write(obj.Lv1Param);
            bw.Write(obj.Lvl2Param);
            bw.Write(obj.Lvl3Param);
            bw.Write(obj.Lvl4Param);
            bw.Write(obj.Lvl5Param);
            bw.Write(obj.Lvl6Param);
            bw.Write(obj.Lvl7Param);
            bw.Write(obj.Lvl8Param);
            bw.Write(obj.Lvl9Param);
            bw.Write(obj.Lvl10Param);
        }
    }

    public static class SkillLevelSkillSettingHelper
    {
        public static SkillLevelSkillSetting DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelSkillSetting
            {
                ChrId = br.ReadUInt16(),
                SkillId = br.ReadUInt16(),
                Empty1 = br.ReadUInt16(),
                Empty2 = br.ReadUInt16(),
                Toffset1 = br.ReadInt64(),
                Toffset2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelSkillSetting obj)
        {
            bw.Write(obj.ChrId);
            bw.Write(obj.SkillId);
            bw.Write(obj.Empty1);
            bw.Write(obj.Empty2);
            bw.Write(obj.Toffset1);
            bw.Write(obj.Toffset2);
        }
    }

    public static class SkillLevelFactorDefineHelper
    {
        public static SkillLevelFactorDefine DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelFactorDefine
            {
                Id = br.ReadUInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Array1 = br.ReadInt64(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Long2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelFactorDefine obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
            bw.Write(obj.Long1);
            bw.Write(obj.Array1);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Long2);
        }
    }
}