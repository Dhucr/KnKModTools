using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ConstantValueTableHelper
    {
        public static ConstantValueTable DeSerialize(BinaryReader br)
        {
            ConstantValueTable obj = TBLHelper.DeSerialize<ConstantValueTable>(br);
            // 处理SubHeader关联数组: ConstantValues
            SubHeader? subHeader_ConstantValues = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConstantValue");
            if (subHeader_ConstantValues != null)
            {
                br.BaseStream.Seek(subHeader_ConstantValues.DataOffset, SeekOrigin.Begin);
                obj.ConstantValues = new ConstantValue[subHeader_ConstantValues.NodeCount];
                for (var i = 0; i < subHeader_ConstantValues.NodeCount; i++)
                {
                    obj.ConstantValues[i] = ConstantValueHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ConstantValues = Array.Empty<ConstantValue>();
            }
            obj.NodeDatas.Add(subHeader_ConstantValues, obj.ConstantValues);

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
            ConstantValueTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ConstantValueTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ConstantValues
            SubHeader? subHeader_ConstantValues = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ConstantValue");
            if (subHeader_ConstantValues != null)
            {
                bw.BaseStream.Seek(subHeader_ConstantValues.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ConstantValues.NodeCount; i++)
                {
                    ConstantValueHelper.Serialize(bw, obj.ConstantValues[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ConstantValueHelper
    {
        public static ConstantValue DeSerialize(BinaryReader br)
        {
            var obj = new ConstantValue
            {
                ID = br.ReadUInt32(),
                Value1 = br.ReadUInt32(),
                Value2 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ConstantValue tbl)
        {
            if (tbl is not ConstantValue obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Value1);
            bw.Write(obj.Value2);
        }
    }
}