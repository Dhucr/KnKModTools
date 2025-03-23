using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class MapJumpTableHelper
    {
        public static MapJumpTable DeSerialize(BinaryReader br)
        {
            MapJumpTable obj = TBLHelper.DeSerialize<MapJumpTable>(br);
            // 处理SubHeader关联数组: MapJumpWorldDatas
            SubHeader? subHeader_MapJumpWorldDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpWorldData");
            if (subHeader_MapJumpWorldDatas != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpWorldDatas.DataOffset, SeekOrigin.Begin);
                obj.MapJumpWorldDatas = new MapJumpWorldData[subHeader_MapJumpWorldDatas.NodeCount];
                for (var i = 0; i < subHeader_MapJumpWorldDatas.NodeCount; i++)
                {
                    obj.MapJumpWorldDatas[i] = MapJumpWorldDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpWorldDatas = Array.Empty<MapJumpWorldData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpWorldDatas, obj.MapJumpWorldDatas);
            // 处理SubHeader关联数组: MapJumpRegionDatas
            SubHeader? subHeader_MapJumpRegionDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpRegionData");
            if (subHeader_MapJumpRegionDatas != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpRegionDatas.DataOffset, SeekOrigin.Begin);
                obj.MapJumpRegionDatas = new MapJumpRegionData[subHeader_MapJumpRegionDatas.NodeCount];
                for (var i = 0; i < subHeader_MapJumpRegionDatas.NodeCount; i++)
                {
                    obj.MapJumpRegionDatas[i] = MapJumpRegionDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpRegionDatas = Array.Empty<MapJumpRegionData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpRegionDatas, obj.MapJumpRegionDatas);
            // 处理SubHeader关联数组: MapJumpAreaDatas
            SubHeader? subHeader_MapJumpAreaDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpAreaData");
            if (subHeader_MapJumpAreaDatas != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpAreaDatas.DataOffset, SeekOrigin.Begin);
                obj.MapJumpAreaDatas = new MapJumpAreaData[subHeader_MapJumpAreaDatas.NodeCount];
                for (var i = 0; i < subHeader_MapJumpAreaDatas.NodeCount; i++)
                {
                    obj.MapJumpAreaDatas[i] = MapJumpAreaDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpAreaDatas = Array.Empty<MapJumpAreaData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpAreaDatas, obj.MapJumpAreaDatas);

            SubHeader[] subHeader_MapJumpSpotDatas = obj.Nodes.Where(n => n.DisplayName == "MapJumpSpotData").ToArray();
            // 处理SubHeader关联数组: MapJumpSpotData1s
            SubHeader? subHeader_MapJumpSpotData1s = subHeader_MapJumpSpotDatas[0];
            if (subHeader_MapJumpSpotData1s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData1s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotDatas = new MapJumpSpotData[subHeader_MapJumpSpotData1s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData1s.NodeCount; i++)
                {
                    obj.MapJumpSpotDatas[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotDatas = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData1s, obj.MapJumpSpotDatas);

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
            MapJumpTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not MapJumpTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: MapJumpWorldDatas
            SubHeader? subHeader_MapJumpWorldDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpWorldData");
            if (subHeader_MapJumpWorldDatas != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpWorldDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpWorldDatas.NodeCount; i++)
                {
                    MapJumpWorldDataHelper.Serialize(bw, obj.MapJumpWorldDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpRegionDatas
            SubHeader? subHeader_MapJumpRegionDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpRegionData");
            if (subHeader_MapJumpRegionDatas != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpRegionDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpRegionDatas.NodeCount; i++)
                {
                    MapJumpRegionDataHelper.Serialize(bw, obj.MapJumpRegionDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpAreaDatas
            SubHeader? subHeader_MapJumpAreaDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "MapJumpAreaData");
            if (subHeader_MapJumpAreaDatas != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpAreaDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpAreaDatas.NodeCount; i++)
                {
                    MapJumpAreaDataHelper.Serialize(bw, obj.MapJumpAreaDatas[i]);
                }
            }

            SubHeader[] subHeader_MapJumpSpotDatas = obj.Nodes.Where(n => n.DisplayName == "MapJumpSpotData").ToArray();
            // 处理SubHeader关联数组的序列化: MapJumpSpotData1s
            SubHeader subHeader_MapJumpSpotData1s = subHeader_MapJumpSpotDatas[0];
            if (subHeader_MapJumpSpotData1s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData1s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData1s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class MapJumpWorldDataHelper
    {
        public static MapJumpWorldData DeSerialize(BinaryReader br)
        {
            var obj = new MapJumpWorldData
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MapJumpWorldData tbl)
        {
            if (tbl is not MapJumpWorldData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
        }
    }

    public static class MapJumpRegionDataHelper
    {
        public static MapJumpRegionData DeSerialize(BinaryReader br)
        {
            var obj = new MapJumpRegionData
            {
                ID = br.ReadInt64(),
                Name = br.ReadInt64(),
                Flag = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MapJumpRegionData tbl)
        {
            if (tbl is not MapJumpRegionData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Name);
            bw.Write(obj.Flag);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
        }
    }

    public static class MapJumpAreaDataHelper
    {
        public static MapJumpAreaData DeSerialize(BinaryReader br)
        {
            var obj = new MapJumpAreaData
            {
                ID = br.ReadInt64(),
                PlaceName = br.ReadInt64(),
                PlaceNameHighlight = br.ReadInt64(),
                ScenaFile = br.ReadInt64(),
                Int1 = br.ReadUInt32(),
                Int2 = br.ReadUInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Place = br.ReadInt64(),
                JumpFile = br.ReadInt64(),
                Description = br.ReadInt64(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MapJumpAreaData tbl)
        {
            if (tbl is not MapJumpAreaData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.PlaceName);
            bw.Write(obj.PlaceNameHighlight);
            bw.Write(obj.ScenaFile);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Place);
            bw.Write(obj.JumpFile);
            bw.Write(obj.Description);
            bw.Write(obj.Long1);
        }
    }

    public static class MapJumpSpotDataHelper
    {
        public static MapJumpSpotData DeSerialize(BinaryReader br)
        {
            var obj = new MapJumpSpotData
            {
                ID = br.ReadInt64(),
                PlaceName = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                ScenaFile = br.ReadInt64(),
                Toffset1 = br.ReadInt64(),
                XCoordinate = br.ReadSingle(),
                YCoordinate = br.ReadSingle(),
                ZCoordinate = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Long2 = br.ReadInt64(),
                JumpFile = br.ReadInt64(),
                Toffset2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, MapJumpSpotData tbl)
        {
            if (tbl is not MapJumpSpotData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.PlaceName);
            bw.Write(obj.Long1);
            bw.Write(obj.ScenaFile);
            bw.Write(obj.Toffset1);
            bw.Write(obj.XCoordinate);
            bw.Write(obj.YCoordinate);
            bw.Write(obj.ZCoordinate);
            bw.Write(obj.Float4);
            bw.Write(obj.Long2);
            bw.Write(obj.JumpFile);
            bw.Write(obj.Toffset2);
        }
    }
}