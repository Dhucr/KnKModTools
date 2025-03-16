using KnKModTools.Helper;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ItemHelpTableHelper
    {
        public static TBL DeSerialize(BinaryReader br)
        {
            ItemHelpTable obj = TBLHelper.DeSerialize<ItemHelpTable>(br);
            // 处理SubHeader关联数组: ConditionHelpList
            SubHeader? subHeader_ConditionHelpList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionHelpData");
            if (subHeader_ConditionHelpList != null)
            {
                br.BaseStream.Seek(subHeader_ConditionHelpList.DataOffset, SeekOrigin.Begin);
                obj.ConditionHelpList = new ConditionHelpData[subHeader_ConditionHelpList.NodeCount];
                for (var i = 0; i < subHeader_ConditionHelpList.NodeCount; i++)
                {
                    obj.ConditionHelpList[i] = ConditionHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConditionHelpList = Array.Empty<ConditionHelpData>();
            }
            obj.NodeDatas.Add(subHeader_ConditionHelpList, obj.ConditionHelpList);
            TBL.SubHeaderMap.Add("ConditionHelpData", obj.ConditionHelpList);

            // 处理SubHeader关联数组: SkillEffectTypeList
            SubHeader? subHeader_SkillEffectTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillEffectTypeHelpData");
            if (subHeader_SkillEffectTypeList != null)
            {
                br.BaseStream.Seek(subHeader_SkillEffectTypeList.DataOffset, SeekOrigin.Begin);
                obj.SkillEffectTypeList = new SkillEffectTypeHelpData[subHeader_SkillEffectTypeList.NodeCount];
                for (var i = 0; i < subHeader_SkillEffectTypeList.NodeCount; i++)
                {
                    obj.SkillEffectTypeList[i] = SkillEffectTypeHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillEffectTypeList = Array.Empty<SkillEffectTypeHelpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillEffectTypeList, obj.SkillEffectTypeList);
            TBL.SubHeaderMap.Add("SkillEffectTypeHelpData", obj.SkillEffectTypeList);

            // 处理SubHeader关联数组: SkillEffectList
            SubHeader? subHeader_SkillEffectList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillEffectHelpData");
            if (subHeader_SkillEffectList != null)
            {
                br.BaseStream.Seek(subHeader_SkillEffectList.DataOffset, SeekOrigin.Begin);
                obj.SkillEffectList = new SkillEffectHelpData[subHeader_SkillEffectList.NodeCount];
                for (var i = 0; i < subHeader_SkillEffectList.NodeCount; i++)
                {
                    obj.SkillEffectList[i] = SkillEffectHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillEffectList = Array.Empty<SkillEffectHelpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillEffectList, obj.SkillEffectList);
            TBL.SubHeaderMap.Add("SkillEffectHelpData", obj.SkillEffectList);

            // 处理SubHeader关联数组: AttrTypeList
            SubHeader? subHeader_AttrTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AttrTypeHelpData");
            if (subHeader_AttrTypeList != null)
            {
                br.BaseStream.Seek(subHeader_AttrTypeList.DataOffset, SeekOrigin.Begin);
                obj.AttrTypeList = new AttrTypeHelpData[subHeader_AttrTypeList.NodeCount];
                for (var i = 0; i < subHeader_AttrTypeList.NodeCount; i++)
                {
                    obj.AttrTypeList[i] = AttrTypeHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.AttrTypeList = Array.Empty<AttrTypeHelpData>();
            }
            obj.NodeDatas.Add(subHeader_AttrTypeList, obj.AttrTypeList);
            TBL.SubHeaderMap.Add("AttrTypeHelpData", obj.AttrTypeList);

            // 处理SubHeader关联数组: SkillTypeList
            SubHeader? subHeader_SkillTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillTypeHelpData");
            if (subHeader_SkillTypeList != null)
            {
                br.BaseStream.Seek(subHeader_SkillTypeList.DataOffset, SeekOrigin.Begin);
                obj.SkillTypeList = new SkillTypeHelpData[subHeader_SkillTypeList.NodeCount];
                for (var i = 0; i < subHeader_SkillTypeList.NodeCount; i++)
                {
                    obj.SkillTypeList[i] = SkillTypeHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillTypeList = Array.Empty<SkillTypeHelpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillTypeList, obj.SkillTypeList);
            TBL.SubHeaderMap.Add("SkillTypeHelpData", obj.SkillTypeList);

            // 处理SubHeader关联数组: SkillRangeList
            SubHeader? subHeader_SkillRangeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillRangeHelpData");
            if (subHeader_SkillRangeList != null)
            {
                br.BaseStream.Seek(subHeader_SkillRangeList.DataOffset, SeekOrigin.Begin);
                obj.SkillRangeList = new SkillRangeHelpData[subHeader_SkillRangeList.NodeCount];
                for (var i = 0; i < subHeader_SkillRangeList.NodeCount; i++)
                {
                    obj.SkillRangeList[i] = SkillRangeHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillRangeList = Array.Empty<SkillRangeHelpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillRangeList, obj.SkillRangeList);
            TBL.SubHeaderMap.Add("SkillRangeHelpData", obj.SkillRangeList);

            // 处理SubHeader关联数组: ItemKindList
            SubHeader? subHeader_ItemKindList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemKindHelpData");
            if (subHeader_ItemKindList != null)
            {
                br.BaseStream.Seek(subHeader_ItemKindList.DataOffset, SeekOrigin.Begin);
                obj.ItemKindList = new ItemKindHelpData[subHeader_ItemKindList.NodeCount];
                for (var i = 0; i < subHeader_ItemKindList.NodeCount; i++)
                {
                    obj.ItemKindList[i] = ItemKindHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ItemKindList = Array.Empty<ItemKindHelpData>();
            }
            obj.NodeDatas.Add(subHeader_ItemKindList, obj.ItemKindList);
            TBL.SubHeaderMap.Add("ItemKindHelpData", obj.ItemKindList);

            // 处理SubHeader关联数组: SkillItemStatusList
            SubHeader? subHeader_SkillItemStatusList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillItemStatusData");
            if (subHeader_SkillItemStatusList != null)
            {
                br.BaseStream.Seek(subHeader_SkillItemStatusList.DataOffset, SeekOrigin.Begin);
                obj.SkillItemStatusList = new SkillItemStatusData[subHeader_SkillItemStatusList.NodeCount];
                for (var i = 0; i < subHeader_SkillItemStatusList.NodeCount; i++)
                {
                    obj.SkillItemStatusList[i] = SkillItemStatusDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillItemStatusList = Array.Empty<SkillItemStatusData>();
            }
            obj.NodeDatas.Add(subHeader_SkillItemStatusList, obj.SkillItemStatusList);
            TBL.SubHeaderMap.Add("SkillItemStatusData", obj.SkillItemStatusList);

            // 处理SubHeader关联数组: EquipLineList
            SubHeader? subHeader_EquipLineList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EquipLineHelpData");
            if (subHeader_EquipLineList != null)
            {
                br.BaseStream.Seek(subHeader_EquipLineList.DataOffset, SeekOrigin.Begin);
                obj.EquipLineList = new EquipLineHelpData[subHeader_EquipLineList.NodeCount];
                for (var i = 0; i < subHeader_EquipLineList.NodeCount; i++)
                {
                    obj.EquipLineList[i] = EquipLineHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EquipLineList = Array.Empty<EquipLineHelpData>();
            }
            obj.NodeDatas.Add(subHeader_EquipLineList, obj.EquipLineList);
            TBL.SubHeaderMap.Add("EquipLineHelpData", obj.EquipLineList);

            // 处理SubHeader关联数组: SkillConnectList
            SubHeader? subHeader_SkillConnectList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillConnectListData");
            if (subHeader_SkillConnectList != null)
            {
                br.BaseStream.Seek(subHeader_SkillConnectList.DataOffset, SeekOrigin.Begin);
                obj.SkillConnectList = new SkillConnectListData[subHeader_SkillConnectList.NodeCount];
                for (var i = 0; i < subHeader_SkillConnectList.NodeCount; i++)
                {
                    obj.SkillConnectList[i] = SkillConnectListDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillConnectList = Array.Empty<SkillConnectListData>();
            }
            obj.NodeDatas.Add(subHeader_SkillConnectList, obj.SkillConnectList);
            TBL.SubHeaderMap.Add("SkillConnectListData", obj.SkillConnectList);

            // 处理SubHeader关联数组: SkillTextArray
            SubHeader? subHeader_SkillTextArray = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillTextArrayData");
            if (subHeader_SkillTextArray != null)
            {
                br.BaseStream.Seek(subHeader_SkillTextArray.DataOffset, SeekOrigin.Begin);
                obj.SkillTextArray = new SkillTextArrayData[subHeader_SkillTextArray.NodeCount];
                for (var i = 0; i < subHeader_SkillTextArray.NodeCount; i++)
                {
                    obj.SkillTextArray[i] = SkillTextArrayDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillTextArray = Array.Empty<SkillTextArrayData>();
            }
            obj.NodeDatas.Add(subHeader_SkillTextArray, obj.SkillTextArray);
            TBL.SubHeaderMap.Add("SkillTextArrayData", obj.SkillTextArray);

            // 处理SubHeader关联数组: SkillRangeData
            SubHeader? subHeader_SkillRangeData = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillRangeData");
            if (subHeader_SkillRangeData != null)
            {
                br.BaseStream.Seek(subHeader_SkillRangeData.DataOffset, SeekOrigin.Begin);
                obj.SkillRangeData = new SkillRangeData[subHeader_SkillRangeData.NodeCount];
                for (var i = 0; i < subHeader_SkillRangeData.NodeCount; i++)
                {
                    obj.SkillRangeData[i] = SkillRangeDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillRangeData = Array.Empty<SkillRangeData>();
            }
            obj.NodeDatas.Add(subHeader_SkillRangeData, obj.SkillRangeData);
            TBL.SubHeaderMap.Add("SkillRangeData", obj.SkillRangeData);

            // 处理SubHeader关联数组: SkillLevelList
            SubHeader? subHeader_SkillLevelList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelHelpData");
            if (subHeader_SkillLevelList != null)
            {
                br.BaseStream.Seek(subHeader_SkillLevelList.DataOffset, SeekOrigin.Begin);
                obj.SkillLevelList = new SkillLevelHelpData[subHeader_SkillLevelList.NodeCount];
                for (var i = 0; i < subHeader_SkillLevelList.NodeCount; i++)
                {
                    obj.SkillLevelList[i] = SkillLevelHelpDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SkillLevelList = Array.Empty<SkillLevelHelpData>();
            }
            obj.NodeDatas.Add(subHeader_SkillLevelList, obj.SkillLevelList);
            TBL.SubHeaderMap.Add("SkillLevelHelpData", obj.SkillLevelList);

            var add = new SkillEffectHelpDataAdd[]
            {
                new SkillEffectHelpDataAdd(0, Utilities.GetDisplayName("Null")),
                new SkillEffectHelpDataAdd(3, Utilities.GetDisplayName("WildernessPA")),
                new SkillEffectHelpDataAdd(4, Utilities.GetDisplayName("WildernessMA")),
                new SkillEffectHelpDataAdd(5, Utilities.GetDisplayName("Wilderness5")),
                new SkillEffectHelpDataAdd(6, Utilities.GetDisplayName("WildernessPowerA")),
                new SkillEffectHelpDataAdd(9, Utilities.GetDisplayName("Wilderness9")),
                new SkillEffectHelpDataAdd(25, Utilities.GetDisplayName("KnockUp")),
                new SkillEffectHelpDataAdd(12, Utilities.GetDisplayName("CombatPA")),
                new SkillEffectHelpDataAdd(15, Utilities.GetDisplayName("CombatMA")),
                new SkillEffectHelpDataAdd(7, Utilities.GetDisplayName("YoldaShadow")),
                new SkillEffectHelpDataAdd(8, Utilities.GetDisplayName("ScatterChips")),
                new SkillEffectHelpDataAdd(20, Utilities.GetDisplayName("ChipAttack")),
                new SkillEffectHelpDataAdd(54, Utilities.GetDisplayName("SIncrease")),
                new SkillEffectHelpDataAdd(251, Utilities.GetDisplayName("Rebound")),
                new SkillEffectHelpDataAdd(255, Utilities.GetDisplayName("ViciousInfection")),
                new SkillEffectHelpDataAdd(231, Utilities.GetDisplayName("HolyFeather")),
                new SkillEffectHelpDataAdd(23, Utilities.GetDisplayName("HolyFeatherUp")),
                new SkillEffectHelpDataAdd(248, Utilities.GetDisplayName("DevouringNightmares")),
                new SkillEffectHelpDataAdd(249, Utilities.GetDisplayName("PhantomCat")),
                new SkillEffectHelpDataAdd(252, Utilities.GetDisplayName("DROP-00Z")),
                new SkillEffectHelpDataAdd(24, Utilities.GetDisplayName("Unknown"))
            };
            TBL.SubHeaderMap.Add("SkillEffectHelpDataAdd", add);

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

            ItemHelpTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ItemHelpTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ConditionHelpList
            SubHeader? subHeader_ConditionHelpList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConditionHelpData");
            if (subHeader_ConditionHelpList != null)
            {
                bw.BaseStream.Seek(subHeader_ConditionHelpList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConditionHelpList.NodeCount; i++)
                {
                    ConditionHelpDataHelper.Serialize(bw, obj.ConditionHelpList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillEffectTypeList
            SubHeader? subHeader_SkillEffectTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillEffectTypeHelpData");
            if (subHeader_SkillEffectTypeList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillEffectTypeList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillEffectTypeList.NodeCount; i++)
                {
                    SkillEffectTypeHelpDataHelper.Serialize(bw, obj.SkillEffectTypeList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillEffectList
            SubHeader? subHeader_SkillEffectList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillEffectHelpData");
            if (subHeader_SkillEffectList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillEffectList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillEffectList.NodeCount; i++)
                {
                    SkillEffectHelpDataHelper.Serialize(bw, obj.SkillEffectList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: AttrTypeList
            SubHeader? subHeader_AttrTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AttrTypeHelpData");
            if (subHeader_AttrTypeList != null)
            {
                bw.BaseStream.Seek(subHeader_AttrTypeList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_AttrTypeList.NodeCount; i++)
                {
                    AttrTypeHelpDataHelper.Serialize(bw, obj.AttrTypeList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillTypeList
            SubHeader? subHeader_SkillTypeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillTypeHelpData");
            if (subHeader_SkillTypeList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillTypeList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillTypeList.NodeCount; i++)
                {
                    SkillTypeHelpDataHelper.Serialize(bw, obj.SkillTypeList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillRangeList
            SubHeader? subHeader_SkillRangeList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillRangeHelpData");
            if (subHeader_SkillRangeList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillRangeList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillRangeList.NodeCount; i++)
                {
                    SkillRangeHelpDataHelper.Serialize(bw, obj.SkillRangeList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ItemKindList
            SubHeader? subHeader_ItemKindList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemKindHelpData");
            if (subHeader_ItemKindList != null)
            {
                bw.BaseStream.Seek(subHeader_ItemKindList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ItemKindList.NodeCount; i++)
                {
                    ItemKindHelpDataHelper.Serialize(bw, obj.ItemKindList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillItemStatusList
            SubHeader? subHeader_SkillItemStatusList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillItemStatusData");
            if (subHeader_SkillItemStatusList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillItemStatusList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillItemStatusList.NodeCount; i++)
                {
                    SkillItemStatusDataHelper.Serialize(bw, obj.SkillItemStatusList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: EquipLineList
            SubHeader? subHeader_EquipLineList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EquipLineHelpData");
            if (subHeader_EquipLineList != null)
            {
                bw.BaseStream.Seek(subHeader_EquipLineList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EquipLineList.NodeCount; i++)
                {
                    EquipLineHelpDataHelper.Serialize(bw, obj.EquipLineList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillConnectList
            SubHeader? subHeader_SkillConnectList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillConnectListData");
            if (subHeader_SkillConnectList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillConnectList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillConnectList.NodeCount; i++)
                {
                    SkillConnectListDataHelper.Serialize(bw, obj.SkillConnectList[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillTextArray
            SubHeader? subHeader_SkillTextArray = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillTextArrayData");
            if (subHeader_SkillTextArray != null)
            {
                bw.BaseStream.Seek(subHeader_SkillTextArray.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillTextArray.NodeCount; i++)
                {
                    SkillTextArrayDataHelper.Serialize(bw, obj.SkillTextArray[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillRangeData
            SubHeader? subHeader_SkillRangeData = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillRangeData");
            if (subHeader_SkillRangeData != null)
            {
                bw.BaseStream.Seek(subHeader_SkillRangeData.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillRangeData.NodeCount; i++)
                {
                    SkillRangeDataHelper.Serialize(bw, obj.SkillRangeData[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SkillLevelList
            SubHeader? subHeader_SkillLevelList = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SkillLevelHelpData");
            if (subHeader_SkillLevelList != null)
            {
                bw.BaseStream.Seek(subHeader_SkillLevelList.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SkillLevelList.NodeCount; i++)
                {
                    SkillLevelHelpDataHelper.Serialize(bw, obj.SkillLevelList[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ConditionHelpDataHelper
    {
        public static ConditionHelpData DeSerialize(BinaryReader br)
        {
            var obj = new ConditionHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset1 = br.ReadInt64(),
                TextOffset2 = br.ReadInt64(),
                TextOffset3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConditionHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset1);
            bw.Write(obj.TextOffset2);
            bw.Write(obj.TextOffset3);
        }
    }

    public static class SkillEffectTypeHelpDataHelper
    {
        public static SkillEffectTypeHelpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillEffectTypeHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillEffectTypeHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset);
        }
    }

    public static class SkillEffectHelpDataHelper
    {
        public static SkillEffectHelpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillEffectHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                UnknownOffset = br.ReadInt64(),
                Count = (int)br.ReadInt64(),
                TextOffset1 = br.ReadInt64(),
                TextOffset2 = br.ReadInt64(),
                ColorOffset = br.ReadInt64(),
                TextOffset3 = br.ReadInt64(),
                Unknown2 = br.ReadUInt32(),
                Unknown3 = br.ReadUInt32(),
                TextOffset4 = br.ReadInt64(),
                TextOffset5 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillEffectHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.UnknownOffset);
            if (obj.Count > 1)
            {
                bw.Write(obj.Count + 0x100000000);
            }
            else
            {
                bw.Write(obj.Count);
            }
            bw.Write(obj.TextOffset1);
            bw.Write(obj.TextOffset2);
            bw.Write(obj.ColorOffset);
            bw.Write(obj.TextOffset3);
            bw.Write(obj.Unknown2);
            bw.Write(obj.Unknown3);
            bw.Write(obj.TextOffset4);
            bw.Write(obj.TextOffset5);
        }
    }

    public static class AttrTypeHelpDataHelper
    {
        public static AttrTypeHelpData DeSerialize(BinaryReader br)
        {
            var obj = new AttrTypeHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                IconId = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, AttrTypeHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.IconId);
        }
    }

    public static class SkillTypeHelpDataHelper
    {
        public static SkillTypeHelpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillTypeHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillTypeHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset);
        }
    }

    public static class SkillRangeHelpDataHelper
    {
        public static SkillRangeHelpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillRangeHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                RangeType = br.ReadInt64(),
                TextOffset = br.ReadInt64(),
                ColorOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillRangeHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.RangeType);
            bw.Write(obj.TextOffset);
            bw.Write(obj.ColorOffset);
        }
    }

    public static class ItemKindHelpDataHelper
    {
        public static ItemKindHelpData DeSerialize(BinaryReader br)
        {
            var obj = new ItemKindHelpData
            {
                ID = br.ReadUInt16(),
                SubId1 = br.ReadUInt16(),
                SubId2 = br.ReadUInt32(),
                Description = br.ReadInt64(),
                TextShowType = br.ReadInt64(),
                Name = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ItemKindHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.SubId1);
            bw.Write(obj.SubId2);
            bw.Write(obj.Description);
            bw.Write(obj.TextShowType);
            bw.Write(obj.Name);
        }
    }

    public static class SkillItemStatusDataHelper
    {
        public static SkillItemStatusData DeSerialize(BinaryReader br)
        {
            var obj = new SkillItemStatusData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset1 = br.ReadInt64(),
                TextOffset2 = br.ReadInt64(),
                Unknown = br.ReadInt64(),
                ColorOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillItemStatusData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset1);
            bw.Write(obj.TextOffset2);
            bw.Write(obj.Unknown);
            bw.Write(obj.ColorOffset);
        }
    }

    public static class EquipLineHelpDataHelper
    {
        public static EquipLineHelpData DeSerialize(BinaryReader br)
        {
            var obj = new EquipLineHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset1 = br.ReadInt64(),
                TextOffset2 = br.ReadInt64(),
                ColorOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EquipLineHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset1);
            bw.Write(obj.TextOffset2);
            bw.Write(obj.ColorOffset);
        }
    }

    public static class SkillConnectListDataHelper
    {
        public static SkillConnectListData DeSerialize(BinaryReader br)
        {
            var obj = new SkillConnectListData
            {
                ID = br.ReadInt64(),
                ItemOffset = br.ReadInt64(),
                Count = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillConnectListData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.ItemOffset);
            bw.Write(obj.Count);
        }
    }

    public static class SkillTextArrayDataHelper
    {
        public static SkillTextArrayData DeSerialize(BinaryReader br)
        {
            var obj = new SkillTextArrayData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                ColorOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillTextArrayData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.ColorOffset);
        }
    }

    public static class SkillRangeDataHelper
    {
        public static SkillRangeData DeSerialize(BinaryReader br)
        {
            var obj = new SkillRangeData
            {
                RangeId = br.ReadUInt32(),
                Unkmown1 = br.ReadSingle(),
                Unkmown2 = br.ReadSingle(),
                Unkmown3 = br.ReadSingle(),
                Unkmown4 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillRangeData obj)
        {
            bw.Write(obj.RangeId);
            bw.Write(obj.Unkmown1);
            bw.Write(obj.Unkmown2);
            bw.Write(obj.Unkmown3);
            bw.Write(obj.Unkmown4);
        }
    }

    public static class SkillLevelHelpDataHelper
    {
        public static SkillLevelHelpData DeSerialize(BinaryReader br)
        {
            var obj = new SkillLevelHelpData
            {
                ID = br.ReadInt64(),
                NameOffset = br.ReadInt64(),
                TextOffset = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SkillLevelHelpData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.NameOffset);
            bw.Write(obj.TextOffset);
        }
    }
}