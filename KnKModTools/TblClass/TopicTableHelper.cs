using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class TopicTableHelper
    {
        public static TopicTable DeSerialize(BinaryReader br)
        {
            TopicTable obj = TBLHelper.DeSerialize<TopicTable>(br);
            // 处理SubHeader关联数组: TopicTableDatas
            SubHeader? subHeader_TopicTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicTableData");
            if (subHeader_TopicTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_TopicTableDatas.DataOffset, SeekOrigin.Begin);
                obj.TopicTableDatas = new TopicTableData[subHeader_TopicTableDatas.NodeCount];
                for (var i = 0; i < subHeader_TopicTableDatas.NodeCount; i++)
                {
                    obj.TopicTableDatas[i] = TopicTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TopicTableDatas = Array.Empty<TopicTableData>();
            }
            obj.NodeDatas.Add(subHeader_TopicTableDatas, obj.TopicTableDatas);
            // 处理SubHeader关联数组: TopicGetConds
            SubHeader? subHeader_TopicGetConds = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicGetCond");
            if (subHeader_TopicGetConds != null)
            {
                br.BaseStream.Seek(subHeader_TopicGetConds.DataOffset, SeekOrigin.Begin);
                obj.TopicGetConds = new TopicGetCond[subHeader_TopicGetConds.NodeCount];
                for (var i = 0; i < subHeader_TopicGetConds.NodeCount; i++)
                {
                    obj.TopicGetConds[i] = TopicGetCondHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TopicGetConds = Array.Empty<TopicGetCond>();
            }
            obj.NodeDatas.Add(subHeader_TopicGetConds, obj.TopicGetConds);
            // 处理SubHeader关联数组: TopicUseConds
            SubHeader? subHeader_TopicUseConds = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicUseCond");
            if (subHeader_TopicUseConds != null)
            {
                br.BaseStream.Seek(subHeader_TopicUseConds.DataOffset, SeekOrigin.Begin);
                obj.TopicUseConds = new TopicUseCond[subHeader_TopicUseConds.NodeCount];
                for (var i = 0; i < subHeader_TopicUseConds.NodeCount; i++)
                {
                    obj.TopicUseConds[i] = TopicUseCondHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TopicUseConds = Array.Empty<TopicUseCond>();
            }
            obj.NodeDatas.Add(subHeader_TopicUseConds, obj.TopicUseConds);

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
            TopicTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not TopicTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: TopicTableDatas
            SubHeader? subHeader_TopicTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicTableData");
            if (subHeader_TopicTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_TopicTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TopicTableDatas.NodeCount; i++)
                {
                    TopicTableDataHelper.Serialize(bw, obj.TopicTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TopicGetConds
            SubHeader? subHeader_TopicGetConds = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicGetCond");
            if (subHeader_TopicGetConds != null)
            {
                bw.BaseStream.Seek(subHeader_TopicGetConds.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TopicGetConds.NodeCount; i++)
                {
                    TopicGetCondHelper.Serialize(bw, obj.TopicGetConds[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TopicUseConds
            SubHeader? subHeader_TopicUseConds = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TopicUseCond");
            if (subHeader_TopicUseConds != null)
            {
                bw.BaseStream.Seek(subHeader_TopicUseConds.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TopicUseConds.NodeCount; i++)
                {
                    TopicUseCondHelper.Serialize(bw, obj.TopicUseConds[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class TopicTableDataHelper
    {
        public static TopicTableData DeSerialize(BinaryReader br)
        {
            var obj = new TopicTableData
            {
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TopicTableData tbl)
        {
            if (tbl is not TopicTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
        }
    }

    public static class TopicGetCondHelper
    {
        public static TopicGetCond DeSerialize(BinaryReader br)
        {
            var obj = new TopicGetCond
            {
                ID = br.ReadUInt16(),
                Short1 = br.ReadUInt16(),
                Short2 = br.ReadUInt16(),
                Short3 = br.ReadUInt16(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TopicGetCond tbl)
        {
            if (tbl is not TopicGetCond obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Short3);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
        }
    }

    public static class TopicUseCondHelper
    {
        public static TopicUseCond DeSerialize(BinaryReader br)
        {
            var obj = new TopicUseCond
            {
                ID = br.ReadUInt16(),
                Short1 = br.ReadUInt16(),
                Empty = br.ReadInt32(),
                Text1 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TopicUseCond tbl)
        {
            if (tbl is not TopicUseCond obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Empty);
            bw.Write(obj.Text1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
        }
    }
}