using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class ArchiveTableHelper
    {
        public static ArchiveTable DeSerialize(BinaryReader br)
        {
            ArchiveTable obj = TBLHelper.DeSerialize<ArchiveTable>(br);
            // 处理SubHeader关联数组: PreStoryLists
            SubHeader? subHeader_PreStoryLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PreStoryList");
            if (subHeader_PreStoryLists != null)
            {
                br.BaseStream.Seek(subHeader_PreStoryLists.DataOffset, SeekOrigin.Begin);
                obj.PreStoryLists = new PreStoryList[subHeader_PreStoryLists.NodeCount];
                for (var i = 0; i < subHeader_PreStoryLists.NodeCount; i++)
                {
                    obj.PreStoryLists[i] = PreStoryListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.PreStoryLists = Array.Empty<PreStoryList>();
            }
            obj.NodeDatas.Add(subHeader_PreStoryLists, obj.PreStoryLists);
            // 处理SubHeader关联数组: TimeLineLists
            SubHeader? subHeader_TimeLineLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TimeLineList");
            if (subHeader_TimeLineLists != null)
            {
                br.BaseStream.Seek(subHeader_TimeLineLists.DataOffset, SeekOrigin.Begin);
                obj.TimeLineLists = new TimeLineList[subHeader_TimeLineLists.NodeCount];
                for (var i = 0; i < subHeader_TimeLineLists.NodeCount; i++)
                {
                    obj.TimeLineLists[i] = TimeLineListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TimeLineLists = Array.Empty<TimeLineList>();
            }
            obj.NodeDatas.Add(subHeader_TimeLineLists, obj.TimeLineLists);
            // 处理SubHeader关联数组: SeriesLists
            SubHeader? subHeader_SeriesLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SeriesList");
            if (subHeader_SeriesLists != null)
            {
                br.BaseStream.Seek(subHeader_SeriesLists.DataOffset, SeekOrigin.Begin);
                obj.SeriesLists = new SeriesList[subHeader_SeriesLists.NodeCount];
                for (var i = 0; i < subHeader_SeriesLists.NodeCount; i++)
                {
                    obj.SeriesLists[i] = SeriesListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SeriesLists = Array.Empty<SeriesList>();
            }
            obj.NodeDatas.Add(subHeader_SeriesLists, obj.SeriesLists);
            // 处理SubHeader关联数组: SeriesContentsLists
            SubHeader? subHeader_SeriesContentsLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SeriesContents");
            if (subHeader_SeriesContentsLists != null)
            {
                br.BaseStream.Seek(subHeader_SeriesContentsLists.DataOffset, SeekOrigin.Begin);
                obj.SeriesContentsLists = new SeriesContents[subHeader_SeriesContentsLists.NodeCount];
                for (var i = 0; i < subHeader_SeriesContentsLists.NodeCount; i++)
                {
                    obj.SeriesContentsLists[i] = SeriesContentsHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.SeriesContentsLists = Array.Empty<SeriesContents>();
            }
            obj.NodeDatas.Add(subHeader_SeriesContentsLists, obj.SeriesContentsLists);
            // 处理SubHeader关联数组: LogLists
            SubHeader? subHeader_LogLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LogList");
            if (subHeader_LogLists != null)
            {
                br.BaseStream.Seek(subHeader_LogLists.DataOffset, SeekOrigin.Begin);
                obj.LogLists = new LogList[subHeader_LogLists.NodeCount];
                for (var i = 0; i < subHeader_LogLists.NodeCount; i++)
                {
                    obj.LogLists[i] = LogListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.LogLists = Array.Empty<LogList>();
            }
            obj.NodeDatas.Add(subHeader_LogLists, obj.LogLists);
            // 处理SubHeader关联数组: LogContentss
            SubHeader? subHeader_LogContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LogContents");
            if (subHeader_LogContentss != null)
            {
                br.BaseStream.Seek(subHeader_LogContentss.DataOffset, SeekOrigin.Begin);
                obj.LogContentss = new LogContents[subHeader_LogContentss.NodeCount];
                for (var i = 0; i < subHeader_LogContentss.NodeCount; i++)
                {
                    obj.LogContentss[i] = LogContentsHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.LogContentss = Array.Empty<LogContents>();
            }
            obj.NodeDatas.Add(subHeader_LogContentss, obj.LogContentss);
            // 处理SubHeader关联数组: GlossarySelects
            SubHeader? subHeader_GlossarySelects = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossarySelect");
            if (subHeader_GlossarySelects != null)
            {
                br.BaseStream.Seek(subHeader_GlossarySelects.DataOffset, SeekOrigin.Begin);
                obj.GlossarySelects = new GlossarySelect[subHeader_GlossarySelects.NodeCount];
                for (var i = 0; i < subHeader_GlossarySelects.NodeCount; i++)
                {
                    obj.GlossarySelects[i] = GlossarySelectHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GlossarySelects = Array.Empty<GlossarySelect>();
            }
            obj.NodeDatas.Add(subHeader_GlossarySelects, obj.GlossarySelects);
            // 处理SubHeader关联数组: GlossaryLists
            SubHeader? subHeader_GlossaryLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossaryList");
            if (subHeader_GlossaryLists != null)
            {
                br.BaseStream.Seek(subHeader_GlossaryLists.DataOffset, SeekOrigin.Begin);
                obj.GlossaryLists = new GlossaryList[subHeader_GlossaryLists.NodeCount];
                for (var i = 0; i < subHeader_GlossaryLists.NodeCount; i++)
                {
                    obj.GlossaryLists[i] = GlossaryListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GlossaryLists = Array.Empty<GlossaryList>();
            }
            obj.NodeDatas.Add(subHeader_GlossaryLists, obj.GlossaryLists);
            // 处理SubHeader关联数组: GlossaryContentss
            SubHeader? subHeader_GlossaryContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossaryContents");
            if (subHeader_GlossaryContentss != null)
            {
                br.BaseStream.Seek(subHeader_GlossaryContentss.DataOffset, SeekOrigin.Begin);
                obj.GlossaryContentss = new GlossaryContents[subHeader_GlossaryContentss.NodeCount];
                for (var i = 0; i < subHeader_GlossaryContentss.NodeCount; i++)
                {
                    obj.GlossaryContentss[i] = GlossaryContentsHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.GlossaryContentss = Array.Empty<GlossaryContents>();
            }
            obj.NodeDatas.Add(subHeader_GlossaryContentss, obj.GlossaryContentss);
            // 处理SubHeader关联数组: CharactorSelects
            SubHeader? subHeader_CharactorSelects = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorSelect");
            if (subHeader_CharactorSelects != null)
            {
                br.BaseStream.Seek(subHeader_CharactorSelects.DataOffset, SeekOrigin.Begin);
                obj.CharactorSelects = new CharactorSelect[subHeader_CharactorSelects.NodeCount];
                for (var i = 0; i < subHeader_CharactorSelects.NodeCount; i++)
                {
                    obj.CharactorSelects[i] = CharactorSelectHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.CharactorSelects = Array.Empty<CharactorSelect>();
            }
            obj.NodeDatas.Add(subHeader_CharactorSelects, obj.CharactorSelects);
            // 处理SubHeader关联数组: CharactorLists
            SubHeader? subHeader_CharactorLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorList");
            if (subHeader_CharactorLists != null)
            {
                br.BaseStream.Seek(subHeader_CharactorLists.DataOffset, SeekOrigin.Begin);
                obj.CharactorLists = new CharactorList[subHeader_CharactorLists.NodeCount];
                for (var i = 0; i < subHeader_CharactorLists.NodeCount; i++)
                {
                    obj.CharactorLists[i] = CharactorListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.CharactorLists = Array.Empty<CharactorList>();
            }
            obj.NodeDatas.Add(subHeader_CharactorLists, obj.CharactorLists);
            // 处理SubHeader关联数组: CharactorContentss
            SubHeader? subHeader_CharactorContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorContents");
            if (subHeader_CharactorContentss != null)
            {
                br.BaseStream.Seek(subHeader_CharactorContentss.DataOffset, SeekOrigin.Begin);
                obj.CharactorContentss = new CharactorContents[subHeader_CharactorContentss.NodeCount];
                for (var i = 0; i < subHeader_CharactorContentss.NodeCount; i++)
                {
                    obj.CharactorContentss[i] = CharactorContentsHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.CharactorContentss = Array.Empty<CharactorContents>();
            }
            obj.NodeDatas.Add(subHeader_CharactorContentss, obj.CharactorContentss);

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
            ArchiveTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not ArchiveTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: PreStoryLists
            SubHeader? subHeader_PreStoryLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "PreStoryList");
            if (subHeader_PreStoryLists != null)
            {
                bw.BaseStream.Seek(subHeader_PreStoryLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_PreStoryLists.NodeCount; i++)
                {
                    PreStoryListHelper.Serialize(bw, obj.PreStoryLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TimeLineLists
            SubHeader? subHeader_TimeLineLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TimeLineList");
            if (subHeader_TimeLineLists != null)
            {
                bw.BaseStream.Seek(subHeader_TimeLineLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TimeLineLists.NodeCount; i++)
                {
                    TimeLineListHelper.Serialize(bw, obj.TimeLineLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SeriesLists
            SubHeader? subHeader_SeriesLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SeriesList");
            if (subHeader_SeriesLists != null)
            {
                bw.BaseStream.Seek(subHeader_SeriesLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SeriesLists.NodeCount; i++)
                {
                    SeriesListHelper.Serialize(bw, obj.SeriesLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: SeriesContentsLists
            SubHeader? subHeader_SeriesContentsLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "SeriesContentsList");
            if (subHeader_SeriesContentsLists != null)
            {
                bw.BaseStream.Seek(subHeader_SeriesContentsLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_SeriesContentsLists.NodeCount; i++)
                {
                    SeriesContentsHelper.Serialize(bw, obj.SeriesContentsLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: LogLists
            SubHeader? subHeader_LogLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LogList");
            if (subHeader_LogLists != null)
            {
                bw.BaseStream.Seek(subHeader_LogLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_LogLists.NodeCount; i++)
                {
                    LogListHelper.Serialize(bw, obj.LogLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: LogContentss
            SubHeader? subHeader_LogContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "LogContents");
            if (subHeader_LogContentss != null)
            {
                bw.BaseStream.Seek(subHeader_LogContentss.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_LogContentss.NodeCount; i++)
                {
                    LogContentsHelper.Serialize(bw, obj.LogContentss[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: GlossarySelects
            SubHeader? subHeader_GlossarySelects = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossarySelect");
            if (subHeader_GlossarySelects != null)
            {
                bw.BaseStream.Seek(subHeader_GlossarySelects.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GlossarySelects.NodeCount; i++)
                {
                    GlossarySelectHelper.Serialize(bw, obj.GlossarySelects[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: GlossaryLists
            SubHeader? subHeader_GlossaryLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossaryList");
            if (subHeader_GlossaryLists != null)
            {
                bw.BaseStream.Seek(subHeader_GlossaryLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GlossaryLists.NodeCount; i++)
                {
                    GlossaryListHelper.Serialize(bw, obj.GlossaryLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: GlossaryContentss
            SubHeader? subHeader_GlossaryContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "GlossaryContents");
            if (subHeader_GlossaryContentss != null)
            {
                bw.BaseStream.Seek(subHeader_GlossaryContentss.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_GlossaryContentss.NodeCount; i++)
                {
                    GlossaryContentsHelper.Serialize(bw, obj.GlossaryContentss[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: CharactorSelects
            SubHeader? subHeader_CharactorSelects = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorSelect");
            if (subHeader_CharactorSelects != null)
            {
                bw.BaseStream.Seek(subHeader_CharactorSelects.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_CharactorSelects.NodeCount; i++)
                {
                    CharactorSelectHelper.Serialize(bw, obj.CharactorSelects[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: CharactorLists
            SubHeader? subHeader_CharactorLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorList");
            if (subHeader_CharactorLists != null)
            {
                bw.BaseStream.Seek(subHeader_CharactorLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_CharactorLists.NodeCount; i++)
                {
                    CharactorListHelper.Serialize(bw, obj.CharactorLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: CharactorContentss
            SubHeader? subHeader_CharactorContentss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CharactorContents");
            if (subHeader_CharactorContentss != null)
            {
                bw.BaseStream.Seek(subHeader_CharactorContentss.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_CharactorContentss.NodeCount; i++)
                {
                    CharactorContentsHelper.Serialize(bw, obj.CharactorContentss[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class PreStoryListHelper
    {
        public static PreStoryList DeSerialize(BinaryReader br)
        {
            var obj = new PreStoryList
            {
                Id = br.ReadUInt32(),
                Uint1 = br.ReadUInt32(),
                Game = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Long2 = br.ReadInt64(),
                Uint2 = br.ReadUInt32(),
                Uint3 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, PreStoryList tbl)
        {
            if (tbl is not PreStoryList obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Uint1);
            bw.Write(obj.Game);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Long1);
            bw.Write(obj.Long2);
            bw.Write(obj.Uint2);
            bw.Write(obj.Uint3);
        }
    }

    public static class TimeLineListHelper
    {
        public static TimeLineList DeSerialize(BinaryReader br)
        {
            var obj = new TimeLineList
            {
                Id = br.ReadUInt32(),
                Uint1 = br.ReadUInt32(),
                Title = br.ReadInt64(),
                Text = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TimeLineList tbl)
        {
            if (tbl is not TimeLineList obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Uint1);
            bw.Write(obj.Title);
            bw.Write(obj.Text);
            bw.Write(obj.Text2);
        }
    }

    public static class SeriesListHelper
    {
        public static SeriesList DeSerialize(BinaryReader br)
        {
            var obj = new SeriesList
            {
                ID = br.ReadUInt32(),
                UInt1 = br.ReadUInt32(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SeriesList tbl)
        {
            if (tbl is not SeriesList obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.UInt1);
            bw.Write(obj.Text);
        }
    }

    public static class SeriesContentsHelper
    {
        public static SeriesContents DeSerialize(BinaryReader br)
        {
            var obj = new SeriesContents
            {
                ID = br.ReadUInt32(),
                Uint1 = br.ReadUInt32(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, SeriesContents tbl)
        {
            if (tbl is not SeriesContents obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Uint1);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
        }
    }

    public static class LogListHelper
    {
        public static LogList DeSerialize(BinaryReader br)
        {
            var obj = new LogList
            {
                Uint1 = br.ReadUInt32(),
                Uint2 = br.ReadUInt32(),
                Text = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Long2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, LogList tbl)
        {
            if (tbl is not LogList obj) return;
            bw.Write(obj.Uint1);
            bw.Write(obj.Uint2);
            bw.Write(obj.Text);
            bw.Write(obj.Long1);
            bw.Write(obj.Long2);
        }
    }

    public static class LogContentsHelper
    {
        public static LogContents DeSerialize(BinaryReader br)
        {
            var obj = new LogContents
            {
                Uint1 = br.ReadUInt32(),
                Uint2 = br.ReadUInt32(),
                Long2 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, LogContents tbl)
        {
            if (tbl is not LogContents obj) return;
            bw.Write(obj.Uint1);
            bw.Write(obj.Uint2);
            bw.Write(obj.Long2);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }

    public static class GlossarySelectHelper
    {
        public static GlossarySelect DeSerialize(BinaryReader br)
        {
            var obj = new GlossarySelect
            {
                ID = br.ReadUInt32(),
                UInt1 = br.ReadUInt32(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GlossarySelect tbl)
        {
            if (tbl is not GlossarySelect obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.UInt1);
            bw.Write(obj.Text);
        }
    }

    public static class GlossaryListHelper
    {
        public static GlossaryList DeSerialize(BinaryReader br)
        {
            var obj = new GlossaryList
            {
                Short = br.ReadUInt16(),
                ID = br.ReadUInt16(),
                UInt1 = br.ReadUInt32(),
                Text = br.ReadInt64(),
                UInt2 = br.ReadUInt32(),
                UInt3 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GlossaryList tbl)
        {
            if (tbl is not GlossaryList obj) return;
            bw.Write(obj.Short);
            bw.Write(obj.ID);
            bw.Write(obj.UInt1);
            bw.Write(obj.Text);
            bw.Write(obj.UInt2);
            bw.Write(obj.UInt3);
        }
    }

    public static class GlossaryContentsHelper
    {
        public static GlossaryContents DeSerialize(BinaryReader br)
        {
            var obj = new GlossaryContents
            {
                Short = br.ReadUInt16(),
                ID = br.ReadUInt16(),
                UInt1 = br.ReadUInt32(),
                File = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, GlossaryContents tbl)
        {
            if (tbl is not GlossaryContents obj) return;
            bw.Write(obj.Short);
            bw.Write(obj.ID);
            bw.Write(obj.UInt1);
            bw.Write(obj.File);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
        }
    }

    public static class CharactorSelectHelper
    {
        public static CharactorSelect DeSerialize(BinaryReader br)
        {
            var obj = new CharactorSelect
            {
                ID = br.ReadUInt32(),
                UInt1 = br.ReadUInt32(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, CharactorSelect tbl)
        {
            if (tbl is not CharactorSelect obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.UInt1);
            bw.Write(obj.Text);
        }
    }

    public static class CharactorListHelper
    {
        public static CharactorList DeSerialize(BinaryReader br)
        {
            var obj = new CharactorList
            {
                Field1 = br.ReadInt64(),
                Field3 = br.ReadInt64(),
                Field4 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, CharactorList tbl)
        {
            if (tbl is not CharactorList obj) return;
            bw.Write(obj.Field1);
            bw.Write(obj.Field3);
            bw.Write(obj.Field4);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }

    public static class CharactorContentsHelper
    {
        public static CharactorContents DeSerialize(BinaryReader br)
        {
            var obj = new CharactorContents
            {
                Uint1 = br.ReadUInt32(),
                Uint2 = br.ReadUInt32(),
                Uint3 = br.ReadUInt32(),
                Uint4 = br.ReadUInt32(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, CharactorContents tbl)
        {
            if (tbl is not CharactorContents obj) return;
            bw.Write(obj.Uint1);
            bw.Write(obj.Uint2);
            bw.Write(obj.Uint3);
            bw.Write(obj.Uint4);
            bw.Write(obj.Text);
        }
    }
}