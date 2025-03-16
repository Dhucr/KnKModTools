using System.Linq;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ActiveVoiceTableHelper
    {
        public static ActiveVoiceTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<ActiveVoiceTable>(br);
            // 处理SubHeader关联数组: ActiveVoiceTableDatas
            var subHeader_ActiveVoiceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ActiveVoiceTableData");
            if (subHeader_ActiveVoiceTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_ActiveVoiceTableDatas.DataOffset, SeekOrigin.Begin);
                obj.ActiveVoiceTableDatas = new ActiveVoiceTableData[subHeader_ActiveVoiceTableDatas.NodeCount];
                for (int i = 0; i < subHeader_ActiveVoiceTableDatas.NodeCount; i++)
                {
                    obj.ActiveVoiceTableDatas[i] = ActiveVoiceTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ActiveVoiceTableDatas = Array.Empty<ActiveVoiceTableData>();
            }
            obj.NodeDatas.Add(subHeader_ActiveVoiceTableDatas, obj.ActiveVoiceTableDatas);

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
            ActiveVoiceTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ActiveVoiceTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ActiveVoiceTableDatas
            var subHeader_ActiveVoiceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ActiveVoiceTableData");
            if (subHeader_ActiveVoiceTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_ActiveVoiceTableDatas.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_ActiveVoiceTableDatas.NodeCount; i++)
                {
                    ActiveVoiceTableDataHelper.Serialize(bw, obj.ActiveVoiceTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ActiveVoiceTableDataHelper
    {
        public static ActiveVoiceTableData DeSerialize(BinaryReader br)
        {
            var obj = new ActiveVoiceTableData();
            obj.ID = br.ReadUInt32();
            obj.Short1 = br.ReadInt16();
            obj.Short2 = br.ReadInt16();
            obj.Toffset1 = br.ReadInt64();
            obj.Empty1 = br.ReadInt64();
            obj.Toffset2 = br.ReadInt64();
            obj.Long1 = br.ReadInt64();
            obj.ScenaFlagArray = br.ReadInt64();
            obj.FlagCount = br.ReadInt64();
            obj.Arr = br.ReadInt64();
            obj.Count = br.ReadInt32();
            obj.Int1 = br.ReadInt32();
            obj.VoiceSubtitle = br.ReadInt64();
            obj.Empty2 = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ActiveVoiceTableData tbl)
        {
            if (tbl is not ActiveVoiceTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Toffset1);
            bw.Write(obj.Empty1);
            bw.Write(obj.Toffset2);
            bw.Write(obj.Long1);
            bw.Write(obj.ScenaFlagArray);
            bw.Write(obj.FlagCount);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.Int1);
            bw.Write(obj.VoiceSubtitle);
            bw.Write(obj.Empty2);
        }
    }

}

