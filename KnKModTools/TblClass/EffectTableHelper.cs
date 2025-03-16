using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class EffectTableHelper
    {
        public static EffectTable DeSerialize(BinaryReader br)
        {
            EffectTable obj = TBLHelper.DeSerialize<EffectTable>(br);
            // 处理SubHeader关联数组: EffectTableDatas
            SubHeader? subHeader_EffectTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EffectTableData");
            if (subHeader_EffectTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_EffectTableDatas.DataOffset, SeekOrigin.Begin);
                obj.EffectTableDatas = new EffectTableData[subHeader_EffectTableDatas.NodeCount];
                for (var i = 0; i < subHeader_EffectTableDatas.NodeCount; i++)
                {
                    obj.EffectTableDatas[i] = EffectTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EffectTableDatas = Array.Empty<EffectTableData>();
            }
            obj.NodeDatas.Add(subHeader_EffectTableDatas, obj.EffectTableDatas);
            // 处理SubHeader关联数组: EffectTableDataChrs
            SubHeader? subHeader_EffectTableDataChrs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EffectTableDataChr");
            if (subHeader_EffectTableDataChrs != null)
            {
                br.BaseStream.Seek(subHeader_EffectTableDataChrs.DataOffset, SeekOrigin.Begin);
                obj.EffectTableDataChrs = new EffectTableDataChr[subHeader_EffectTableDataChrs.NodeCount];
                for (var i = 0; i < subHeader_EffectTableDataChrs.NodeCount; i++)
                {
                    obj.EffectTableDataChrs[i] = EffectTableDataChrHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.EffectTableDataChrs = Array.Empty<EffectTableDataChr>();
            }
            obj.NodeDatas.Add(subHeader_EffectTableDataChrs, obj.EffectTableDataChrs);

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
            EffectTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not EffectTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: EffectTableDatas
            SubHeader? subHeader_EffectTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EffectTableData");
            if (subHeader_EffectTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_EffectTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EffectTableDatas.NodeCount; i++)
                {
                    EffectTableDataHelper.Serialize(bw, obj.EffectTableDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: EffectTableDataChrs
            SubHeader? subHeader_EffectTableDataChrs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "EffectTableDataChr");
            if (subHeader_EffectTableDataChrs != null)
            {
                bw.BaseStream.Seek(subHeader_EffectTableDataChrs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_EffectTableDataChrs.NodeCount; i++)
                {
                    EffectTableDataChrHelper.Serialize(bw, obj.EffectTableDataChrs[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class EffectTableDataHelper
    {
        public static EffectTableData DeSerialize(BinaryReader br)
        {
            var obj = new EffectTableData
            {
                Id = br.ReadInt64(),
                FilePath = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EffectTableData tbl)
        {
            if (tbl is not EffectTableData obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.FilePath);
            bw.Write(obj.Text);
        }
    }

    public static class EffectTableDataChrHelper
    {
        public static EffectTableDataChr DeSerialize(BinaryReader br)
        {
            var obj = new EffectTableDataChr();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, EffectTableDataChr tbl)
        {
            if (tbl is not EffectTableDataChr obj) return;
        }
    }
}