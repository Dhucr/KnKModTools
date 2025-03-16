using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ChrdataTableHelper
    {
        public static ChrdataTable DeSerialize(BinaryReader br)
        {
            ChrdataTable obj = TBLHelper.DeSerialize<ChrdataTable>(br);
            // 处理SubHeader关联数组: ChrDataParams
            SubHeader? subHeader_ChrDataParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ChrDataParam");
            if (subHeader_ChrDataParams != null)
            {
                br.BaseStream.Seek(subHeader_ChrDataParams.DataOffset, SeekOrigin.Begin);
                obj.ChrDataParams = new ChrDataParam[subHeader_ChrDataParams.NodeCount];
                for (var i = 0; i < subHeader_ChrDataParams.NodeCount; i++)
                {
                    obj.ChrDataParams[i] = ChrDataParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ChrDataParams = Array.Empty<ChrDataParam>();
            }
            obj.NodeDatas.Add(subHeader_ChrDataParams, obj.ChrDataParams);
            // 处理SubHeader关联数组: TalkChrDatas
            SubHeader? subHeader_TalkChrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TalkChrData");
            if (subHeader_TalkChrDatas != null)
            {
                br.BaseStream.Seek(subHeader_TalkChrDatas.DataOffset, SeekOrigin.Begin);
                obj.TalkChrDatas = new TalkChrData[subHeader_TalkChrDatas.NodeCount];
                for (var i = 0; i < subHeader_TalkChrDatas.NodeCount; i++)
                {
                    obj.TalkChrDatas[i] = TalkChrDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TalkChrDatas = Array.Empty<TalkChrData>();
            }
            obj.NodeDatas.Add(subHeader_TalkChrDatas, obj.TalkChrDatas);
            // 处理SubHeader关联数组: PortraitChrDatas
            SubHeader? subHeader_PortraitChrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PortraitChrData");
            if (subHeader_PortraitChrDatas != null)
            {
                br.BaseStream.Seek(subHeader_PortraitChrDatas.DataOffset, SeekOrigin.Begin);
                obj.PortraitChrDatas = new PortraitChrData[subHeader_PortraitChrDatas.NodeCount];
                for (var i = 0; i < subHeader_PortraitChrDatas.NodeCount; i++)
                {
                    obj.PortraitChrDatas[i] = PortraitChrDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.PortraitChrDatas = Array.Empty<PortraitChrData>();
            }
            obj.NodeDatas.Add(subHeader_PortraitChrDatas, obj.PortraitChrDatas);

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
            ChrdataTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ChrdataTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ChrDataParams
            SubHeader? subHeader_ChrDataParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ChrDataParam");
            if (subHeader_ChrDataParams != null)
            {
                bw.BaseStream.Seek(subHeader_ChrDataParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ChrDataParams.NodeCount; i++)
                {
                    ChrDataParamHelper.Serialize(bw, obj.ChrDataParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TalkChrDatas
            SubHeader? subHeader_TalkChrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TalkChrData");
            if (subHeader_TalkChrDatas != null)
            {
                bw.BaseStream.Seek(subHeader_TalkChrDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TalkChrDatas.NodeCount; i++)
                {
                    TalkChrDataHelper.Serialize(bw, obj.TalkChrDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: PortraitChrDatas
            SubHeader? subHeader_PortraitChrDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PortraitChrData");
            if (subHeader_PortraitChrDatas != null)
            {
                bw.BaseStream.Seek(subHeader_PortraitChrDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_PortraitChrDatas.NodeCount; i++)
                {
                    PortraitChrDataHelper.Serialize(bw, obj.PortraitChrDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ChrDataParamHelper
    {
        public static ChrDataParam DeSerialize(BinaryReader br)
        {
            var obj = new ChrDataParam
            {
                ID = br.ReadInt64(),
                FileName = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Float5 = br.ReadSingle(),
                Float6 = br.ReadSingle(),
                Float7 = br.ReadSingle(),
                Float8 = br.ReadSingle(),
                Float9 = br.ReadSingle(),
                Float10 = br.ReadSingle(),
                Float11 = br.ReadSingle(),
                Float12 = br.ReadSingle(),
                Text = br.ReadInt64(),
                Float13 = br.ReadSingle(),
                Float14 = br.ReadSingle(),
                Float15 = br.ReadSingle(),
                Float16 = br.ReadSingle(),
                Int1 = br.ReadInt32(),
                Float17 = br.ReadSingle(),
                Float18 = br.ReadSingle(),
                Int2 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ChrDataParam tbl)
        {
            if (tbl is not ChrDataParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.FileName);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Float4);
            bw.Write(obj.Float5);
            bw.Write(obj.Float6);
            bw.Write(obj.Float7);
            bw.Write(obj.Float8);
            bw.Write(obj.Float9);
            bw.Write(obj.Float10);
            bw.Write(obj.Float11);
            bw.Write(obj.Float12);
            bw.Write(obj.Text);
            bw.Write(obj.Float13);
            bw.Write(obj.Float14);
            bw.Write(obj.Float15);
            bw.Write(obj.Float16);
            bw.Write(obj.Int1);
            bw.Write(obj.Float17);
            bw.Write(obj.Float18);
            bw.Write(obj.Int2);
        }
    }

    public static class TalkChrDataHelper
    {
        public static TalkChrData DeSerialize(BinaryReader br)
        {
            var obj = new TalkChrData
            {
                ID = br.ReadInt64(),
                Array1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Array2 = br.ReadInt64(),
                Count2 = br.ReadInt32(),
                Int1 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TalkChrData tbl)
        {
            if (tbl is not TalkChrData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Array1);
            bw.Write(obj.Count1);
            bw.Write(obj.Array2);
            bw.Write(obj.Count2);
            bw.Write(obj.Int1);
        }
    }

    public static class PortraitChrDataHelper
    {
        public static PortraitChrData DeSerialize(BinaryReader br)
        {
            var obj = new PortraitChrData
            {
                ID = br.ReadInt64(),
                FileName = br.ReadInt64(),
                Text = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Empty = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, PortraitChrData tbl)
        {
            if (tbl is not PortraitChrData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.FileName);
            bw.Write(obj.Text);
            bw.Write(obj.Float1);
            bw.Write(obj.Empty);
        }
    }
}