using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class TextTableHelper
    {
        public static TextTable DeSerialize(BinaryReader br)
        {
            TextTable obj = TBLHelper.DeSerialize<TextTable>(br);
            // 处理SubHeader关联数组: TextTableDatas
            SubHeader? subHeader_TextTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TextTableData");
            if (subHeader_TextTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_TextTableDatas.DataOffset, SeekOrigin.Begin);
                obj.TextTableDatas = new TextTableData[subHeader_TextTableDatas.NodeCount];
                for (var i = 0; i < subHeader_TextTableDatas.NodeCount; i++)
                {
                    obj.TextTableDatas[i] = TextTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TextTableDatas = Array.Empty<TextTableData>();
            }
            obj.NodeDatas.Add(subHeader_TextTableDatas, obj.TextTableDatas);

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
            TextTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not TextTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: TextTableDatas
            SubHeader? subHeader_TextTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TextTableData");
            if (subHeader_TextTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_TextTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TextTableDatas.NodeCount; i++)
                {
                    TextTableDataHelper.Serialize(bw, obj.TextTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class TextTableDataHelper
    {
        public static TextTableData DeSerialize(BinaryReader br)
        {
            var obj = new TextTableData
            {
                Key = br.ReadInt64(),
                Value = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TextTableData obj)
        {
            bw.Write(obj.Key);
            bw.Write(obj.Value);
        }
    }
}