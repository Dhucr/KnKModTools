using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class EvTableHelper
    {
        public static EvTable DeSerialize(BinaryReader br)
        {
            EvTable obj = TBLHelper.DeSerialize<EvTable>(br);
            // 处理SubHeader关联数组: EventGroupDatas
            SubHeader? subHeader_EventGroupDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventGroupData");
            if (subHeader_EventGroupDatas != null)
            {
                br.BaseStream.Seek(subHeader_EventGroupDatas.DataOffset, SeekOrigin.Begin);
                obj.EventGroupDatas = new EventGroupData[subHeader_EventGroupDatas.NodeCount];
                for (var i = 0; i < subHeader_EventGroupDatas.NodeCount; i++)
                {
                    obj.EventGroupDatas[i] = EventGroupDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EventGroupDatas = Array.Empty<EventGroupData>();
            }
            obj.NodeDatas.Add(subHeader_EventGroupDatas, obj.EventGroupDatas);
            // 处理SubHeader关联数组: EventSubGroupData1s
            SubHeader? subHeader_EventSubGroupData1s = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventSubGroupData");
            if (subHeader_EventSubGroupData1s != null)
            {
                br.BaseStream.Seek(subHeader_EventSubGroupData1s.DataOffset, SeekOrigin.Begin);
                obj.EventSubGroupData1s = new EventSubGroupData[subHeader_EventSubGroupData1s.NodeCount];
                for (var i = 0; i < subHeader_EventSubGroupData1s.NodeCount; i++)
                {
                    obj.EventSubGroupData1s[i] = EventSubGroupDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EventSubGroupData1s = Array.Empty<EventSubGroupData>();
            }
            obj.NodeDatas.Add(subHeader_EventSubGroupData1s, obj.EventSubGroupData1s);
            // 处理SubHeader关联数组: EventSubGroupData2s
            SubHeader? subHeader_EventSubGroupData2s = obj.Nodes
                .LastOrDefault(n => n.DisplayName == "EventSubGroupData");
            if (subHeader_EventSubGroupData2s != null)
            {
                br.BaseStream.Seek(subHeader_EventSubGroupData2s.DataOffset, SeekOrigin.Begin);
                obj.EventSubGroupData2s = new EventSubGroupData[subHeader_EventSubGroupData2s.NodeCount];
                for (var i = 0; i < subHeader_EventSubGroupData2s.NodeCount; i++)
                {
                    obj.EventSubGroupData2s[i] = EventSubGroupDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EventSubGroupData2s = Array.Empty<EventSubGroupData>();
            }
            obj.NodeDatas.Add(subHeader_EventSubGroupData2s, obj.EventSubGroupData2s);
            // 处理SubHeader关联数组: EventTableDatas
            SubHeader? subHeader_EventTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventTableData");
            if (subHeader_EventTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_EventTableDatas.DataOffset, SeekOrigin.Begin);
                obj.EventTableDatas = new EventTableData[subHeader_EventTableDatas.NodeCount];
                for (var i = 0; i < subHeader_EventTableDatas.NodeCount; i++)
                {
                    obj.EventTableDatas[i] = EventTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EventTableDatas = Array.Empty<EventTableData>();
            }
            obj.NodeDatas.Add(subHeader_EventTableDatas, obj.EventTableDatas);

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
            EvTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not EvTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: EventGroupDatas
            SubHeader? subHeader_EventGroupDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventGroupData");
            if (subHeader_EventGroupDatas != null)
            {
                bw.BaseStream.Seek(subHeader_EventGroupDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EventGroupDatas.NodeCount; i++)
                {
                    EventGroupDataHelper.Serialize(bw, obj.EventGroupDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: EventSubGroupData1s
            SubHeader? subHeader_EventSubGroupData1s = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventSubGroupData");
            if (subHeader_EventSubGroupData1s != null)
            {
                bw.BaseStream.Seek(subHeader_EventSubGroupData1s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EventSubGroupData1s.NodeCount; i++)
                {
                    EventSubGroupDataHelper.Serialize(bw, obj.EventSubGroupData1s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: EventSubGroupData2s
            SubHeader? subHeader_EventSubGroupData2s = obj.Nodes
                .LastOrDefault(n => n.DisplayName == "EventSubGroupData");
            if (subHeader_EventSubGroupData2s != null)
            {
                bw.BaseStream.Seek(subHeader_EventSubGroupData2s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EventSubGroupData2s.NodeCount; i++)
                {
                    EventSubGroupDataHelper.Serialize(bw, obj.EventSubGroupData2s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: EventTableDatas
            SubHeader? subHeader_EventTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EventTableData");
            if (subHeader_EventTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_EventTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EventTableDatas.NodeCount; i++)
                {
                    EventTableDataHelper.Serialize(bw, obj.EventTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class EventGroupDataHelper
    {
        public static EventGroupData DeSerialize(BinaryReader br)
        {
            var obj = new EventGroupData
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EventGroupData tbl)
        {
            if (tbl is not EventGroupData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
        }
    }

    public static class EventSubGroupDataHelper
    {
        public static EventSubGroupData DeSerialize(BinaryReader br)
        {
            var obj = new EventSubGroupData
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EventSubGroupData tbl)
        {
            if (tbl is not EventSubGroupData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
        }
    }

    public static class EventTableDataHelper
    {
        public static EventTableData DeSerialize(BinaryReader br)
        {
            var obj = new EventTableData
            {
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Text4 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Text5 = br.ReadInt64(),
                Uint1 = br.ReadUInt32(),
                Uint2 = br.ReadUInt32(),
                Text6 = br.ReadInt64(),
                Array = br.ReadInt64(),
                Count = br.ReadInt64(),
                Long3 = br.ReadInt64(),
                Long4 = br.ReadInt64(),
                Text = br.ReadInt64(),
                Long5 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EventTableData tbl)
        {
            if (tbl is not EventTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Long1);
            bw.Write(obj.Text3);
            bw.Write(obj.Text4);
            bw.Write(obj.Long2);
            bw.Write(obj.Text5);
            bw.Write(obj.Uint1);
            bw.Write(obj.Uint2);
            bw.Write(obj.Text6);
            bw.Write(obj.Array);
            bw.Write(obj.Count);
            bw.Write(obj.Long3);
            bw.Write(obj.Long4);
            bw.Write(obj.Text);
            bw.Write(obj.Long5);
        }
    }
}