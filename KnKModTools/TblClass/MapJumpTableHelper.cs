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
                obj.MapJumpSpotData1s = new MapJumpSpotData[subHeader_MapJumpSpotData1s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData1s.NodeCount; i++)
                {
                    obj.MapJumpSpotData1s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData1s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData1s, obj.MapJumpSpotData1s);
            // 处理SubHeader关联数组: MapJumpSpotData2s
            SubHeader? subHeader_MapJumpSpotData2s = subHeader_MapJumpSpotDatas[1];
            if (subHeader_MapJumpSpotData2s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData2s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData2s = new MapJumpSpotData[subHeader_MapJumpSpotData2s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData2s.NodeCount; i++)
                {
                    obj.MapJumpSpotData2s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData2s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData2s, obj.MapJumpSpotData2s);
            // 处理SubHeader关联数组: MapJumpSpotData3s
            SubHeader? subHeader_MapJumpSpotData3s = subHeader_MapJumpSpotDatas[2];
            if (subHeader_MapJumpSpotData3s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData3s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData3s = new MapJumpSpotData[subHeader_MapJumpSpotData3s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData3s.NodeCount; i++)
                {
                    obj.MapJumpSpotData3s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData3s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData3s, obj.MapJumpSpotData3s);
            // 处理SubHeader关联数组: MapJumpSpotData4s
            SubHeader? subHeader_MapJumpSpotData4s = subHeader_MapJumpSpotDatas[3];
            if (subHeader_MapJumpSpotData4s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData4s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData4s = new MapJumpSpotData[subHeader_MapJumpSpotData4s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData4s.NodeCount; i++)
                {
                    obj.MapJumpSpotData4s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData4s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData4s, obj.MapJumpSpotData4s);
            // 处理SubHeader关联数组: MapJumpSpotData5s
            SubHeader? subHeader_MapJumpSpotData5s = subHeader_MapJumpSpotDatas[4];
            if (subHeader_MapJumpSpotData5s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData5s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData5s = new MapJumpSpotData[subHeader_MapJumpSpotData5s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData5s.NodeCount; i++)
                {
                    obj.MapJumpSpotData5s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData5s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData5s, obj.MapJumpSpotData5s);
            // 处理SubHeader关联数组: MapJumpSpotData6s
            SubHeader? subHeader_MapJumpSpotData6s = subHeader_MapJumpSpotDatas[5];
            if (subHeader_MapJumpSpotData6s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData6s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData6s = new MapJumpSpotData[subHeader_MapJumpSpotData6s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData6s.NodeCount; i++)
                {
                    obj.MapJumpSpotData6s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData6s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData6s, obj.MapJumpSpotData6s);
            // 处理SubHeader关联数组: MapJumpSpotData7s
            SubHeader? subHeader_MapJumpSpotData7s = subHeader_MapJumpSpotDatas[6];
            if (subHeader_MapJumpSpotData7s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData7s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData7s = new MapJumpSpotData[subHeader_MapJumpSpotData7s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData7s.NodeCount; i++)
                {
                    obj.MapJumpSpotData7s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData7s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData7s, obj.MapJumpSpotData7s);
            // 处理SubHeader关联数组: MapJumpSpotData8s
            SubHeader? subHeader_MapJumpSpotData8s = subHeader_MapJumpSpotDatas[7];
            if (subHeader_MapJumpSpotData8s != null)
            {
                br.BaseStream.Seek(subHeader_MapJumpSpotData8s.DataOffset, SeekOrigin.Begin);
                obj.MapJumpSpotData8s = new MapJumpSpotData[subHeader_MapJumpSpotData8s.NodeCount];
                for (var i = 0; i < subHeader_MapJumpSpotData8s.NodeCount; i++)
                {
                    obj.MapJumpSpotData8s[i] = MapJumpSpotDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.MapJumpSpotData8s = Array.Empty<MapJumpSpotData>();
            }
            obj.NodeDatas.Add(subHeader_MapJumpSpotData8s, obj.MapJumpSpotData8s);

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
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData1s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData2s
            SubHeader subHeader_MapJumpSpotData2s = subHeader_MapJumpSpotDatas[1];
            if (subHeader_MapJumpSpotData2s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData2s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData2s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData2s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData3s
            SubHeader subHeader_MapJumpSpotData3s = subHeader_MapJumpSpotDatas[2];
            if (subHeader_MapJumpSpotData3s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData3s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData3s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData3s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData4s
            SubHeader subHeader_MapJumpSpotData4s = subHeader_MapJumpSpotDatas[3];
            if (subHeader_MapJumpSpotData4s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData4s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData4s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData4s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData5s
            SubHeader subHeader_MapJumpSpotData5s = subHeader_MapJumpSpotDatas[4];
            if (subHeader_MapJumpSpotData5s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData5s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData5s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData5s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData6s
            SubHeader subHeader_MapJumpSpotData6s = subHeader_MapJumpSpotDatas[5];
            if (subHeader_MapJumpSpotData6s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData6s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData6s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData6s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData7s
            SubHeader subHeader_MapJumpSpotData7s = subHeader_MapJumpSpotDatas[6];
            if (subHeader_MapJumpSpotData7s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData7s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData7s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData7s[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: MapJumpSpotData8s
            SubHeader subHeader_MapJumpSpotData8s = subHeader_MapJumpSpotDatas[7];
            if (subHeader_MapJumpSpotData8s != null)
            {
                bw.BaseStream.Seek(subHeader_MapJumpSpotData8s.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_MapJumpSpotData8s.NodeCount; i++)
                {
                    MapJumpSpotDataHelper.Serialize(bw, obj.MapJumpSpotData8s[i]);
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