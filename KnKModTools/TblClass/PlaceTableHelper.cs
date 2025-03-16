using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class PlaceTableHelper
    {
        public static PlaceTable DeSerialize(BinaryReader br)
        {
            PlaceTable obj = TBLHelper.DeSerialize<PlaceTable>(br);
            // 处理SubHeader关联数组: PlaceTableDatas
            SubHeader? subHeader_PlaceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PlaceTableData");
            if (subHeader_PlaceTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_PlaceTableDatas.DataOffset, SeekOrigin.Begin);
                obj.PlaceTableDatas = new PlaceTableData[subHeader_PlaceTableDatas.NodeCount];
                for (var i = 0; i < subHeader_PlaceTableDatas.NodeCount; i++)
                {
                    obj.PlaceTableDatas[i] = PlaceTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.PlaceTableDatas = Array.Empty<PlaceTableData>();
            }
            obj.NodeDatas.Add(subHeader_PlaceTableDatas, obj.PlaceTableDatas);

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
            PlaceTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not PlaceTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: PlaceTableDatas
            SubHeader? subHeader_PlaceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PlaceTableData");
            if (subHeader_PlaceTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_PlaceTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_PlaceTableDatas.NodeCount; i++)
                {
                    PlaceTableDataHelper.Serialize(bw, obj.PlaceTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class PlaceTableDataHelper
    {
        public static PlaceTableData DeSerialize(BinaryReader br)
        {
            var obj = new PlaceTableData
            {
                ID = br.ReadUInt64(),
                Text = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Empty1 = br.ReadUInt32(),
                ScenaFlagArr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                ScenaFlagArr2 = br.ReadInt64(),
                Count2 = br.ReadInt32(),
                Float6 = br.ReadSingle(),
                Text3 = br.ReadInt64(),
                Name = br.ReadInt64(),
                Text5 = br.ReadInt64(),
                Float7 = br.ReadSingle(),
                Float8 = br.ReadSingle(),
                Float9 = br.ReadSingle(),
                Empty2 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, PlaceTableData tbl)
        {
            if (tbl is not PlaceTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
            bw.Write(obj.Text2);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Empty1);
            bw.Write(obj.ScenaFlagArr1);
            bw.Write(obj.Count1);
            bw.Write(obj.ScenaFlagArr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Float6);
            bw.Write(obj.Text3);
            bw.Write(obj.Name);
            bw.Write(obj.Text5);
            bw.Write(obj.Float7);
            bw.Write(obj.Float8);
            bw.Write(obj.Float9);
            bw.Write(obj.Empty2);
        }
    }
}