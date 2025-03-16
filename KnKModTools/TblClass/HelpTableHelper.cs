using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class HelpTableHelper
    {
        public static HelpTable DeSerialize(BinaryReader br)
        {
            HelpTable obj = TBLHelper.DeSerialize<HelpTable>(br);
            // 处理SubHeader关联数组: HelpTableDatas
            SubHeader? subHeader_HelpTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HelpTableData");
            if (subHeader_HelpTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_HelpTableDatas.DataOffset, SeekOrigin.Begin);
                obj.HelpTableDatas = new HelpTableData[subHeader_HelpTableDatas.NodeCount];
                for (var i = 0; i < subHeader_HelpTableDatas.NodeCount; i++)
                {
                    obj.HelpTableDatas[i] = HelpTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.HelpTableDatas = Array.Empty<HelpTableData>();
            }
            obj.NodeDatas.Add(subHeader_HelpTableDatas, obj.HelpTableDatas);
            // 处理SubHeader关联数组: RealTimeHelpTableDatas
            SubHeader? subHeader_RealTimeHelpTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "RealTimeHelpTableData");
            if (subHeader_RealTimeHelpTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_RealTimeHelpTableDatas.DataOffset, SeekOrigin.Begin);
                obj.RealTimeHelpTableDatas = new RealTimeHelpTableData[subHeader_RealTimeHelpTableDatas.NodeCount];
                for (var i = 0; i < subHeader_RealTimeHelpTableDatas.NodeCount; i++)
                {
                    obj.RealTimeHelpTableDatas[i] = RealTimeHelpTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.RealTimeHelpTableDatas = Array.Empty<RealTimeHelpTableData>();
            }
            obj.NodeDatas.Add(subHeader_RealTimeHelpTableDatas, obj.RealTimeHelpTableDatas);

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
            HelpTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not HelpTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: HelpTableDatas
            SubHeader? subHeader_HelpTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "HelpTableData");
            if (subHeader_HelpTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_HelpTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_HelpTableDatas.NodeCount; i++)
                {
                    HelpTableDataHelper.Serialize(bw, obj.HelpTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: RealTimeHelpTableDatas
            SubHeader? subHeader_RealTimeHelpTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "RealTimeHelpTableData");
            if (subHeader_RealTimeHelpTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_RealTimeHelpTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_RealTimeHelpTableDatas.NodeCount; i++)
                {
                    RealTimeHelpTableDataHelper.Serialize(bw, obj.RealTimeHelpTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class HelpTableDataHelper
    {
        public static HelpTableData DeSerialize(BinaryReader br)
        {
            var obj = new HelpTableData
            {
                ID = br.ReadInt16(),
                Short1 = br.ReadInt16(),
                Int1 = br.ReadInt32(),
                FileName1 = br.ReadInt64(),
                FileName2 = br.ReadInt64(),
                TabName = br.ReadInt64(),
                EmptyTextOffset = br.ReadInt64(),
                Text = br.ReadInt64(),
                Long2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, HelpTableData tbl)
        {
            if (tbl is not HelpTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Int1);
            bw.Write(obj.FileName1);
            bw.Write(obj.FileName2);
            bw.Write(obj.TabName);
            bw.Write(obj.EmptyTextOffset);
            bw.Write(obj.Text);
            bw.Write(obj.Long2);
        }
    }

    public static class RealTimeHelpTableDataHelper
    {
        public static RealTimeHelpTableData DeSerialize(BinaryReader br)
        {
            var obj = new RealTimeHelpTableData
            {
                ID = br.ReadUInt32(),
                Uint1 = br.ReadUInt32(),
                Text = br.ReadInt64(),
                Float = br.ReadSingle(),
                Uint2 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, RealTimeHelpTableData tbl)
        {
            if (tbl is not RealTimeHelpTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Uint1);
            bw.Write(obj.Text);
            bw.Write(obj.Float);
            bw.Write(obj.Uint2);
        }
    }
}