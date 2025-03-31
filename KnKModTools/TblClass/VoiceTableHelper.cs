using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class VoiceTableHelper
    {
        public static VoiceTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<VoiceTable>(br);
            // 处理SubHeader关联数组: VoiceTableDatas
            var subHeader_VoiceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "VoiceTableData");
            if (subHeader_VoiceTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_VoiceTableDatas.DataOffset, SeekOrigin.Begin);
                obj.VoiceTableDatas = new VoiceTableData[subHeader_VoiceTableDatas.NodeCount];
                for (int i = 0; i < subHeader_VoiceTableDatas.NodeCount; i++)
                {
                    obj.VoiceTableDatas[i] = VoiceTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.VoiceTableDatas = Array.Empty<VoiceTableData>();
            }
            obj.NodeDatas.Add(subHeader_VoiceTableDatas, obj.VoiceTableDatas);

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
            VoiceTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not VoiceTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: VoiceTableDatas
            var subHeader_VoiceTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "VoiceTableData");
            if (subHeader_VoiceTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_VoiceTableDatas.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_VoiceTableDatas.NodeCount; i++)
                {
                    VoiceTableDataHelper.Serialize(bw, obj.VoiceTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class VoiceTableDataHelper
    {
        public static VoiceTableData DeSerialize(BinaryReader br)
        {
            var obj = new VoiceTableData();
            obj.ID = br.ReadInt32();
            obj.Int1 = br.ReadInt32();
            obj.Filename = br.ReadInt64();
            obj.Int2 = br.ReadInt32();
            obj.Float1 = br.ReadSingle();
            obj.Int3 = br.ReadInt32();
            obj.Float2 = br.ReadSingle();
            obj.Text = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, VoiceTableData tbl)
        {
            if (tbl is not VoiceTableData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Filename);
            bw.Write(obj.Int2);
            bw.Write(obj.Float1);
            bw.Write(obj.Int3);
            bw.Write(obj.Float2);
            bw.Write(obj.Text);
        }
    }
}