using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ArtsdriverTableHelper
    {
        public static ArtsdriverTable DeSerialize(BinaryReader br)
        {
            ArtsdriverTable obj = TBLHelper.DeSerialize<ArtsdriverTable>(br);
            // 处理SubHeader关联数组: SlotOpenRates
            SubHeader? subHeader_SlotOpenRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SlotOpenRate");
            if (subHeader_SlotOpenRates != null)
            {
                br.BaseStream.Seek(subHeader_SlotOpenRates.DataOffset, SeekOrigin.Begin);
                obj.SlotOpenRates = new SlotOpenRate[subHeader_SlotOpenRates.NodeCount];
                for (var i = 0; i < subHeader_SlotOpenRates.NodeCount; i++)
                {
                    obj.SlotOpenRates[i] = SlotOpenRateHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SlotOpenRates = Array.Empty<SlotOpenRate>();
            }
            obj.NodeDatas.Add(subHeader_SlotOpenRates, obj.SlotOpenRates);
            // 处理SubHeader关联数组: DriverTableDatas
            SubHeader? subHeader_DriverTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverBaseTableData");
            if (subHeader_DriverTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DriverTableDatas.DataOffset, SeekOrigin.Begin);
                var count = (obj.Nodes.Length - 1) / 2;
                obj.DriverTableDatas = new DriverTableData[count];
                for (var i = 0; i < count; i++)
                {
                    obj.DriverTableDatas[i] = DriverTableDataHelper.DeSerialize(obj, i, br);
                }
            }
            else
            {
                obj.DriverTableDatas = Array.Empty<DriverTableData>();
            }

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
            ArtsdriverTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ArtsdriverTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: SlotOpenRates
            SubHeader? subHeader_SlotOpenRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SlotOpenRate");
            if (subHeader_SlotOpenRates != null)
            {
                bw.BaseStream.Seek(subHeader_SlotOpenRates.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SlotOpenRates.NodeCount; i++)
                {
                    SlotOpenRateHelper.Serialize(bw, obj.SlotOpenRates[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: DriverTableDatas
            SubHeader? subHeader_DriverTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverBaseTableData");
            if (subHeader_DriverTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DriverTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < obj.DriverTableDatas.Length; i++)
                {
                    DriverTableDataHelper.Serialize(obj, i, bw, obj.DriverTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class SlotOpenRateHelper
    {
        public static SlotOpenRate DeSerialize(BinaryReader br)
        {
            var obj = new SlotOpenRate
            {
                Rate1 = br.ReadUInt16(),
                Rate2 = br.ReadUInt16(),
                Rate3 = br.ReadUInt16(),
                Rate4 = br.ReadUInt16(),
                Rate5 = br.ReadUInt16(),
                Rate6 = br.ReadUInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SlotOpenRate obj)
        {
            bw.Write(obj.Rate1);
            bw.Write(obj.Rate2);
            bw.Write(obj.Rate3);
            bw.Write(obj.Rate4);
            bw.Write(obj.Rate5);
            bw.Write(obj.Rate6);
        }
    }

    public static class DriverTableDataHelper
    {
        public static DriverTableData DeSerialize(ArtsdriverTable owner, int index, BinaryReader br)
        {
            var obj = new DriverTableData();
            // 处理SubHeader关联数组: DriverBaseTableDatas
            SubHeader? subHeader_DriverBaseTableDatas = owner.Nodes[index * 2 + 1];
            if (subHeader_DriverBaseTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DriverBaseTableDatas.DataOffset, SeekOrigin.Begin);
                obj.DriverBaseTableDatas = new DriverBaseTableData[subHeader_DriverBaseTableDatas.NodeCount];
                for (var i = 0; i < subHeader_DriverBaseTableDatas.NodeCount; i++)
                {
                    obj.DriverBaseTableDatas[i] = DriverBaseTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.DriverBaseTableDatas = Array.Empty<DriverBaseTableData>();
            }
            owner.NodeDatas.Add(subHeader_DriverBaseTableDatas, obj.DriverBaseTableDatas);
            // 处理SubHeader关联数组: DriverArtsTableDatas
            SubHeader? subHeader_DriverArtsTableDatas = owner.Nodes[index * 2 + 2];
            if (subHeader_DriverArtsTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DriverArtsTableDatas.DataOffset, SeekOrigin.Begin);
                obj.DriverArtsTableDatas = new DriverArtsTableData[subHeader_DriverArtsTableDatas.NodeCount];
                for (var i = 0; i < subHeader_DriverArtsTableDatas.NodeCount; i++)
                {
                    obj.DriverArtsTableDatas[i] = DriverArtsTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.DriverArtsTableDatas = Array.Empty<DriverArtsTableData>();
            }
            owner.NodeDatas.Add(subHeader_DriverArtsTableDatas, obj.DriverArtsTableDatas);
            return obj;
        }

        public static void Serialize(ArtsdriverTable owner, int index, BinaryWriter bw, DriverTableData obj)
        {
            // 处理SubHeader关联数组的序列化: DriverBaseTableDatas
            SubHeader subHeader_DriverBaseTableDatas = owner.Nodes[index * 2 + 1];
            if (subHeader_DriverBaseTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DriverBaseTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_DriverBaseTableDatas.NodeCount; i++)
                {
                    DriverBaseTableDataHelper.Serialize(bw, obj.DriverBaseTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: DriverArtsTableDatas
            SubHeader subHeader_DriverArtsTableDatas = owner.Nodes[index * 2 + 2];
            if (subHeader_DriverArtsTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DriverArtsTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_DriverArtsTableDatas.NodeCount; i++)
                {
                    DriverArtsTableDataHelper.Serialize(bw, obj.DriverArtsTableDatas[i]);
                }
            }
        }
    }

    public static class DriverBaseTableDataHelper
    {
        public static DriverBaseTableData DeSerialize(BinaryReader br)
        {
            var obj = new DriverBaseTableData
            {
                ItemID = br.ReadUInt32(),
                Attr1 = br.ReadUInt16(),
                Short1 = br.ReadUInt16(),
                Attr2 = br.ReadUInt16(),
                CustomSolt = br.ReadUInt16(),
                FixedSolt = br.ReadUInt16(),
                SumSolt = br.ReadUInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, DriverBaseTableData obj)
        {
            bw.Write(obj.ItemID);
            bw.Write(obj.Attr1);
            bw.Write(obj.Short1);
            bw.Write(obj.Attr2);
            bw.Write(obj.CustomSolt);
            bw.Write(obj.FixedSolt);
            bw.Write(obj.SumSolt);
        }
    }

    public static class DriverArtsTableDataHelper
    {
        public static DriverArtsTableData DeSerialize(BinaryReader br)
        {
            var obj = new DriverArtsTableData
            {
                ItemID = br.ReadUInt32(),
                SoltInx = br.ReadUInt16(),
                UnLockLevel = br.ReadUInt16(),
                SkillId = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, DriverArtsTableData obj)
        {
            bw.Write(obj.ItemID);
            bw.Write(obj.SoltInx);
            bw.Write(obj.UnLockLevel);
            bw.Write(obj.SkillId);
        }
    }
}