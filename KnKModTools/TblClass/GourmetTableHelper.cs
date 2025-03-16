using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class GourmetTableHelper
    {
        public static GourmetTable DeSerialize(BinaryReader br)
        {
            GourmetTable obj = TBLHelper.DeSerialize<GourmetTable>(br);
            // 处理SubHeader关联数组: GourmetParams
            SubHeader? subHeader_GourmetParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetParam");
            if (subHeader_GourmetParams != null)
            {
                br.BaseStream.Seek(subHeader_GourmetParams.DataOffset, SeekOrigin.Begin);
                obj.GourmetParams = new GourmetParam[subHeader_GourmetParams.NodeCount];
                for (var i = 0; i < subHeader_GourmetParams.NodeCount; i++)
                {
                    obj.GourmetParams[i] = GourmetParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GourmetParams = Array.Empty<GourmetParam>();
            }
            obj.NodeDatas.Add(subHeader_GourmetParams, obj.GourmetParams);
            // 处理SubHeader关联数组: GourmetRankParams
            SubHeader? subHeader_GourmetRankParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetRankParam");
            if (subHeader_GourmetRankParams != null)
            {
                br.BaseStream.Seek(subHeader_GourmetRankParams.DataOffset, SeekOrigin.Begin);
                obj.GourmetRankParams = new GourmetRankParam[subHeader_GourmetRankParams.NodeCount];
                for (var i = 0; i < subHeader_GourmetRankParams.NodeCount; i++)
                {
                    obj.GourmetRankParams[i] = GourmetRankParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GourmetRankParams = Array.Empty<GourmetRankParam>();
            }
            obj.NodeDatas.Add(subHeader_GourmetRankParams, obj.GourmetRankParams);
            // 处理SubHeader关联数组: GourmetChrs
            SubHeader? subHeader_GourmetChrs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetChr");
            if (subHeader_GourmetChrs != null)
            {
                br.BaseStream.Seek(subHeader_GourmetChrs.DataOffset, SeekOrigin.Begin);
                obj.GourmetChrs = new GourmetChr[subHeader_GourmetChrs.NodeCount];
                for (var i = 0; i < subHeader_GourmetChrs.NodeCount; i++)
                {
                    obj.GourmetChrs[i] = GourmetChrHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GourmetChrs = Array.Empty<GourmetChr>();
            }
            obj.NodeDatas.Add(subHeader_GourmetChrs, obj.GourmetChrs);

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
            GourmetTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not GourmetTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: GourmetParams
            SubHeader? subHeader_GourmetParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetParam");
            if (subHeader_GourmetParams != null)
            {
                bw.BaseStream.Seek(subHeader_GourmetParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GourmetParams.NodeCount; i++)
                {
                    GourmetParamHelper.Serialize(bw, obj.GourmetParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: GourmetRankParams
            SubHeader? subHeader_GourmetRankParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetRankParam");
            if (subHeader_GourmetRankParams != null)
            {
                bw.BaseStream.Seek(subHeader_GourmetRankParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GourmetRankParams.NodeCount; i++)
                {
                    GourmetRankParamHelper.Serialize(bw, obj.GourmetRankParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: GourmetChrs
            SubHeader? subHeader_GourmetChrs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GourmetChr");
            if (subHeader_GourmetChrs != null)
            {
                bw.BaseStream.Seek(subHeader_GourmetChrs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GourmetChrs.NodeCount; i++)
                {
                    GourmetChrHelper.Serialize(bw, obj.GourmetChrs[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class GourmetParamHelper
    {
        public static GourmetParam DeSerialize(BinaryReader br)
        {
            var obj = new GourmetParam
            {
                ID = br.ReadInt16(),
                Short = br.ReadInt16(),
                Int1 = br.ReadInt32(),
                Description = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Uint1 = br.ReadUInt32(),
                Uint2 = br.ReadUInt32(),
                Long2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GourmetParam tbl)
        {
            if (tbl is not GourmetParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short);
            bw.Write(obj.Int1);
            bw.Write(obj.Description);
            bw.Write(obj.Long1);
            bw.Write(obj.Text1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
            bw.Write(obj.Uint1);
            bw.Write(obj.Uint2);
            bw.Write(obj.Long2);
        }
    }

    public static class GourmetRankParamHelper
    {
        public static GourmetRankParam DeSerialize(BinaryReader br)
        {
            var obj = new GourmetRankParam
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                RankUpText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GourmetRankParam tbl)
        {
            if (tbl is not GourmetRankParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.RankUpText);
        }
    }

    public static class GourmetChrHelper
    {
        public static GourmetChr DeSerialize(BinaryReader br)
        {
            var obj = new GourmetChr
            {
                Value = br.ReadInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GourmetChr tbl)
        {
            if (tbl is not GourmetChr obj) return;
            bw.Write(obj.Value);
        }
    }
}