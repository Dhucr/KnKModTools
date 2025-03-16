using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ChapterTableHelper
    {
        public static ChapterTable DeSerialize(BinaryReader br)
        {
            ChapterTable obj = TBLHelper.DeSerialize<ChapterTable>(br);
            // 处理SubHeader关联数组: ChapterParams
            SubHeader? subHeader_ChapterParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ChapterParam");
            if (subHeader_ChapterParams != null)
            {
                br.BaseStream.Seek(subHeader_ChapterParams.DataOffset, SeekOrigin.Begin);
                obj.ChapterParams = new ChapterParam[subHeader_ChapterParams.NodeCount];
                for (var i = 0; i < subHeader_ChapterParams.NodeCount; i++)
                {
                    obj.ChapterParams[i] = ChapterParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ChapterParams = Array.Empty<ChapterParam>();
            }
            obj.NodeDatas.Add(subHeader_ChapterParams, obj.ChapterParams);

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
            ChapterTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ChapterTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ChapterParams
            SubHeader? subHeader_ChapterParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ChapterParam");
            if (subHeader_ChapterParams != null)
            {
                bw.BaseStream.Seek(subHeader_ChapterParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ChapterParams.NodeCount; i++)
                {
                    ChapterParamHelper.Serialize(bw, obj.ChapterParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ChapterParamHelper
    {
        public static ChapterParam DeSerialize(BinaryReader br)
        {
            var obj = new ChapterParam
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Long1 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Text4 = br.ReadInt64(),
                Text5 = br.ReadInt64(),
                Text6 = br.ReadInt64(),
                Text7 = br.ReadInt64(),
                Text8 = br.ReadInt64(),
                Long3 = br.ReadInt64(),
                Arr = br.ReadInt64(),
                Empty1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Empty2 = br.ReadInt64(),
                Text9 = br.ReadInt64(),
                Text10 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ChapterParam tbl)
        {
            if (tbl is not ChapterParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Long1);
            bw.Write(obj.Long2);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
            bw.Write(obj.Text4);
            bw.Write(obj.Text5);
            bw.Write(obj.Text6);
            bw.Write(obj.Text7);
            bw.Write(obj.Text8);
            bw.Write(obj.Long3);
            bw.Write(obj.Arr);
            bw.Write(obj.Empty1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Empty2);
            bw.Write(obj.Text9);
            bw.Write(obj.Text10);
        }
    }
}