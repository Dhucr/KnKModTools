using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class NoteMenuTableHelper
    {
        public static NoteMenuTable DeSerialize(BinaryReader br)
        {
            NoteMenuTable obj = TBLHelper.DeSerialize<NoteMenuTable>(br);
            // 处理SubHeader关联数组: NoteCategoryTabs
            SubHeader? subHeader_NoteCategoryTabs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteCategoryTab");
            if (subHeader_NoteCategoryTabs != null)
            {
                br.BaseStream.Seek(subHeader_NoteCategoryTabs.DataOffset, SeekOrigin.Begin);
                obj.NoteCategoryTabs = new NoteCategoryTab[subHeader_NoteCategoryTabs.NodeCount];
                for (var i = 0; i < subHeader_NoteCategoryTabs.NodeCount; i++)
                {
                    obj.NoteCategoryTabs[i] = NoteCategoryTabHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteCategoryTabs = Array.Empty<NoteCategoryTab>();
            }
            obj.NodeDatas.Add(subHeader_NoteCategoryTabs, obj.NoteCategoryTabs);
            // 处理SubHeader关联数组: NoteChapterMenus
            SubHeader? subHeader_NoteChapterMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteChapterMenu");
            if (subHeader_NoteChapterMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteChapterMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteChapterMenus = new NoteChapterMenu[subHeader_NoteChapterMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteChapterMenus.NodeCount; i++)
                {
                    obj.NoteChapterMenus[i] = NoteChapterMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteChapterMenus = Array.Empty<NoteChapterMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteChapterMenus, obj.NoteChapterMenus);
            // 处理SubHeader关联数组: NoteMainCharas
            SubHeader? subHeader_NoteMainCharas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMainChara");
            if (subHeader_NoteMainCharas != null)
            {
                br.BaseStream.Seek(subHeader_NoteMainCharas.DataOffset, SeekOrigin.Begin);
                obj.NoteMainCharas = new NoteMainChara[subHeader_NoteMainCharas.NodeCount];
                for (var i = 0; i < subHeader_NoteMainCharas.NodeCount; i++)
                {
                    obj.NoteMainCharas[i] = NoteMainCharaHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteMainCharas = Array.Empty<NoteMainChara>();
            }
            obj.NodeDatas.Add(subHeader_NoteMainCharas, obj.NoteMainCharas);
            // 处理SubHeader关联数组: NoteConnectMenus
            SubHeader? subHeader_NoteConnectMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteConnectMenu");
            if (subHeader_NoteConnectMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteConnectMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteConnectMenus = new NoteConnectMenu[subHeader_NoteConnectMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteConnectMenus.NodeCount; i++)
                {
                    obj.NoteConnectMenus[i] = NoteConnectMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteConnectMenus = Array.Empty<NoteConnectMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteConnectMenus, obj.NoteConnectMenus);
            // 处理SubHeader关联数组: NoteMonsAreaMenus
            SubHeader? subHeader_NoteMonsAreaMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsAreaMenu");
            if (subHeader_NoteMonsAreaMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteMonsAreaMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteMonsAreaMenus = new NoteMonsAreaMenu[subHeader_NoteMonsAreaMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteMonsAreaMenus.NodeCount; i++)
                {
                    obj.NoteMonsAreaMenus[i] = NoteMonsAreaMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteMonsAreaMenus = Array.Empty<NoteMonsAreaMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteMonsAreaMenus, obj.NoteMonsAreaMenus);
            // 处理SubHeader关联数组: NoteMonsSpotMenus
            SubHeader? subHeader_NoteMonsSpotMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsSpotMenu");
            if (subHeader_NoteMonsSpotMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteMonsSpotMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteMonsSpotMenus = new NoteMonsSpotMenu[subHeader_NoteMonsSpotMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteMonsSpotMenus.NodeCount; i++)
                {
                    obj.NoteMonsSpotMenus[i] = NoteMonsSpotMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteMonsSpotMenus = Array.Empty<NoteMonsSpotMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteMonsSpotMenus, obj.NoteMonsSpotMenus);
            // 处理SubHeader关联数组: NoteFishingMenus
            SubHeader? subHeader_NoteFishingMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteFishingMenu");
            if (subHeader_NoteFishingMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteFishingMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteFishingMenus = new NoteFishingMenu[subHeader_NoteFishingMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteFishingMenus.NodeCount; i++)
                {
                    obj.NoteFishingMenus[i] = NoteFishingMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteFishingMenus = Array.Empty<NoteFishingMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteFishingMenus, obj.NoteFishingMenus);
            // 处理SubHeader关联数组: NoteGourmetMenus
            SubHeader? subHeader_NoteGourmetMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGourmetMenu");
            if (subHeader_NoteGourmetMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteGourmetMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteGourmetMenus = new NoteGourmetMenu[subHeader_NoteGourmetMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteGourmetMenus.NodeCount; i++)
                {
                    obj.NoteGourmetMenus[i] = NoteGourmetMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteGourmetMenus = Array.Empty<NoteGourmetMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteGourmetMenus, obj.NoteGourmetMenus);
            // 处理SubHeader关联数组: NoteGourmetAreas
            SubHeader? subHeader_NoteGourmetAreas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGourmetArea");
            if (subHeader_NoteGourmetAreas != null)
            {
                br.BaseStream.Seek(subHeader_NoteGourmetAreas.DataOffset, SeekOrigin.Begin);
                obj.NoteGourmetAreas = new NoteGourmetArea[subHeader_NoteGourmetAreas.NodeCount];
                for (var i = 0; i < subHeader_NoteGourmetAreas.NodeCount; i++)
                {
                    obj.NoteGourmetAreas[i] = NoteGourmetAreaHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteGourmetAreas = Array.Empty<NoteGourmetArea>();
            }
            obj.NodeDatas.Add(subHeader_NoteGourmetAreas, obj.NoteGourmetAreas);
            // 处理SubHeader关联数组: NoteBooksMenus
            SubHeader? subHeader_NoteBooksMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteBooksMenu");
            if (subHeader_NoteBooksMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteBooksMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteBooksMenus = new NoteBooksMenu[subHeader_NoteBooksMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteBooksMenus.NodeCount; i++)
                {
                    obj.NoteBooksMenus[i] = NoteBooksMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteBooksMenus = Array.Empty<NoteBooksMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteBooksMenus, obj.NoteBooksMenus);
            // 处理SubHeader关联数组: NoteHelpMenus
            SubHeader? subHeader_NoteHelpMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteHelpMenu");
            if (subHeader_NoteHelpMenus != null)
            {
                br.BaseStream.Seek(subHeader_NoteHelpMenus.DataOffset, SeekOrigin.Begin);
                obj.NoteHelpMenus = new NoteHelpMenu[subHeader_NoteHelpMenus.NodeCount];
                for (var i = 0; i < subHeader_NoteHelpMenus.NodeCount; i++)
                {
                    obj.NoteHelpMenus[i] = NoteHelpMenuHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteHelpMenus = Array.Empty<NoteHelpMenu>();
            }
            obj.NodeDatas.Add(subHeader_NoteHelpMenus, obj.NoteHelpMenus);
            // 处理SubHeader关联数组: NoteMonsRecordDatas
            SubHeader? subHeader_NoteMonsRecordDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsRecordData");
            if (subHeader_NoteMonsRecordDatas != null)
            {
                br.BaseStream.Seek(subHeader_NoteMonsRecordDatas.DataOffset, SeekOrigin.Begin);
                obj.NoteMonsRecordDatas = new NoteMonsRecordData[subHeader_NoteMonsRecordDatas.NodeCount];
                for (var i = 0; i < subHeader_NoteMonsRecordDatas.NodeCount; i++)
                {
                    obj.NoteMonsRecordDatas[i] = NoteMonsRecordDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteMonsRecordDatas = Array.Empty<NoteMonsRecordData>();
            }
            obj.NodeDatas.Add(subHeader_NoteMonsRecordDatas, obj.NoteMonsRecordDatas);
            // 处理SubHeader关联数组: NoteGenesisDatas
            SubHeader? subHeader_NoteGenesisDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGenesisData");
            if (subHeader_NoteGenesisDatas != null)
            {
                br.BaseStream.Seek(subHeader_NoteGenesisDatas.DataOffset, SeekOrigin.Begin);
                obj.NoteGenesisDatas = new NoteGenesisData[subHeader_NoteGenesisDatas.NodeCount];
                for (var i = 0; i < subHeader_NoteGenesisDatas.NodeCount; i++)
                {
                    obj.NoteGenesisDatas[i] = NoteGenesisDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.NoteGenesisDatas = Array.Empty<NoteGenesisData>();
            }
            obj.NodeDatas.Add(subHeader_NoteGenesisDatas, obj.NoteGenesisDatas);

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
            NoteMenuTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not NoteMenuTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: NoteCategoryTabs
            SubHeader? subHeader_NoteCategoryTabs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteCategoryTab");
            if (subHeader_NoteCategoryTabs != null)
            {
                bw.BaseStream.Seek(subHeader_NoteCategoryTabs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteCategoryTabs.NodeCount; i++)
                {
                    NoteCategoryTabHelper.Serialize(bw, obj.NoteCategoryTabs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteChapterMenus
            SubHeader? subHeader_NoteChapterMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteChapterMenu");
            if (subHeader_NoteChapterMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteChapterMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteChapterMenus.NodeCount; i++)
                {
                    NoteChapterMenuHelper.Serialize(bw, obj.NoteChapterMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteMainCharas
            SubHeader? subHeader_NoteMainCharas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMainChara");
            if (subHeader_NoteMainCharas != null)
            {
                bw.BaseStream.Seek(subHeader_NoteMainCharas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteMainCharas.NodeCount; i++)
                {
                    NoteMainCharaHelper.Serialize(bw, obj.NoteMainCharas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteConnectMenus
            SubHeader? subHeader_NoteConnectMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteConnectMenu");
            if (subHeader_NoteConnectMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteConnectMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteConnectMenus.NodeCount; i++)
                {
                    NoteConnectMenuHelper.Serialize(bw, obj.NoteConnectMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteMonsAreaMenus
            SubHeader? subHeader_NoteMonsAreaMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsAreaMenu");
            if (subHeader_NoteMonsAreaMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteMonsAreaMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteMonsAreaMenus.NodeCount; i++)
                {
                    NoteMonsAreaMenuHelper.Serialize(bw, obj.NoteMonsAreaMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteMonsSpotMenus
            SubHeader? subHeader_NoteMonsSpotMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsSpotMenu");
            if (subHeader_NoteMonsSpotMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteMonsSpotMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteMonsSpotMenus.NodeCount; i++)
                {
                    NoteMonsSpotMenuHelper.Serialize(bw, obj.NoteMonsSpotMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteFishingMenus
            SubHeader? subHeader_NoteFishingMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteFishingMenu");
            if (subHeader_NoteFishingMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteFishingMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteFishingMenus.NodeCount; i++)
                {
                    NoteFishingMenuHelper.Serialize(bw, obj.NoteFishingMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteGourmetMenus
            SubHeader? subHeader_NoteGourmetMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGourmetMenu");
            if (subHeader_NoteGourmetMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteGourmetMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteGourmetMenus.NodeCount; i++)
                {
                    NoteGourmetMenuHelper.Serialize(bw, obj.NoteGourmetMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteGourmetAreas
            SubHeader? subHeader_NoteGourmetAreas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGourmetArea");
            if (subHeader_NoteGourmetAreas != null)
            {
                bw.BaseStream.Seek(subHeader_NoteGourmetAreas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteGourmetAreas.NodeCount; i++)
                {
                    NoteGourmetAreaHelper.Serialize(bw, obj.NoteGourmetAreas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteBooksMenus
            SubHeader? subHeader_NoteBooksMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteBooksMenu");
            if (subHeader_NoteBooksMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteBooksMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteBooksMenus.NodeCount; i++)
                {
                    NoteBooksMenuHelper.Serialize(bw, obj.NoteBooksMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteHelpMenus
            SubHeader? subHeader_NoteHelpMenus = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteHelpMenu");
            if (subHeader_NoteHelpMenus != null)
            {
                bw.BaseStream.Seek(subHeader_NoteHelpMenus.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteHelpMenus.NodeCount; i++)
                {
                    NoteHelpMenuHelper.Serialize(bw, obj.NoteHelpMenus[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteMonsRecordDatas
            SubHeader? subHeader_NoteMonsRecordDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteMonsRecordData");
            if (subHeader_NoteMonsRecordDatas != null)
            {
                bw.BaseStream.Seek(subHeader_NoteMonsRecordDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteMonsRecordDatas.NodeCount; i++)
                {
                    NoteMonsRecordDataHelper.Serialize(bw, obj.NoteMonsRecordDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: NoteGenesisDatas
            SubHeader? subHeader_NoteGenesisDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "NoteGenesisData");
            if (subHeader_NoteGenesisDatas != null)
            {
                bw.BaseStream.Seek(subHeader_NoteGenesisDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_NoteGenesisDatas.NodeCount; i++)
                {
                    NoteGenesisDataHelper.Serialize(bw, obj.NoteGenesisDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class NoteCategoryTabHelper
    {
        public static NoteCategoryTab DeSerialize(BinaryReader br)
        {
            var obj = new NoteCategoryTab
            {
                ID = br.ReadUInt16(),
                Short1 = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteCategoryTab tbl)
        {
            if (tbl is not NoteCategoryTab obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Int1);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteChapterMenuHelper
    {
        public static NoteChapterMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteChapterMenu
            {
                ID = br.ReadUInt16(),
                Short1 = br.ReadUInt16(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Byte3 = br.ReadByte(),
                Byte4 = br.ReadByte(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteChapterMenu tbl)
        {
            if (tbl is not NoteChapterMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Byte3);
            bw.Write(obj.Byte4);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteMainCharaHelper
    {
        public static NoteMainChara DeSerialize(BinaryReader br)
        {
            var obj = new NoteMainChara
            {
                ID = br.ReadInt64(),
                NoteText1 = br.ReadInt64(),
                NoteText2 = br.ReadInt64(),
                NoteText3 = br.ReadInt64(),
                NoteText4 = br.ReadInt64(),
                NoteText5 = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteMainChara tbl)
        {
            if (tbl is not NoteMainChara obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.NoteText1);
            bw.Write(obj.NoteText2);
            bw.Write(obj.NoteText3);
            bw.Write(obj.NoteText4);
            bw.Write(obj.NoteText5);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Float4);
        }
    }

    public static class NoteConnectMenuHelper
    {
        public static NoteConnectMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteConnectMenu
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteConnectMenu tbl)
        {
            if (tbl is not NoteConnectMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteMonsAreaMenuHelper
    {
        public static NoteMonsAreaMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteMonsAreaMenu
            {
                ID = br.ReadUInt32(),
                Short1 = br.ReadUInt16(),
                Short2 = br.ReadUInt16(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteMonsAreaMenu tbl)
        {
            if (tbl is not NoteMonsAreaMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteMonsSpotMenuHelper
    {
        public static NoteMonsSpotMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteMonsSpotMenu
            {
                Long1 = br.ReadInt64(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteMonsSpotMenu tbl)
        {
            if (tbl is not NoteMonsSpotMenu obj) return;
            bw.Write(obj.Long1);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteFishingMenuHelper
    {
        public static NoteFishingMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteFishingMenu
            {
                ID = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteFishingMenu tbl)
        {
            if (tbl is not NoteFishingMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteGourmetMenuHelper
    {
        public static NoteGourmetMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteGourmetMenu
            {
                ID = br.ReadInt64(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteGourmetMenu tbl)
        {
            if (tbl is not NoteGourmetMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteGourmetAreaHelper
    {
        public static NoteGourmetArea DeSerialize(BinaryReader br)
        {
            var obj = new NoteGourmetArea
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteGourmetArea tbl)
        {
            if (tbl is not NoteGourmetArea obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
        }
    }

    public static class NoteBooksMenuHelper
    {
        public static NoteBooksMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteBooksMenu
            {
                ID = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                NoteText = br.ReadInt64(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteBooksMenu tbl)
        {
            if (tbl is not NoteBooksMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.NoteText);
            bw.Write(obj.Long1);
        }
    }

    public static class NoteHelpMenuHelper
    {
        public static NoteHelpMenu DeSerialize(BinaryReader br)
        {
            var obj = new NoteHelpMenu
            {
                ID = br.ReadInt64(),
                NoteText1 = br.ReadInt64(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteHelpMenu tbl)
        {
            if (tbl is not NoteHelpMenu obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.NoteText1);
            bw.Write(obj.NoteText);
        }
    }

    public static class NoteMonsRecordDataHelper
    {
        public static NoteMonsRecordData DeSerialize(BinaryReader br)
        {
            var obj = new NoteMonsRecordData
            {
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                ID = br.ReadInt32(),
                Short1 = br.ReadUInt16(),
                Short2 = br.ReadUInt16(),
                Text3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteMonsRecordData tbl)
        {
            if (tbl is not NoteMonsRecordData obj) return;
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.ID);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Text3);
        }
    }

    public static class NoteGenesisDataHelper
    {
        public static NoteGenesisData DeSerialize(BinaryReader br)
        {
            var obj = new NoteGenesisData
            {
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                NoteText = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, NoteGenesisData tbl)
        {
            if (tbl is not NoteGenesisData obj) return;
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.NoteText);
        }
    }
}