using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class DLCTableHelper
    {
        public static DLCTable DeSerialize(BinaryReader br)
        {
            DLCTable obj = TBLHelper.DeSerialize<DLCTable>(br);
            // 处理SubHeader关联数组: DLCTableDatas
            SubHeader? subHeader_DLCTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DLCTableData");
            if (subHeader_DLCTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_DLCTableDatas.DataOffset, SeekOrigin.Begin);
                obj.DLCTableDatas = new DLCTableData[subHeader_DLCTableDatas.NodeCount];
                for (var i = 0; i < subHeader_DLCTableDatas.NodeCount; i++)
                {
                    obj.DLCTableDatas[i] = DLCTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.DLCTableDatas = Array.Empty<DLCTableData>();
            }
            obj.NodeDatas.Add(subHeader_DLCTableDatas, obj.DLCTableDatas);

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
            DLCTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not DLCTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: DLCTableDatas
            SubHeader? subHeader_DLCTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "DLCTableData");
            if (subHeader_DLCTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_DLCTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_DLCTableDatas.NodeCount; i++)
                {
                    DLCTableDataHelper.Serialize(bw, obj.DLCTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class DLCTableDataHelper
    {
        public static DLCTableData DeSerialize(BinaryReader br)
        {
            var obj = new DLCTableData
            {
                Int1 = br.ReadUInt32(),
                Int2 = br.ReadUInt32(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Name = br.ReadInt64(),
                Description = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, DLCTableData tbl)
        {
            if (tbl is not DLCTableData obj) return;
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Name);
            bw.Write(obj.Description);
            bw.Write(obj.Text2);
        }
    }
}