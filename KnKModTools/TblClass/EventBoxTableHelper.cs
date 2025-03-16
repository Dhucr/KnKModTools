using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class EventBoxTableHelper
    {
        public static EventBoxTable DeSerialize(BinaryReader br)
        {
            EventBoxTable obj = TBLHelper.DeSerialize<EventBoxTable>(br);
            // 处理SubHeader关联数组: EventBoxTableDatas
            SubHeader? subHeader_EventBoxTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventBoxTableData");
            if (subHeader_EventBoxTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_EventBoxTableDatas.DataOffset, SeekOrigin.Begin);
                obj.EventBoxTableDatas = new EventBoxTableData[subHeader_EventBoxTableDatas.NodeCount];
                for (var i = 0; i < subHeader_EventBoxTableDatas.NodeCount; i++)
                {
                    obj.EventBoxTableDatas[i] = EventBoxTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EventBoxTableDatas = Array.Empty<EventBoxTableData>();
            }
            obj.NodeDatas.Add(subHeader_EventBoxTableDatas, obj.EventBoxTableDatas);

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
            EventBoxTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not EventBoxTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: EventBoxTableDatas
            SubHeader? subHeader_EventBoxTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventBoxTableData");
            if (subHeader_EventBoxTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_EventBoxTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EventBoxTableDatas.NodeCount; i++)
                {
                    EventBoxTableDataHelper.Serialize(bw, obj.EventBoxTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class EventBoxTableDataHelper
    {
        public static EventBoxTableData DeSerialize(BinaryReader br)
        {
            var obj = new EventBoxTableData
            {
                FileName = br.ReadInt64(),
                EventName = br.ReadInt64(),
                Text = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EventBoxTableData tbl)
        {
            if (tbl is not EventBoxTableData obj) return;
            bw.Write(obj.FileName);
            bw.Write(obj.EventName);
            bw.Write(obj.Text);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
        }
    }
}