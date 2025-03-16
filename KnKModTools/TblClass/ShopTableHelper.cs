using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ShopTableHelper
    {
        public static ShopTable DeSerialize(BinaryReader br)
        {
            ShopTable obj = TBLHelper.DeSerialize<ShopTable>(br);
            // 处理SubHeader关联数组: ShopInfos
            SubHeader? subHeader_ShopInfos = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopInfo");
            if (subHeader_ShopInfos != null)
            {
                br.BaseStream.Seek(subHeader_ShopInfos.DataOffset, SeekOrigin.Begin);
                obj.ShopInfos = new ShopInfo[subHeader_ShopInfos.NodeCount];
                for (var i = 0; i < subHeader_ShopInfos.NodeCount; i++)
                {
                    obj.ShopInfos[i] = ShopInfoHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ShopInfos = Array.Empty<ShopInfo>();
            }
            obj.NodeDatas.Add(subHeader_ShopInfos, obj.ShopInfos);
            TBL.SubHeaderMap.Add("ShopInfo", obj.ShopInfos);
            // 处理SubHeader关联数组: ShopItems
            SubHeader? subHeader_ShopItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopItem");
            if (subHeader_ShopItems != null)
            {
                br.BaseStream.Seek(subHeader_ShopItems.DataOffset, SeekOrigin.Begin);
                obj.ShopItems = new ShopItem[subHeader_ShopItems.NodeCount];
                for (var i = 0; i < subHeader_ShopItems.NodeCount; i++)
                {
                    obj.ShopItems[i] = ShopItemHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ShopItems = Array.Empty<ShopItem>();
            }
            obj.NodeDatas.Add(subHeader_ShopItems, obj.ShopItems);
            // 处理SubHeader关联数组: ShopTypeDescs
            SubHeader? subHeader_ShopTypeDescs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopTypeDesc");
            if (subHeader_ShopTypeDescs != null)
            {
                br.BaseStream.Seek(subHeader_ShopTypeDescs.DataOffset, SeekOrigin.Begin);
                obj.ShopTypeDescs = new ShopTypeDesc[subHeader_ShopTypeDescs.NodeCount];
                for (var i = 0; i < subHeader_ShopTypeDescs.NodeCount; i++)
                {
                    obj.ShopTypeDescs[i] = ShopTypeDescHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ShopTypeDescs = Array.Empty<ShopTypeDesc>();
            }
            obj.NodeDatas.Add(subHeader_ShopTypeDescs, obj.ShopTypeDescs);
            TBL.SubHeaderMap.Add("ShopTypeDesc", obj.ShopTypeDescs);
            // 处理SubHeader关联数组: ShopConvs
            SubHeader? subHeader_ShopConvs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopConv");
            if (subHeader_ShopConvs != null)
            {
                br.BaseStream.Seek(subHeader_ShopConvs.DataOffset, SeekOrigin.Begin);
                obj.ShopConvs = new ShopConv[subHeader_ShopConvs.NodeCount];
                for (var i = 0; i < subHeader_ShopConvs.NodeCount; i++)
                {
                    obj.ShopConvs[i] = ShopConvHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ShopConvs = Array.Empty<ShopConv>();
            }
            obj.NodeDatas.Add(subHeader_ShopConvs, obj.ShopConvs);
            // 处理SubHeader关联数组: TradeItems
            SubHeader? subHeader_TradeItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TradeItem");
            if (subHeader_TradeItems != null)
            {
                br.BaseStream.Seek(subHeader_TradeItems.DataOffset, SeekOrigin.Begin);
                obj.TradeItems = new TradeItem[subHeader_TradeItems.NodeCount];
                for (var i = 0; i < subHeader_TradeItems.NodeCount; i++)
                {
                    obj.TradeItems[i] = TradeItemHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TradeItems = Array.Empty<TradeItem>();
            }
            obj.NodeDatas.Add(subHeader_TradeItems, obj.TradeItems);
            // 处理SubHeader关联数组: BargainItems
            SubHeader? subHeader_BargainItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BargainItem");
            if (subHeader_BargainItems != null)
            {
                br.BaseStream.Seek(subHeader_BargainItems.DataOffset, SeekOrigin.Begin);
                obj.BargainItems = new BargainItem[subHeader_BargainItems.NodeCount];
                for (var i = 0; i < subHeader_BargainItems.NodeCount; i++)
                {
                    obj.BargainItems[i] = BargainItemHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BargainItems = Array.Empty<BargainItem>();
            }
            obj.NodeDatas.Add(subHeader_BargainItems, obj.BargainItems);

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
            ShopTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ShopTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ShopInfos
            SubHeader? subHeader_ShopInfos = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopInfo");
            if (subHeader_ShopInfos != null)
            {
                bw.BaseStream.Seek(subHeader_ShopInfos.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ShopInfos.NodeCount; i++)
                {
                    ShopInfoHelper.Serialize(bw, obj.ShopInfos[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ShopItems
            SubHeader? subHeader_ShopItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopItem");
            if (subHeader_ShopItems != null)
            {
                bw.BaseStream.Seek(subHeader_ShopItems.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ShopItems.NodeCount; i++)
                {
                    ShopItemHelper.Serialize(bw, obj.ShopItems[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ShopTypeDescs
            SubHeader? subHeader_ShopTypeDescs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopTypeDesc");
            if (subHeader_ShopTypeDescs != null)
            {
                bw.BaseStream.Seek(subHeader_ShopTypeDescs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ShopTypeDescs.NodeCount; i++)
                {
                    ShopTypeDescHelper.Serialize(bw, obj.ShopTypeDescs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ShopConvs
            SubHeader? subHeader_ShopConvs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ShopConv");
            if (subHeader_ShopConvs != null)
            {
                bw.BaseStream.Seek(subHeader_ShopConvs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ShopConvs.NodeCount; i++)
                {
                    ShopConvHelper.Serialize(bw, obj.ShopConvs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TradeItems
            SubHeader? subHeader_TradeItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TradeItem");
            if (subHeader_TradeItems != null)
            {
                bw.BaseStream.Seek(subHeader_TradeItems.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TradeItems.NodeCount; i++)
                {
                    TradeItemHelper.Serialize(bw, obj.TradeItems[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BargainItems
            SubHeader? subHeader_BargainItems = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BargainItem");
            if (subHeader_BargainItems != null)
            {
                bw.BaseStream.Seek(subHeader_BargainItems.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BargainItems.NodeCount; i++)
                {
                    BargainItemHelper.Serialize(bw, obj.BargainItems[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ShopInfoHelper
    {
        public static ShopInfo DeSerialize(BinaryReader br)
        {
            var obj = new ShopInfo
            {
                Id = br.ReadUInt64(),
                ShopName = br.ReadInt64(),
                ShopType = br.ReadInt64(),
                Flag = br.ReadInt64(),
                Empty = br.ReadUInt16(),
                ShopPricePercent = br.ReadInt16(),
                ShopCamPosX = br.ReadSingle(),
                ShopCamPosY = br.ReadSingle(),
                ShopCamPosZ = br.ReadSingle(),
                ShopCamRotation1 = br.ReadSingle(),
                ShopCamRotation2 = br.ReadSingle(),
                ShopCamRotation3 = br.ReadSingle(),
                ShopCamRotation4 = br.ReadSingle(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShopInfo obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.ShopName);
            bw.Write(obj.ShopType);
            bw.Write(obj.Flag);
            bw.Write(obj.Empty);
            bw.Write(obj.ShopPricePercent);
            bw.Write(obj.ShopCamPosX);
            bw.Write(obj.ShopCamPosY);
            bw.Write(obj.ShopCamPosZ);
            bw.Write(obj.ShopCamRotation1);
            bw.Write(obj.ShopCamRotation2);
            bw.Write(obj.ShopCamRotation3);
            bw.Write(obj.ShopCamRotation4);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
        }
    }

    public static class ShopItemHelper
    {
        public static ShopItem DeSerialize(BinaryReader br)
        {
            var obj = new ShopItem
            {
                ShopId = br.ReadUInt16(),
                ItemId = br.ReadInt16(),
                Unknown = br.ReadInt32(),
                StartScenaFlags = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                EndScenaFlags = br.ReadInt64(),
                Count2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShopItem obj)
        {
            bw.Write(obj.ShopId);
            bw.Write(obj.ItemId);
            bw.Write(obj.Unknown);
            bw.Write(obj.StartScenaFlags);
            bw.Write(obj.Count1);
            bw.Write(obj.EndScenaFlags);
            bw.Write(obj.Count2);
        }
    }

    public static class ShopTypeDescHelper
    {
        public static ShopTypeDesc DeSerialize(BinaryReader br)
        {
            var obj = new ShopTypeDesc
            {
                Id = br.ReadUInt64(),
                Flag = br.ReadInt64(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Byte3 = br.ReadByte(),
                Byte4 = br.ReadByte(),
                Byte5 = br.ReadByte(),
                Byte6 = br.ReadByte(),
                Byte7 = br.ReadByte(),
                Byte8 = br.ReadByte()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShopTypeDesc obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Flag);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Byte3);
            bw.Write(obj.Byte4);
            bw.Write(obj.Byte5);
            bw.Write(obj.Byte6);
            bw.Write(obj.Byte7);
            bw.Write(obj.Byte8);
        }
    }

    public static class ShopConvHelper
    {
        public static ShopConv DeSerialize(BinaryReader br)
        {
            var obj = new ShopConv
            {
                Id = br.ReadUInt32(),
                EarthSepithExchangeRate = br.ReadSingle(),
                WaterSepithExchangeRate = br.ReadSingle(),
                FireSepithExchangeRate = br.ReadSingle(),
                WindSepithExchangeRate = br.ReadSingle(),
                TimeSepithExchangeRate = br.ReadSingle(),
                SpaceSepithExchangeRate = br.ReadSingle(),
                MirageSepithExchangeRate = br.ReadSingle(),
                SepithMassExchangeRate = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShopConv obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.EarthSepithExchangeRate);
            bw.Write(obj.WaterSepithExchangeRate);
            bw.Write(obj.FireSepithExchangeRate);
            bw.Write(obj.WindSepithExchangeRate);
            bw.Write(obj.TimeSepithExchangeRate);
            bw.Write(obj.SpaceSepithExchangeRate);
            bw.Write(obj.MirageSepithExchangeRate);
            bw.Write(obj.SepithMassExchangeRate);
        }
    }

    public static class TradeItemHelper
    {
        public static TradeItem DeSerialize(BinaryReader br)
        {
            var obj = new TradeItem
            {
                OfferedItemId = br.ReadUInt32(),
                Effects = new ShopEffect[6]
            };
            for (var i = 0; i < 6; i++)
            {
                obj.Effects[i] = ShopEffectHelper.DeSerialize(br);
            }
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TradeItem obj)
        {
            bw.Write(obj.OfferedItemId);
            for (var i = 0; i < 6; i++)
            {
                ShopEffectHelper.Serialize(bw, obj.Effects[i]);
            }
        }
    }

    public static class BargainItemHelper
    {
        public static BargainItem DeSerialize(BinaryReader br)
        {
            var obj = new BargainItem
            {
                Id = br.ReadUInt32(),
                Short0 = br.ReadInt16(),
                Short1 = br.ReadInt16(),
                Long0 = br.ReadInt64(),
                Arr0 = br.ReadInt64(),
                Count0 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BargainItem obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.Short0);
            bw.Write(obj.Short1);
            bw.Write(obj.Long0);
            bw.Write(obj.Arr0);
            bw.Write(obj.Count0);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
        }
    }

    public static class ShopEffectHelper
    {
        public static ShopEffect DeSerialize(BinaryReader br)
        {
            var obj = new ShopEffect
            {
                TradeMaterialItemID = br.ReadInt32(),
                RequirAmount = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ShopEffect obj)
        {
            bw.Write(obj.TradeMaterialItemID);
            bw.Write(obj.RequirAmount);
        }
    }
}