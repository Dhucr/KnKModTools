using System.Linq;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class BGMTableHelper
    {
        public static BGMTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<BGMTable>(br);
            // 处理SubHeader关联数组: BGMTableDatas
            var subHeader_BGMTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BGMTableData");
            if (subHeader_BGMTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_BGMTableDatas.DataOffset, SeekOrigin.Begin);
                obj.BGMTableDatas = new BGMTableData[subHeader_BGMTableDatas.NodeCount];
                for (int i = 0; i < subHeader_BGMTableDatas.NodeCount; i++)
                {
                    obj.BGMTableDatas[i] = BGMTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BGMTableDatas = Array.Empty<BGMTableData>();
            }
            obj.NodeDatas.Add(subHeader_BGMTableDatas, obj.BGMTableDatas);

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
            BGMTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not BGMTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: BGMTableDatas
            var subHeader_BGMTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BGMTableData");
            if (subHeader_BGMTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_BGMTableDatas.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_BGMTableDatas.NodeCount; i++)
                {
                    BGMTableDataHelper.Serialize(bw, obj.BGMTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class BGMTableDataHelper
    {
        public static BGMTableData DeSerialize(BinaryReader br)
        {
            var obj = new BGMTableData();
            obj.ID = br.ReadInt64();
            obj.Text = br.ReadInt64();
            obj.Float1 = br.ReadSingle();
            obj.Int1 = br.ReadInt32();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BGMTableData tbl)
        {
            if (tbl is not BGMTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
            bw.Write(obj.Float1);
            bw.Write(obj.Int1);
        }
    }

}

