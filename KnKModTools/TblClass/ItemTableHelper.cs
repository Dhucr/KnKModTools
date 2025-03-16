using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ItemTableHelper
    {
        public static ItemTable DeSerialize(BinaryReader br)
        {
            ItemTable obj = TBLHelper.DeSerialize<ItemTable>(br);
            // 处理SubHeader关联数组: Items
            SubHeader? subHeader_Items = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemTableData");
            if (subHeader_Items != null)
            {
                br.BaseStream.Seek(subHeader_Items.DataOffset, SeekOrigin.Begin);
                obj.Items = new ItemTableData[subHeader_Items.NodeCount];
                for (var i = 0; i < subHeader_Items.NodeCount; i++)
                {
                    obj.Items[i] = ItemTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.Items = Array.Empty<ItemTableData>();
            }
            obj.NodeDatas.Add(subHeader_Items, obj.Items);
            List<ItemTableData> items = obj.Items.ToList();
            items.Add(new ItemTableEx(0, "无"));
            TBL.SubHeaderMap.Add("ItemTableData", items.ToArray());
            items.Clear();

            // 处理SubHeader关联数组: KindParams
            SubHeader? subHeader_KindParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemKindParam2");
            if (subHeader_KindParams != null)
            {
                br.BaseStream.Seek(subHeader_KindParams.DataOffset, SeekOrigin.Begin);
                obj.KindParams = new ItemKindParam2[subHeader_KindParams.NodeCount];
                for (var i = 0; i < subHeader_KindParams.NodeCount; i++)
                {
                    obj.KindParams[i] = ItemKindParam2Helper.DeSerialize(br);
                }
            }
            else
            {
                obj.KindParams = Array.Empty<ItemKindParam2>();
            }
            obj.NodeDatas.Add(subHeader_KindParams, obj.KindParams);
            TBL.SubHeaderMap.Add("ItemKindParam2", obj.KindParams);

            // 处理SubHeader关联数组: TabTypes
            SubHeader? subHeader_TabTypes = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemTabType");
            if (subHeader_TabTypes != null)
            {
                br.BaseStream.Seek(subHeader_TabTypes.DataOffset, SeekOrigin.Begin);
                obj.TabTypes = new ItemTabType[subHeader_TabTypes.NodeCount];
                for (var i = 0; i < subHeader_TabTypes.NodeCount; i++)
                {
                    obj.TabTypes[i] = ItemTabTypeHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TabTypes = Array.Empty<ItemTabType>();
            }
            obj.NodeDatas.Add(subHeader_TabTypes, obj.TabTypes);
            TBL.SubHeaderMap.Add("ItemTabType", obj.TabTypes);

            // 处理SubHeader关联数组: QuartzParams
            SubHeader? subHeader_QuartzParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuartzParam");
            if (subHeader_QuartzParams != null)
            {
                br.BaseStream.Seek(subHeader_QuartzParams.DataOffset, SeekOrigin.Begin);
                obj.QuartzParams = new QuartzParam[subHeader_QuartzParams.NodeCount];
                for (var i = 0; i < subHeader_QuartzParams.NodeCount; i++)
                {
                    obj.QuartzParams[i] = QuartzParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuartzParams = Array.Empty<QuartzParam>();
            }
            obj.NodeDatas.Add(subHeader_QuartzParams, obj.QuartzParams);
            TBL.SubHeaderMap.Add("QuartzParam", obj.QuartzParams);

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
            ItemTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ItemTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: Items
            SubHeader? subHeader_Items = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemTableData");
            if (subHeader_Items != null)
            {
                bw.BaseStream.Seek(subHeader_Items.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_Items.NodeCount; i++)
                {
                    ItemTableDataHelper.Serialize(bw, obj.Items[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: KindParams
            SubHeader? subHeader_KindParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemKindParam2");
            if (subHeader_KindParams != null)
            {
                bw.BaseStream.Seek(subHeader_KindParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_KindParams.NodeCount; i++)
                {
                    ItemKindParam2Helper.Serialize(bw, obj.KindParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TabTypes
            SubHeader? subHeader_TabTypes = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ItemTabType");
            if (subHeader_TabTypes != null)
            {
                bw.BaseStream.Seek(subHeader_TabTypes.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TabTypes.NodeCount; i++)
                {
                    ItemTabTypeHelper.Serialize(bw, obj.TabTypes[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuartzParams
            SubHeader? subHeader_QuartzParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuartzParam");
            if (subHeader_QuartzParams != null)
            {
                bw.BaseStream.Seek(subHeader_QuartzParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuartzParams.NodeCount; i++)
                {
                    QuartzParamHelper.Serialize(bw, obj.QuartzParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ItemTableDataHelper
    {
        public static ItemTableData DeSerialize(BinaryReader br)
        {
            var obj = new ItemTableData
            {
                ID = br.ReadUInt32(),
                CharLimit = br.ReadUInt32(),
                TextOff1 = br.ReadInt64(),
                TextOff2 = br.ReadInt64(),
                ItemKind = br.ReadByte(),
                SubItemKind = br.ReadByte(),
                ItemIcon = br.ReadUInt16(),
                EffectIcon = br.ReadUInt16(),
                Attr = br.ReadUInt16(),
                Unknown = br.ReadUInt16(),
                Unknown1 = br.ReadUInt16(),
                Unknown2 = br.ReadSingle(),
                Unknown3 = br.ReadSingle(),
                Effects = new Effect[5]
            };
            for (var i = 0; i < 5; i++)
            {
                obj.Effects[i] = EffectHelper.DeSerialize(br);
            }
            obj.Unknown4 = br.ReadSingle();
            obj.HP = br.ReadUInt32();
            obj.EP = br.ReadUInt32();
            obj.PhysicalAttack = br.ReadUInt32();
            obj.PhysicalDefense = br.ReadUInt32();
            obj.MagicAttack = br.ReadUInt32();
            obj.MagicDefense = br.ReadUInt32();
            obj.STR = br.ReadUInt32();
            obj.DEF = br.ReadUInt32();
            obj.AST = br.ReadUInt32();
            obj.ADF = br.ReadUInt32();
            obj.AGL = br.ReadUInt32();
            obj.DEX = br.ReadUInt32();
            obj.Accuracy = br.ReadUInt32();
            obj.Dodge = br.ReadUInt32();
            obj.MagicDodge = br.ReadUInt32();
            obj.Critical = br.ReadUInt32();
            obj.SPD = br.ReadUInt32();
            obj.MOV = br.ReadUInt32();
            obj.UpperLimit = br.ReadUInt32();
            obj.Price = br.ReadUInt32();
            obj.AnimationOff = br.ReadInt64();
            obj.NameOff = br.ReadInt64();
            obj.DescriptionOff = br.ReadInt64();
            obj.Unknown5 = br.ReadUInt32();
            obj.Unknown6 = br.ReadUInt32();
            obj.Unknown7 = br.ReadUInt32();
            obj.Unknown8 = br.ReadUInt32();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ItemTableData obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.CharLimit);
            bw.Write(obj.TextOff1);
            bw.Write(obj.TextOff2);
            bw.Write(obj.ItemKind);
            bw.Write(obj.SubItemKind);
            bw.Write(obj.ItemIcon);
            bw.Write(obj.EffectIcon);
            bw.Write(obj.Attr);
            bw.Write(obj.Unknown);
            bw.Write(obj.Unknown1);
            bw.Write(obj.Unknown2);
            bw.Write(obj.Unknown3);
            for (var i = 0; i < 5; i++)
            {
                EffectHelper.Serialize(bw, obj.Effects[i]);
            }
            bw.Write(obj.Unknown4);
            bw.Write(obj.HP);
            bw.Write(obj.EP);
            bw.Write(obj.PhysicalAttack);
            bw.Write(obj.PhysicalDefense);
            bw.Write(obj.MagicAttack);
            bw.Write(obj.MagicDefense);
            bw.Write(obj.STR);
            bw.Write(obj.DEF);
            bw.Write(obj.AST);
            bw.Write(obj.ADF);
            bw.Write(obj.AGL);
            bw.Write(obj.DEX);
            bw.Write(obj.Accuracy);
            bw.Write(obj.Dodge);
            bw.Write(obj.MagicDodge);
            bw.Write(obj.Critical);
            bw.Write(obj.SPD);
            bw.Write(obj.MOV);
            bw.Write(obj.UpperLimit);
            bw.Write(obj.Price);
            bw.Write(obj.AnimationOff);
            bw.Write(obj.NameOff);
            bw.Write(obj.DescriptionOff);
            bw.Write(obj.Unknown5);
            bw.Write(obj.Unknown6);
            bw.Write(obj.Unknown7);
            bw.Write(obj.Unknown8);
        }
    }

    public static class ItemKindParam2Helper
    {
        public static ItemKindParam2 DeSerialize(BinaryReader br)
        {
            var obj = new ItemKindParam2
            {
                ID = br.ReadUInt64(),
                KindTextOff = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ItemKindParam2 obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.KindTextOff);
        }
    }

    public static class ItemTabTypeHelper
    {
        public static ItemTabType DeSerialize(BinaryReader br)
        {
            var obj = new ItemTabType
            {
                ID = br.ReadInt32(),
                Unknown2 = br.ReadInt32(),
                Unknown3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ItemTabType obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.Unknown2);
            bw.Write(obj.Unknown3);
        }
    }

    public static class QuartzParamHelper
    {
        public static QuartzParam DeSerialize(BinaryReader br)
        {
            var obj = new QuartzParam
            {
                ItemID = br.ReadUInt16(),
                EarthCost = br.ReadUInt16(),
                WaterCost = br.ReadUInt16(),
                FireCost = br.ReadUInt16(),
                WindCost = br.ReadUInt16(),
                TimeCost = br.ReadUInt16(),
                SpaceCost = br.ReadUInt16(),
                MirageCost = br.ReadUInt16(),
                EarthAmount = br.ReadByte(),
                WaterAmount = br.ReadByte(),
                FireAmount = br.ReadByte(),
                WindAmount = br.ReadByte(),
                TimeAmount = br.ReadByte(),
                SpaceAmount = br.ReadByte(),
                MirageAmount = br.ReadByte(),
                Unknown1 = br.ReadByte(),
                Unknown2 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuartzParam obj)
        {
            bw.Write(obj.ItemID);
            bw.Write(obj.EarthCost);
            bw.Write(obj.WaterCost);
            bw.Write(obj.FireCost);
            bw.Write(obj.WindCost);
            bw.Write(obj.TimeCost);
            bw.Write(obj.SpaceCost);
            bw.Write(obj.MirageCost);
            bw.Write(obj.EarthAmount);
            bw.Write(obj.WaterAmount);
            bw.Write(obj.FireAmount);
            bw.Write(obj.WindAmount);
            bw.Write(obj.TimeAmount);
            bw.Write(obj.SpaceAmount);
            bw.Write(obj.MirageAmount);
            bw.Write(obj.Unknown1);
            bw.Write(obj.Unknown2);
        }
    }

    public static class EffectHelper
    {
        public static Effect DeSerialize(BinaryReader br)
        {
            var obj = new Effect
            {
                ID = br.ReadInt32(),
                Param1 = br.ReadInt32(),
                Param2 = br.ReadInt32(),
                Param3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, Effect obj)
        {
            bw.Write(obj.ID);
            bw.Write(obj.Param1);
            bw.Write(obj.Param2);
            bw.Write(obj.Param3);
        }
    }
}