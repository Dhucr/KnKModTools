using System.Linq;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ArtsdriverTableHelper
    {
        public static ArtsdriverTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<ArtsdriverTable>(br);
            // 处理SubHeader关联数组: SlotOpenRates
            var subHeader_SlotOpenRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SlotOpenRate");
            if (subHeader_SlotOpenRates != null)
            {
                br.BaseStream.Seek(subHeader_SlotOpenRates.DataOffset, SeekOrigin.Begin);
                obj.SlotOpenRates = new SlotOpenRate[subHeader_SlotOpenRates.NodeCount];
                for (int i = 0; i < subHeader_SlotOpenRates.NodeCount; i++)
                {
                    obj.SlotOpenRates[i] = SlotOpenRateHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SlotOpenRates = Array.Empty<SlotOpenRate>();
            }
            obj.NodeDatas.Add(subHeader_SlotOpenRates, obj.SlotOpenRates);
            // 处理SubHeader关联数组: DriverBaseTableDatas
            var subHeader_DriverBaseTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverBaseTableData");
            if (subHeader_DriverBaseTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DriverBaseTableDatas.DataOffset, SeekOrigin.Begin);
                obj.DriverBaseTableDatas = new DriverBaseTableData[subHeader_DriverBaseTableDatas.NodeCount];
                for (int i = 0; i < subHeader_DriverBaseTableDatas.NodeCount; i++)
                {
                    obj.DriverBaseTableDatas[i] = DriverBaseTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.DriverBaseTableDatas = Array.Empty<DriverBaseTableData>();
            }
            obj.NodeDatas.Add(subHeader_DriverBaseTableDatas, obj.DriverBaseTableDatas);
            // 处理SubHeader关联数组: DriverArtsTableDatas
            var subHeader_DriverArtsTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverArtsTableData");
            if (subHeader_DriverArtsTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DriverArtsTableDatas.DataOffset, SeekOrigin.Begin);
                obj.DriverArtsTableDatas = new DriverArtsTableData[subHeader_DriverArtsTableDatas.NodeCount];
                for (int i = 0; i < subHeader_DriverArtsTableDatas.NodeCount; i++)
                {
                    obj.DriverArtsTableDatas[i] = DriverArtsTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.DriverArtsTableDatas = Array.Empty<DriverArtsTableData>();
            }
            obj.NodeDatas.Add(subHeader_DriverArtsTableDatas, obj.DriverArtsTableDatas);

            var list = new List<IDataPointer>();
            obj.Pointers = new Dictionary<OffsetKey, IDataPointer>();
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
            var subHeader_SlotOpenRates = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SlotOpenRate");
            if (subHeader_SlotOpenRates != null)
            {
                bw.BaseStream.Seek(subHeader_SlotOpenRates.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_SlotOpenRates.NodeCount; i++)
                {
                    SlotOpenRateHelper.Serialize(bw, obj.SlotOpenRates[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: DriverBaseTableDatas
            var subHeader_DriverBaseTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverBaseTableData");
            if (subHeader_DriverBaseTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DriverBaseTableDatas.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_DriverBaseTableDatas.NodeCount; i++)
                {
                    DriverBaseTableDataHelper.Serialize(bw, obj.DriverBaseTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: DriverArtsTableDatas
            var subHeader_DriverArtsTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DriverArtsTableData");
            if (subHeader_DriverArtsTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DriverArtsTableDatas.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_DriverArtsTableDatas.NodeCount; i++)
                {
                    DriverArtsTableDataHelper.Serialize(bw, obj.DriverArtsTableDatas[i]);
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
            var obj = new SlotOpenRate();
            obj.Rate1 = br.ReadUInt16();
            obj.Rate2 = br.ReadUInt16();
            obj.Rate3 = br.ReadUInt16();
            obj.Rate4 = br.ReadUInt16();
            obj.Rate5 = br.ReadUInt16();
            obj.Rate6 = br.ReadUInt16();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SlotOpenRate tbl)
        {
            if (tbl is not SlotOpenRate obj) return;
            bw.Write(obj.Rate1);
            bw.Write(obj.Rate2);
            bw.Write(obj.Rate3);
            bw.Write(obj.Rate4);
            bw.Write(obj.Rate5);
            bw.Write(obj.Rate6);
        }
    }

    public static class DriverBaseTableDataHelper
    {
        public static DriverBaseTableData DeSerialize(BinaryReader br)
        {
            var obj = new DriverBaseTableData();
            obj.ItemID = br.ReadUInt32();
            obj.Attr1 = br.ReadUInt16();
            obj.Short1 = br.ReadUInt16();
            obj.Attr2 = br.ReadUInt16();
            obj.CustomSolt = br.ReadUInt16();
            obj.FixedSolt = br.ReadUInt16();
            obj.SumSolt = br.ReadUInt16();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, DriverBaseTableData tbl)
        {
            if (tbl is not DriverBaseTableData obj) return;
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
            var obj = new DriverArtsTableData();
            obj.ItemID = br.ReadUInt32();
            obj.SoltInx = br.ReadUInt16();
            obj.UnLockLevel = br.ReadUInt16();
            obj.SkillId = br.ReadUInt32();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, DriverArtsTableData tbl)
        {
            if (tbl is not DriverArtsTableData obj) return;
            bw.Write(obj.ItemID);
            bw.Write(obj.SoltInx);
            bw.Write(obj.UnLockLevel);
            bw.Write(obj.SkillId);
        }
    }

}

