using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class NameTableHelper
    {
        public static NameTable DeSerialize(BinaryReader br)
        {
            NameTable obj = TBLHelper.DeSerialize<NameTable>(br);
            // 处理SubHeader关联数组: NameTableDatas
            SubHeader? subHeader_NameTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NameTableData");
            if (subHeader_NameTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_NameTableDatas.DataOffset, SeekOrigin.Begin);
                obj.NameTableDatas = new NameTableData[subHeader_NameTableDatas.NodeCount];
                for (var i = 0; i < subHeader_NameTableDatas.NodeCount; i++)
                {
                    obj.NameTableDatas[i] = NameTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NameTableDatas = Array.Empty<NameTableData>();
            }
            obj.NodeDatas.Add(subHeader_NameTableDatas, obj.NameTableDatas);

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
            NameTable.SManager = obj.Manager;

            List<NameTableData> nts = obj.NameTableDatas.ToList();
            nts.RemoveAll(nt => nt.CharacterId == 65535);
            nts.Add(new NameTableEx(65535, "通用"));
            TBL.SubHeaderMap.Add("NameTableData", nts.ToArray());
            nts.Clear();

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not NameTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: NameTableDatas
            SubHeader? subHeader_NameTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NameTableData");
            if (subHeader_NameTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_NameTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NameTableDatas.NodeCount; i++)
                {
                    NameTableDataHelper.Serialize(bw, obj.NameTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class NameTableDataHelper
    {
        public static NameTableData DeSerialize(BinaryReader br)
        {
            var obj = new NameTableData
            {
                CharacterId = br.ReadUInt64(),
                Name = br.ReadInt64(),
                Texture = br.ReadInt64(),
                Face = br.ReadInt64(),
                Model = br.ReadInt64(),
                Long1 = br.ReadUInt64(),
                Text1 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Long3 = br.ReadUInt64(),
                Text3 = br.ReadInt64(),
                FullName = br.ReadInt64(),
                EngName = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NameTableData obj)
        {
            bw.Write(obj.CharacterId);
            bw.Write(obj.Name);
            bw.Write(obj.Texture);
            bw.Write(obj.Face);
            bw.Write(obj.Model);
            bw.Write(obj.Long1);
            bw.Write(obj.Text1);
            bw.Write(obj.Long2);
            bw.Write(obj.Text2);
            bw.Write(obj.Long3);
            bw.Write(obj.Text3);
            bw.Write(obj.FullName);
            bw.Write(obj.EngName);
        }
    }
}