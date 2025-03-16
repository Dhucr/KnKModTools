using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class TBoxTableHelper
    {
        public static TBoxTable DeSerialize(BinaryReader br)
        {
            TBoxTable obj = TBLHelper.DeSerialize<TBoxTable>(br);
            // 处理SubHeader关联数组: TBoxParams
            SubHeader? subHeader_TBoxParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TBoxParam");
            if (subHeader_TBoxParams != null)
            {
                br.BaseStream.Seek(subHeader_TBoxParams.DataOffset, SeekOrigin.Begin);
                obj.TBoxParams = new TBoxParam[subHeader_TBoxParams.NodeCount];
                for (var i = 0; i < subHeader_TBoxParams.NodeCount; i++)
                {
                    obj.TBoxParams[i] = TBoxParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TBoxParams = Array.Empty<TBoxParam>();
            }
            obj.NodeDatas.Add(subHeader_TBoxParams, obj.TBoxParams);

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
            TBoxTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not TBoxTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: TBoxParams
            SubHeader? subHeader_TBoxParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TBoxParam");
            if (subHeader_TBoxParams != null)
            {
                bw.BaseStream.Seek(subHeader_TBoxParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TBoxParams.NodeCount; i++)
                {
                    TBoxParamHelper.Serialize(bw, obj.TBoxParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class TBoxParamHelper
    {
        public static TBoxParam DeSerialize(BinaryReader br)
        {
            var obj = new TBoxParam
            {
                Scena = br.ReadInt64(),
                Name = br.ReadInt64(),
                Flag = br.ReadInt64(),
                HackBoxID = br.ReadInt64(),
                ItemList = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                HackBoxThing1 = br.ReadInt64(),
                HackBoxThing2 = br.ReadInt64(),
                Count2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBoxParam tbl)
        {
            if (tbl is not TBoxParam obj) return;
            bw.Write(obj.Scena);
            bw.Write(obj.Name);
            bw.Write(obj.Flag);
            bw.Write(obj.HackBoxID);
            bw.Write(obj.ItemList);
            bw.Write(obj.Count1);
            bw.Write(obj.HackBoxThing1);
            bw.Write(obj.HackBoxThing2);
            bw.Write(obj.Count2);
        }
    }
}