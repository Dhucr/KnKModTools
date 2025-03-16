using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class MarkerTableHelper
    {
        public static MarkerTable DeSerialize(BinaryReader br)
        {
            MarkerTable obj = TBLHelper.DeSerialize<MarkerTable>(br);
            // 处理SubHeader关联数组: MarkerTableDatas
            SubHeader? subHeader_MarkerTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MarkerTableData");
            if (subHeader_MarkerTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_MarkerTableDatas.DataOffset, SeekOrigin.Begin);
                obj.MarkerTableDatas = new MarkerTableData[subHeader_MarkerTableDatas.NodeCount];
                for (var i = 0; i < subHeader_MarkerTableDatas.NodeCount; i++)
                {
                    obj.MarkerTableDatas[i] = MarkerTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MarkerTableDatas = Array.Empty<MarkerTableData>();
            }
            obj.NodeDatas.Add(subHeader_MarkerTableDatas, obj.MarkerTableDatas);

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
            MarkerTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not MarkerTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: MarkerTableDatas
            SubHeader? subHeader_MarkerTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MarkerTableData");
            if (subHeader_MarkerTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_MarkerTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MarkerTableDatas.NodeCount; i++)
                {
                    MarkerTableDataHelper.Serialize(bw, obj.MarkerTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class MarkerTableDataHelper
    {
        public static MarkerTableData DeSerialize(BinaryReader br)
        {
            var obj = new MarkerTableData
            {
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt32(),
                Short1 = br.ReadInt16(),
                Short2 = br.ReadInt16(),
                Text2 = br.ReadInt64(),
                Int1 = br.ReadInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Float5 = br.ReadSingle(),
                Float6 = br.ReadSingle(),
                Float7 = br.ReadSingle(),
                Text3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MarkerTableData tbl)
        {
            if (tbl is not MarkerTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Text2);
            bw.Write(obj.Int1);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Float4);
            bw.Write(obj.Float5);
            bw.Write(obj.Float6);
            bw.Write(obj.Float7);
            bw.Write(obj.Text3);
        }
    }
}