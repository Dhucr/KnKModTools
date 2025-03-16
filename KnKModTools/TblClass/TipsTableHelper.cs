using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class TipsTableHelper
    {
        public static TipsTable DeSerialize(BinaryReader br)
        {
            TipsTable obj = TBLHelper.DeSerialize<TipsTable>(br);
            // 处理SubHeader关联数组: TipsTableDatas
            SubHeader? subHeader_TipsTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TipsTableData");
            if (subHeader_TipsTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_TipsTableDatas.DataOffset, SeekOrigin.Begin);
                obj.TipsTableDatas = new TipsTableData[subHeader_TipsTableDatas.NodeCount];
                for (var i = 0; i < subHeader_TipsTableDatas.NodeCount; i++)
                {
                    obj.TipsTableDatas[i] = TipsTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TipsTableDatas = Array.Empty<TipsTableData>();
            }
            obj.NodeDatas.Add(subHeader_TipsTableDatas, obj.TipsTableDatas);

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
            TipsTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not TipsTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: TipsTableDatas
            SubHeader? subHeader_TipsTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TipsTableData");
            if (subHeader_TipsTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_TipsTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TipsTableDatas.NodeCount; i++)
                {
                    TipsTableDataHelper.Serialize(bw, obj.TipsTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class TipsTableDataHelper
    {
        public static TipsTableData DeSerialize(BinaryReader br)
        {
            var obj = new TipsTableData
            {
                Long1 = br.ReadInt64(),
                Arr = br.ReadInt64(),
                Count = br.ReadInt64(),
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TipsTableData tbl)
        {
            if (tbl is not TipsTableData obj) return;
            bw.Write(obj.Long1);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }
}