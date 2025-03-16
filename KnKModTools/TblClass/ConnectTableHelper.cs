using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ConnectTableHelper
    {
        public static ConnectTable DeSerialize(BinaryReader br)
        {
            ConnectTable obj = TBLHelper.DeSerialize<ConnectTable>(br);
            // 处理SubHeader关联数组: ConnectChrParams
            SubHeader? subHeader_ConnectChrParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectChrParam");
            if (subHeader_ConnectChrParams != null)
            {
                br.BaseStream.Seek(subHeader_ConnectChrParams.DataOffset, SeekOrigin.Begin);
                obj.ConnectChrParams = new ConnectChrParam[subHeader_ConnectChrParams.NodeCount];
                for (var i = 0; i < subHeader_ConnectChrParams.NodeCount; i++)
                {
                    obj.ConnectChrParams[i] = ConnectChrParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConnectChrParams = Array.Empty<ConnectChrParam>();
            }
            obj.NodeDatas.Add(subHeader_ConnectChrParams, obj.ConnectChrParams);
            // 处理SubHeader关联数组: ConnectBonusParams
            SubHeader? subHeader_ConnectBonusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectBonusParam");
            if (subHeader_ConnectBonusParams != null)
            {
                br.BaseStream.Seek(subHeader_ConnectBonusParams.DataOffset, SeekOrigin.Begin);
                obj.ConnectBonusParams = new ConnectBonusParam[subHeader_ConnectBonusParams.NodeCount];
                for (var i = 0; i < subHeader_ConnectBonusParams.NodeCount; i++)
                {
                    obj.ConnectBonusParams[i] = ConnectBonusParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConnectBonusParams = Array.Empty<ConnectBonusParam>();
            }
            obj.NodeDatas.Add(subHeader_ConnectBonusParams, obj.ConnectBonusParams);
            // 处理SubHeader关联数组: ConnectTopicDatas
            SubHeader? subHeader_ConnectTopicDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectTopicData");
            if (subHeader_ConnectTopicDatas != null)
            {
                br.BaseStream.Seek(subHeader_ConnectTopicDatas.DataOffset, SeekOrigin.Begin);
                obj.ConnectTopicDatas = new ConnectTopicData[subHeader_ConnectTopicDatas.NodeCount];
                for (var i = 0; i < subHeader_ConnectTopicDatas.NodeCount; i++)
                {
                    obj.ConnectTopicDatas[i] = ConnectTopicDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConnectTopicDatas = Array.Empty<ConnectTopicData>();
            }
            obj.NodeDatas.Add(subHeader_ConnectTopicDatas, obj.ConnectTopicDatas);
            // 处理SubHeader关联数组: ConnectEventDatas
            SubHeader? subHeader_ConnectEventDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectEventData");
            if (subHeader_ConnectEventDatas != null)
            {
                br.BaseStream.Seek(subHeader_ConnectEventDatas.DataOffset, SeekOrigin.Begin);
                obj.ConnectEventDatas = new ConnectEventData[subHeader_ConnectEventDatas.NodeCount];
                for (var i = 0; i < subHeader_ConnectEventDatas.NodeCount; i++)
                {
                    obj.ConnectEventDatas[i] = ConnectEventDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConnectEventDatas = Array.Empty<ConnectEventData>();
            }
            obj.NodeDatas.Add(subHeader_ConnectEventDatas, obj.ConnectEventDatas);
            // 处理SubHeader关联数组: ConnectItemDatas
            SubHeader? subHeader_ConnectItemDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectItemData");
            if (subHeader_ConnectItemDatas != null)
            {
                br.BaseStream.Seek(subHeader_ConnectItemDatas.DataOffset, SeekOrigin.Begin);
                obj.ConnectItemDatas = new ConnectItemData[subHeader_ConnectItemDatas.NodeCount];
                for (var i = 0; i < subHeader_ConnectItemDatas.NodeCount; i++)
                {
                    obj.ConnectItemDatas[i] = ConnectItemDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConnectItemDatas = Array.Empty<ConnectItemData>();
            }
            obj.NodeDatas.Add(subHeader_ConnectItemDatas, obj.ConnectItemDatas);
            // 处理SubHeader关联数组: LGCParams
            SubHeader? subHeader_LGCParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LGCParam");
            if (subHeader_LGCParams != null)
            {
                br.BaseStream.Seek(subHeader_LGCParams.DataOffset, SeekOrigin.Begin);
                obj.LGCParams = new LGCParam[subHeader_LGCParams.NodeCount];
                for (var i = 0; i < subHeader_LGCParams.NodeCount; i++)
                {
                    obj.LGCParams[i] = LGCParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.LGCParams = Array.Empty<LGCParam>();
            }
            obj.NodeDatas.Add(subHeader_LGCParams, obj.LGCParams);

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
            ConnectTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ConnectTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ConnectChrParams
            SubHeader? subHeader_ConnectChrParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectChrParam");
            if (subHeader_ConnectChrParams != null)
            {
                bw.BaseStream.Seek(subHeader_ConnectChrParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConnectChrParams.NodeCount; i++)
                {
                    ConnectChrParamHelper.Serialize(bw, obj.ConnectChrParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ConnectBonusParams
            SubHeader? subHeader_ConnectBonusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectBonusParam");
            if (subHeader_ConnectBonusParams != null)
            {
                bw.BaseStream.Seek(subHeader_ConnectBonusParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConnectBonusParams.NodeCount; i++)
                {
                    ConnectBonusParamHelper.Serialize(bw, obj.ConnectBonusParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ConnectTopicDatas
            SubHeader? subHeader_ConnectTopicDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectTopicData");
            if (subHeader_ConnectTopicDatas != null)
            {
                bw.BaseStream.Seek(subHeader_ConnectTopicDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConnectTopicDatas.NodeCount; i++)
                {
                    ConnectTopicDataHelper.Serialize(bw, obj.ConnectTopicDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ConnectEventDatas
            SubHeader? subHeader_ConnectEventDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectEventData");
            if (subHeader_ConnectEventDatas != null)
            {
                bw.BaseStream.Seek(subHeader_ConnectEventDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConnectEventDatas.NodeCount; i++)
                {
                    ConnectEventDataHelper.Serialize(bw, obj.ConnectEventDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ConnectItemDatas
            SubHeader? subHeader_ConnectItemDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConnectItemData");
            if (subHeader_ConnectItemDatas != null)
            {
                bw.BaseStream.Seek(subHeader_ConnectItemDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConnectItemDatas.NodeCount; i++)
                {
                    ConnectItemDataHelper.Serialize(bw, obj.ConnectItemDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: LGCParams
            SubHeader? subHeader_LGCParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LGCParam");
            if (subHeader_LGCParams != null)
            {
                bw.BaseStream.Seek(subHeader_LGCParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_LGCParams.NodeCount; i++)
                {
                    LGCParamHelper.Serialize(bw, obj.LGCParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ConnectChrParamHelper
    {
        public static ConnectChrParam DeSerialize(BinaryReader br)
        {
            var obj = new ConnectChrParam
            {
                ID1 = br.ReadUInt16(),
                ID2 = br.ReadUInt16(),
                Empty = br.ReadInt32(),
                CharFullName = br.ReadInt64(),
                CharFirstName = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                ScenaFile = br.ReadInt64(),
                Affiliation = br.ReadInt64(),
                Occupation = br.ReadInt64(),
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

        public static void Serialize(BinaryWriter bw, ConnectChrParam tbl)
        {
            if (tbl is not ConnectChrParam obj) return;
            bw.Write(obj.ID1);
            bw.Write(obj.ID2);
            bw.Write(obj.Empty);
            bw.Write(obj.CharFullName);
            bw.Write(obj.CharFirstName);
            bw.Write(obj.Long1);
            bw.Write(obj.ScenaFile);
            bw.Write(obj.Affiliation);
            bw.Write(obj.Occupation);
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

    public static class ConnectBonusParamHelper
    {
        public static ConnectBonusParam DeSerialize(BinaryReader br)
        {
            var obj = new ConnectBonusParam
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadUInt32(),
                Long1 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt32(),
                Int2 = br.ReadUInt32(),
                Long2 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt32(),
                Int3 = br.ReadUInt32(),
                Long3 = br.ReadInt64(),
                Empty = br.ReadInt64(),
                Description = br.ReadInt64(),
                Bonus = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConnectBonusParam tbl)
        {
            if (tbl is not ConnectBonusParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Int2);
            bw.Write(obj.Long2);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Int3);
            bw.Write(obj.Long3);
            bw.Write(obj.Empty);
            bw.Write(obj.Description);
            bw.Write(obj.Bonus);
        }
    }

    public static class ConnectTopicDataHelper
    {
        public static ConnectTopicData DeSerialize(BinaryReader br)
        {
            var obj = new ConnectTopicData
            {
                ID = br.ReadInt16(),
                Int1 = br.ReadUInt32(),
                Empty1 = br.ReadInt16(),
                ConnectName = br.ReadInt64(),
                ConnectDescription = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConnectTopicData tbl)
        {
            if (tbl is not ConnectTopicData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.ConnectName);
            bw.Write(obj.ConnectDescription);
        }
    }

    public static class ConnectEventDataHelper
    {
        public static ConnectEventData DeSerialize(BinaryReader br)
        {
            var obj = new ConnectEventData
            {
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt32(),
                Long1 = br.ReadInt64(),
                Empty1 = br.ReadInt32(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConnectEventData tbl)
        {
            if (tbl is not ConnectEventData obj) return;
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Long1);
            bw.Write(obj.Empty1);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Long2);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
        }
    }

    public static class ConnectItemDataHelper
    {
        public static ConnectItemData DeSerialize(BinaryReader br)
        {
            var obj = new ConnectItemData
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadUInt32(),
                Long1 = br.ReadUInt64(),
                ItemName = br.ReadInt64(),
                ItemDescription = br.ReadInt64(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Byte3 = br.ReadByte(),
                Byte4 = br.ReadByte(),
                Empty1 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConnectItemData tbl)
        {
            if (tbl is not ConnectItemData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Long1);
            bw.Write(obj.ItemName);
            bw.Write(obj.ItemDescription);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Byte3);
            bw.Write(obj.Byte4);
            bw.Write(obj.Empty1);
        }
    }

    public static class LGCParamHelper
    {
        public static LGCParam DeSerialize(BinaryReader br)
        {
            var obj = new LGCParam
            {
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Byte3 = br.ReadByte(),
                Byte4 = br.ReadByte()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, LGCParam tbl)
        {
            if (tbl is not LGCParam obj) return;
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Byte3);
            bw.Write(obj.Byte4);
        }
    }
}