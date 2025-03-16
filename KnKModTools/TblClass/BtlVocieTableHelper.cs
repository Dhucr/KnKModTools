using System.Linq;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class BtlVoiceTableHelper
    {
        public static BtlVoiceTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<BtlVoiceTable>(br);
            // 处理SubHeader关联数组: BTLVoiceTable1s
            var subHeader_BTLVoiceTable1s = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BTLVoiceTable");
            if (subHeader_BTLVoiceTable1s != null)
            {
                br.BaseStream.Seek(subHeader_BTLVoiceTable1s.DataOffset, SeekOrigin.Begin);
                obj.BTLVoiceTable1s = new BTLVoiceTable[subHeader_BTLVoiceTable1s.NodeCount];
                for (int i = 0; i < subHeader_BTLVoiceTable1s.NodeCount; i++)
                {
                    obj.BTLVoiceTable1s[i] = BTLVoiceTableHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BTLVoiceTable1s = Array.Empty<BTLVoiceTable>();
            }
            obj.NodeDatas.Add(subHeader_BTLVoiceTable1s, obj.BTLVoiceTable1s);
            // 处理SubHeader关联数组: BTLVoiceTable2s
            var subHeader_BTLVoiceTable2s = obj.Nodes
                .LastOrDefault(n => n.DisplayName == "BTLVoiceTable");
            if (subHeader_BTLVoiceTable2s != null)
            {
                br.BaseStream.Seek(subHeader_BTLVoiceTable2s.DataOffset, SeekOrigin.Begin);
                obj.BTLVoiceTable2s = new BTLVoiceTable[subHeader_BTLVoiceTable2s.NodeCount];
                for (int i = 0; i < subHeader_BTLVoiceTable2s.NodeCount; i++)
                {
                    obj.BTLVoiceTable2s[i] = BTLVoiceTableHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BTLVoiceTable2s = Array.Empty<BTLVoiceTable>();
            }
            obj.NodeDatas.Add(subHeader_BTLVoiceTable2s, obj.BTLVoiceTable2s);

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
            BtlVoiceTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not BtlVoiceTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: BTLVoiceTable1s
            var subHeader_BTLVoiceTable1s = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BTLVoiceTable");
            if (subHeader_BTLVoiceTable1s != null)
            {
                bw.BaseStream.Seek(subHeader_BTLVoiceTable1s.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_BTLVoiceTable1s.NodeCount; i++)
                {
                    BTLVoiceTableHelper.Serialize(bw, obj.BTLVoiceTable1s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BTLVoiceTable2s
            var subHeader_BTLVoiceTable2s = obj.Nodes
                .LastOrDefault(n => n.DisplayName == "BTLVoiceTable");
            if (subHeader_BTLVoiceTable2s != null)
            {
                bw.BaseStream.Seek(subHeader_BTLVoiceTable2s.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_BTLVoiceTable2s.NodeCount; i++)
                {
                    BTLVoiceTableHelper.Serialize(bw, obj.BTLVoiceTable2s[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class BTLVoiceTableHelper
    {
        public static BTLVoiceTable DeSerialize(BinaryReader br)
        {
            var obj = new BTLVoiceTable();
            obj.Int1 = br.ReadInt32();
            obj.Long1 = br.ReadInt64();
            obj.Long2 = br.ReadInt64();
            obj.Empty1 = br.ReadInt32();
            obj.Arr1 = br.ReadInt64();
            obj.Count1 = br.ReadInt64();
            obj.Arr2 = br.ReadInt64();
            obj.Count2 = br.ReadInt64();
            obj.Text1 = br.ReadInt64();
            obj.Arr3 = br.ReadInt64();
            obj.Count3 = br.ReadInt64();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BTLVoiceTable tbl)
        {
            if (tbl is not BTLVoiceTable obj) return;
            bw.Write(obj.Int1);
            bw.Write(obj.Long1);
            bw.Write(obj.Long2);
            bw.Write(obj.Empty1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Text1);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
        }
    }

}

