using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class AttrTableHelper
    {
        public static AttrTable DeSerialize(BinaryReader br)
        {
            AttrTable obj = TBLHelper.DeSerialize<AttrTable>(br);
            // 处理SubHeader关联数组: AttrDatas
            SubHeader? subHeader_AttrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AttrData");
            if (subHeader_AttrDatas != null)
            {
                br.BaseStream.Seek(subHeader_AttrDatas.DataOffset, SeekOrigin.Begin);
                obj.AttrDatas = new AttrData[subHeader_AttrDatas.NodeCount];
                for (var i = 0; i < subHeader_AttrDatas.NodeCount; i++)
                {
                    obj.AttrDatas[i] = AttrDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.AttrDatas = Array.Empty<AttrData>();
            }
            obj.NodeDatas.Add(subHeader_AttrDatas, obj.AttrDatas);
            TBL.SubHeaderMap.Add("AttrData", obj.AttrDatas);

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
            AttrTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not AttrTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: AttrDatas
            SubHeader? subHeader_AttrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AttrData");
            if (subHeader_AttrDatas != null)
            {
                bw.BaseStream.Seek(subHeader_AttrDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_AttrDatas.NodeCount; i++)
                {
                    AttrDataHelper.Serialize(bw, obj.AttrDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class AttrDataHelper
    {
        public static AttrData DeSerialize(BinaryReader br)
        {
            var obj = new AttrData
            {
                Id = br.ReadByte(),
                ElementId1 = br.ReadByte(),
                ElementId2 = br.ReadByte(),
                Byte1 = br.ReadByte(),
                IconId = br.ReadInt32(),
                ElementName = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, AttrData obj)
        {
            bw.Write(obj.Id);
            bw.Write(obj.ElementId1);
            bw.Write(obj.ElementId2);
            bw.Write(obj.Byte1);
            bw.Write(obj.IconId);
            bw.Write(obj.ElementName);
        }
    }
}