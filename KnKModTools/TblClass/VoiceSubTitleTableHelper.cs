using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class VoiceSubTitleTableHelper
    {
        public static VoiceSubTitleTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<VoiceSubTitleTable>(br);
            // 处理SubHeader关联数组: VoiceSubtitleTables
            var subHeader_VoiceSubtitleTables = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "VoiceSubtitleTable");
            if (subHeader_VoiceSubtitleTables != null)
            {
                br.BaseStream.Seek(subHeader_VoiceSubtitleTables.DataOffset, SeekOrigin.Begin);
                obj.VoiceSubtitleTables = new VoiceSubtitleTable[subHeader_VoiceSubtitleTables.NodeCount];
                for (int i = 0; i < subHeader_VoiceSubtitleTables.NodeCount; i++)
                {
                    obj.VoiceSubtitleTables[i] = VoiceSubtitleTableHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.VoiceSubtitleTables = Array.Empty<VoiceSubtitleTable>();
            }
            obj.NodeDatas.Add(subHeader_VoiceSubtitleTables, obj.VoiceSubtitleTables);

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
            VoiceSubTitleTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not VoiceSubTitleTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: VoiceSubtitleTables
            var subHeader_VoiceSubtitleTables = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "VoiceSubtitleTable");
            if (subHeader_VoiceSubtitleTables != null)
            {
                bw.BaseStream.Seek(subHeader_VoiceSubtitleTables.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_VoiceSubtitleTables.NodeCount; i++)
                {
                    VoiceSubtitleTableHelper.Serialize(bw, obj.VoiceSubtitleTables[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class VoiceSubtitleTableHelper
    {
        public static VoiceSubtitleTable DeSerialize(BinaryReader br)
        {
            var obj = new VoiceSubtitleTable();
            obj.ID = br.ReadInt32();
            obj.Int1 = br.ReadInt32();
            obj.Text = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, VoiceSubtitleTable tbl)
        {
            if (tbl is not VoiceSubtitleTable obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Text);
        }
    }
}