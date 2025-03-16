using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class LookPointTableHelper
    {
        public static LookPointTable DeSerialize(BinaryReader br)
        {
            LookPointTable obj = TBLHelper.DeSerialize<LookPointTable>(br);
            // 处理SubHeader关联数组: LookPointTableDatas
            SubHeader? subHeader_LookPointTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LookPointTableData");
            if (subHeader_LookPointTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_LookPointTableDatas.DataOffset, SeekOrigin.Begin);
                obj.LookPointTableDatas = new LookPointTableData[subHeader_LookPointTableDatas.NodeCount];
                for (var i = 0; i < subHeader_LookPointTableDatas.NodeCount; i++)
                {
                    obj.LookPointTableDatas[i] = LookPointTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.LookPointTableDatas = Array.Empty<LookPointTableData>();
            }
            obj.NodeDatas.Add(subHeader_LookPointTableDatas, obj.LookPointTableDatas);

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
            LookPointTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not LookPointTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: LookPointTableDatas
            SubHeader? subHeader_LookPointTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LookPointTableData");
            if (subHeader_LookPointTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_LookPointTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_LookPointTableDatas.NodeCount; i++)
                {
                    LookPointTableDataHelper.Serialize(bw, obj.LookPointTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class LookPointTableDataHelper
    {
        public static LookPointTableData DeSerialize(BinaryReader br)
        {
            var obj = new LookPointTableData
            {
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Long3 = br.ReadInt64(),
                Long4 = br.ReadInt64(),
                Long5 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, LookPointTableData tbl)
        {
            if (tbl is not LookPointTableData obj) return;
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
            bw.Write(obj.Long1);
            bw.Write(obj.Long2);
            bw.Write(obj.Long3);
            bw.Write(obj.Long4);
            bw.Write(obj.Long5);
        }
    }
}