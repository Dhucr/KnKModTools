using System.Linq;
using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class BGMSetTableHelper
    {
        public static BGMSetTable DeSerialize(BinaryReader br)
        {
            var obj = TBLHelper.DeSerialize<BGMSetTable>(br);
            // 处理SubHeader关联数组: MapBGMs
            var subHeader_MapBGMs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapBGM");
            if (subHeader_MapBGMs != null)
            {
                br.BaseStream.Seek(subHeader_MapBGMs.DataOffset, SeekOrigin.Begin);
                obj.MapBGMs = new MapBGM[subHeader_MapBGMs.NodeCount];
                for (int i = 0; i < subHeader_MapBGMs.NodeCount; i++)
                {
                    obj.MapBGMs[i] = MapBGMHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapBGMs = Array.Empty<MapBGM>();
            }
            obj.NodeDatas.Add(subHeader_MapBGMs, obj.MapBGMs);
            // 处理SubHeader关联数组: BattleBGMs
            var subHeader_BattleBGMs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleBGM");
            if (subHeader_BattleBGMs != null)
            {
                br.BaseStream.Seek(subHeader_BattleBGMs.DataOffset, SeekOrigin.Begin);
                obj.BattleBGMs = new BattleBGM[subHeader_BattleBGMs.NodeCount];
                for (int i = 0; i < subHeader_BattleBGMs.NodeCount; i++)
                {
                    obj.BattleBGMs[i] = BattleBGMHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BattleBGMs = Array.Empty<BattleBGM>();
            }
            obj.NodeDatas.Add(subHeader_BattleBGMs, obj.BattleBGMs);
            // 处理SubHeader关联数组: ReplaceBGMParams
            var subHeader_ReplaceBGMParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ReplaceBGMParam");
            if (subHeader_ReplaceBGMParams != null)
            {
                br.BaseStream.Seek(subHeader_ReplaceBGMParams.DataOffset, SeekOrigin.Begin);
                obj.ReplaceBGMParams = new ReplaceBGMParam[subHeader_ReplaceBGMParams.NodeCount];
                for (int i = 0; i < subHeader_ReplaceBGMParams.NodeCount; i++)
                {
                    obj.ReplaceBGMParams[i] = ReplaceBGMParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ReplaceBGMParams = Array.Empty<ReplaceBGMParam>();
            }
            obj.NodeDatas.Add(subHeader_ReplaceBGMParams, obj.ReplaceBGMParams);

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
            BGMSetTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not BGMSetTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: MapBGMs
            var subHeader_MapBGMs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapBGM");
            if (subHeader_MapBGMs != null)
            {
                bw.BaseStream.Seek(subHeader_MapBGMs.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_MapBGMs.NodeCount; i++)
                {
                    MapBGMHelper.Serialize(bw, obj.MapBGMs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BattleBGMs
            var subHeader_BattleBGMs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleBGM");
            if (subHeader_BattleBGMs != null)
            {
                bw.BaseStream.Seek(subHeader_BattleBGMs.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_BattleBGMs.NodeCount; i++)
                {
                    BattleBGMHelper.Serialize(bw, obj.BattleBGMs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ReplaceBGMParams
            var subHeader_ReplaceBGMParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ReplaceBGMParam");
            if (subHeader_ReplaceBGMParams != null)
            {
                bw.BaseStream.Seek(subHeader_ReplaceBGMParams.DataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subHeader_ReplaceBGMParams.NodeCount; i++)
                {
                    ReplaceBGMParamHelper.Serialize(bw, obj.ReplaceBGMParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class MapBGMHelper
    {
        public static MapBGM DeSerialize(BinaryReader br)
        {
            var obj = new MapBGM();
            obj.Long1 = br.ReadInt64();
            obj.Arr1 = br.ReadInt64();
            obj.Count1 = br.ReadInt64();
            obj.Arr2 = br.ReadInt64();
            obj.Count2 = br.ReadInt32();
            obj.Int1 = br.ReadInt32();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MapBGM tbl)
        {
            if (tbl is not MapBGM obj) return;
            bw.Write(obj.Long1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Int1);
        }
    }

    public static class BattleBGMHelper
    {
        public static BattleBGM DeSerialize(BinaryReader br)
        {
            var obj = new BattleBGM();
            obj.ID = br.ReadInt64();
            obj.Arr1 = br.ReadInt64();
            obj.Count1 = br.ReadInt64();
            obj.Arr2 = br.ReadInt64();
            obj.Count2 = (int)br.ReadInt64();
            obj.Int1 = br.ReadInt32();
            obj.Int2 = br.ReadInt32();
            obj.Int3 = br.ReadInt32();
            obj.Int4 = br.ReadInt32();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BattleBGM tbl)
        {
            if (tbl is not BattleBGM obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
        }
    }

    public static class ReplaceBGMParamHelper
    {
        public static ReplaceBGMParam DeSerialize(BinaryReader br)
        {
            var obj = new ReplaceBGMParam();
            obj.ID = br.ReadUInt32();
            obj.Int1 = br.ReadInt32();
            obj.Flag = br.ReadInt64();
            obj.TrackName1 = br.ReadInt64();
            obj.TrackName2 = br.ReadInt64();
            obj.Ost = br.ReadInt64();
            obj.SoundFile = br.ReadInt64();
            obj.Short1 = br.ReadUInt16();
            obj.Short2 = br.ReadUInt16();
            obj.Float = br.ReadSingle();
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ReplaceBGMParam tbl)
        {
            if (tbl is not ReplaceBGMParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Flag);
            bw.Write(obj.TrackName1);
            bw.Write(obj.TrackName2);
            bw.Write(obj.Ost);
            bw.Write(obj.SoundFile);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Float);
        }
    }

}

