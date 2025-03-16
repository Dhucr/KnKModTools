using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class QuestTableHelper
    {
        public static QuestTable DeSerialize(BinaryReader br)
        {
            QuestTable obj = TBLHelper.DeSerialize<QuestTable>(br);
            // 处理SubHeader关联数组: QuestRanks
            SubHeader? subHeader_QuestRanks = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestRank");
            if (subHeader_QuestRanks != null)
            {
                br.BaseStream.Seek(subHeader_QuestRanks.DataOffset, SeekOrigin.Begin);
                obj.QuestRanks = new QuestRank[subHeader_QuestRanks.NodeCount];
                for (var i = 0; i < subHeader_QuestRanks.NodeCount; i++)
                {
                    obj.QuestRanks[i] = QuestRankHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestRanks = Array.Empty<QuestRank>();
            }
            obj.NodeDatas.Add(subHeader_QuestRanks, obj.QuestRanks);
            // 处理SubHeader关联数组: QuestChapterRanks
            SubHeader? subHeader_QuestChapterRanks = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestChapterRank");
            if (subHeader_QuestChapterRanks != null)
            {
                br.BaseStream.Seek(subHeader_QuestChapterRanks.DataOffset, SeekOrigin.Begin);
                obj.QuestChapterRanks = new QuestChapterRank[subHeader_QuestChapterRanks.NodeCount];
                for (var i = 0; i < subHeader_QuestChapterRanks.NodeCount; i++)
                {
                    obj.QuestChapterRanks[i] = QuestChapterRankHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestChapterRanks = Array.Empty<QuestChapterRank>();
            }
            obj.NodeDatas.Add(subHeader_QuestChapterRanks, obj.QuestChapterRanks);
            // 处理SubHeader关联数组: QuestTitles
            SubHeader? subHeader_QuestTitles = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestTitle");
            if (subHeader_QuestTitles != null)
            {
                br.BaseStream.Seek(subHeader_QuestTitles.DataOffset, SeekOrigin.Begin);
                obj.QuestTitles = new QuestTitle[subHeader_QuestTitles.NodeCount];
                for (var i = 0; i < subHeader_QuestTitles.NodeCount; i++)
                {
                    obj.QuestTitles[i] = QuestTitleHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestTitles = Array.Empty<QuestTitle>();
            }
            obj.NodeDatas.Add(subHeader_QuestTitles, obj.QuestTitles);
            // 处理SubHeader关联数组: QuestTexts
            SubHeader? subHeader_QuestTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestText");
            if (subHeader_QuestTexts != null)
            {
                br.BaseStream.Seek(subHeader_QuestTexts.DataOffset, SeekOrigin.Begin);
                obj.QuestTexts = new QuestText[subHeader_QuestTexts.NodeCount];
                for (var i = 0; i < subHeader_QuestTexts.NodeCount; i++)
                {
                    obj.QuestTexts[i] = QuestTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestTexts = Array.Empty<QuestText>();
            }
            obj.NodeDatas.Add(subHeader_QuestTexts, obj.QuestTexts);
            // 处理SubHeader关联数组: QuestSubTexts
            SubHeader? subHeader_QuestSubTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestSubText");
            if (subHeader_QuestSubTexts != null)
            {
                br.BaseStream.Seek(subHeader_QuestSubTexts.DataOffset, SeekOrigin.Begin);
                obj.QuestSubTexts = new QuestSubText[subHeader_QuestSubTexts.NodeCount];
                for (var i = 0; i < subHeader_QuestSubTexts.NodeCount; i++)
                {
                    obj.QuestSubTexts[i] = QuestSubTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestSubTexts = Array.Empty<QuestSubText>();
            }
            obj.NodeDatas.Add(subHeader_QuestSubTexts, obj.QuestSubTexts);
            // 处理SubHeader关联数组: QuestReportMessages
            SubHeader? subHeader_QuestReportMessages = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestReportMessage");
            if (subHeader_QuestReportMessages != null)
            {
                br.BaseStream.Seek(subHeader_QuestReportMessages.DataOffset, SeekOrigin.Begin);
                obj.QuestReportMessages = new QuestReportMessage[subHeader_QuestReportMessages.NodeCount];
                for (var i = 0; i < subHeader_QuestReportMessages.NodeCount; i++)
                {
                    obj.QuestReportMessages[i] = QuestReportMessageHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestReportMessages = Array.Empty<QuestReportMessage>();
            }
            obj.NodeDatas.Add(subHeader_QuestReportMessages, obj.QuestReportMessages);
            // 处理SubHeader关联数组: QuestCheckMessages
            SubHeader? subHeader_QuestCheckMessages = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestCheckMessage");
            if (subHeader_QuestCheckMessages != null)
            {
                br.BaseStream.Seek(subHeader_QuestCheckMessages.DataOffset, SeekOrigin.Begin);
                obj.QuestCheckMessages = new QuestCheckMessage[subHeader_QuestCheckMessages.NodeCount];
                for (var i = 0; i < subHeader_QuestCheckMessages.NodeCount; i++)
                {
                    obj.QuestCheckMessages[i] = QuestCheckMessageHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestCheckMessages = Array.Empty<QuestCheckMessage>();
            }
            obj.NodeDatas.Add(subHeader_QuestCheckMessages, obj.QuestCheckMessages);
            // 处理SubHeader关联数组: RecruitmentMembers
            SubHeader? subHeader_RecruitmentMembers = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "RecruitmentMember");
            if (subHeader_RecruitmentMembers != null)
            {
                br.BaseStream.Seek(subHeader_RecruitmentMembers.DataOffset, SeekOrigin.Begin);
                obj.RecruitmentMembers = new RecruitmentMember[subHeader_RecruitmentMembers.NodeCount];
                for (var i = 0; i < subHeader_RecruitmentMembers.NodeCount; i++)
                {
                    obj.RecruitmentMembers[i] = RecruitmentMemberHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.RecruitmentMembers = Array.Empty<RecruitmentMember>();
            }
            obj.NodeDatas.Add(subHeader_RecruitmentMembers, obj.RecruitmentMembers);
            // 处理SubHeader关联数组: QuestReportBackTextures
            SubHeader? subHeader_QuestReportBackTextures = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestReportBackTexture");
            if (subHeader_QuestReportBackTextures != null)
            {
                br.BaseStream.Seek(subHeader_QuestReportBackTextures.DataOffset, SeekOrigin.Begin);
                obj.QuestReportBackTextures = new QuestReportBackTexture[subHeader_QuestReportBackTextures.NodeCount];
                for (var i = 0; i < subHeader_QuestReportBackTextures.NodeCount; i++)
                {
                    obj.QuestReportBackTextures[i] = QuestReportBackTextureHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.QuestReportBackTextures = Array.Empty<QuestReportBackTexture>();
            }
            obj.NodeDatas.Add(subHeader_QuestReportBackTextures, obj.QuestReportBackTextures);
            // 处理SubHeader关联数组: StampCharaLists
            SubHeader? subHeader_StampCharaLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "StampCharaList");
            if (subHeader_StampCharaLists != null)
            {
                br.BaseStream.Seek(subHeader_StampCharaLists.DataOffset, SeekOrigin.Begin);
                obj.StampCharaLists = new StampCharaList[subHeader_StampCharaLists.NodeCount];
                for (var i = 0; i < subHeader_StampCharaLists.NodeCount; i++)
                {
                    obj.StampCharaLists[i] = StampCharaListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.StampCharaLists = Array.Empty<StampCharaList>();
            }
            obj.NodeDatas.Add(subHeader_StampCharaLists, obj.StampCharaLists);

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
            QuestTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not QuestTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: QuestRanks
            SubHeader? subHeader_QuestRanks = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestRank");
            if (subHeader_QuestRanks != null)
            {
                bw.BaseStream.Seek(subHeader_QuestRanks.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestRanks.NodeCount; i++)
                {
                    QuestRankHelper.Serialize(bw, obj.QuestRanks[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestChapterRanks
            SubHeader? subHeader_QuestChapterRanks = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestChapterRank");
            if (subHeader_QuestChapterRanks != null)
            {
                bw.BaseStream.Seek(subHeader_QuestChapterRanks.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestChapterRanks.NodeCount; i++)
                {
                    QuestChapterRankHelper.Serialize(bw, obj.QuestChapterRanks[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestTitles
            SubHeader? subHeader_QuestTitles = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestTitle");
            if (subHeader_QuestTitles != null)
            {
                bw.BaseStream.Seek(subHeader_QuestTitles.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestTitles.NodeCount; i++)
                {
                    QuestTitleHelper.Serialize(bw, obj.QuestTitles[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestTexts
            SubHeader? subHeader_QuestTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestText");
            if (subHeader_QuestTexts != null)
            {
                bw.BaseStream.Seek(subHeader_QuestTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestTexts.NodeCount; i++)
                {
                    QuestTextHelper.Serialize(bw, obj.QuestTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestSubTexts
            SubHeader? subHeader_QuestSubTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestSubText");
            if (subHeader_QuestSubTexts != null)
            {
                bw.BaseStream.Seek(subHeader_QuestSubTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestSubTexts.NodeCount; i++)
                {
                    QuestSubTextHelper.Serialize(bw, obj.QuestSubTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestReportMessages
            SubHeader? subHeader_QuestReportMessages = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestReportMessage");
            if (subHeader_QuestReportMessages != null)
            {
                bw.BaseStream.Seek(subHeader_QuestReportMessages.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestReportMessages.NodeCount; i++)
                {
                    QuestReportMessageHelper.Serialize(bw, obj.QuestReportMessages[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestCheckMessages
            SubHeader? subHeader_QuestCheckMessages = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestCheckMessage");
            if (subHeader_QuestCheckMessages != null)
            {
                bw.BaseStream.Seek(subHeader_QuestCheckMessages.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestCheckMessages.NodeCount; i++)
                {
                    QuestCheckMessageHelper.Serialize(bw, obj.QuestCheckMessages[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: RecruitmentMembers
            SubHeader? subHeader_RecruitmentMembers = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "RecruitmentMember");
            if (subHeader_RecruitmentMembers != null)
            {
                bw.BaseStream.Seek(subHeader_RecruitmentMembers.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_RecruitmentMembers.NodeCount; i++)
                {
                    RecruitmentMemberHelper.Serialize(bw, obj.RecruitmentMembers[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: QuestReportBackTextures
            SubHeader? subHeader_QuestReportBackTextures = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "QuestReportBackTexture");
            if (subHeader_QuestReportBackTextures != null)
            {
                bw.BaseStream.Seek(subHeader_QuestReportBackTextures.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_QuestReportBackTextures.NodeCount; i++)
                {
                    QuestReportBackTextureHelper.Serialize(bw, obj.QuestReportBackTextures[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: StampCharaLists
            SubHeader? subHeader_StampCharaLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "StampCharaList");
            if (subHeader_StampCharaLists != null)
            {
                bw.BaseStream.Seek(subHeader_StampCharaLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_StampCharaLists.NodeCount; i++)
                {
                    StampCharaListHelper.Serialize(bw, obj.StampCharaLists[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class QuestRankHelper
    {
        public static QuestRank DeSerialize(BinaryReader br)
        {
            var obj = new QuestRank
            {
                Id = br.ReadUInt64(),
                RankName = br.ReadInt64(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestRank tbl)
        {
            if (tbl is not QuestRank obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.RankName);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
        }
    }

    public static class QuestChapterRankHelper
    {
        public static QuestChapterRank DeSerialize(BinaryReader br)
        {
            var obj = new QuestChapterRank
            {
                Id = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestChapterRank tbl)
        {
            if (tbl is not QuestChapterRank obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
        }
    }

    public static class QuestTitleHelper
    {
        public static QuestTitle DeSerialize(BinaryReader br)
        {
            var obj = new QuestTitle
            {
                Id = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                QuestDescription = br.ReadInt64(),
                QuestGiver = br.ReadInt64(),
                Short1 = br.ReadInt16(),
                Int2 = br.ReadInt32(),
                Empty2 = br.ReadInt16(),
                FileName = br.ReadInt64(),
                Text = br.ReadInt64(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Short2 = br.ReadUInt16(),
                Short3 = br.ReadUInt16(),
                Short4 = br.ReadUInt16(),
                Empty3 = br.ReadUInt16(),
                Int5 = br.ReadInt32(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Short5 = br.ReadUInt16(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestTitle tbl)
        {
            if (tbl is not QuestTitle obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.QuestDescription);
            bw.Write(obj.QuestGiver);
            bw.Write(obj.Short1);
            bw.Write(obj.Int2);
            bw.Write(obj.Empty2);
            bw.Write(obj.FileName);
            bw.Write(obj.Text);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Short2);
            bw.Write(obj.Short3);
            bw.Write(obj.Short4);
            bw.Write(obj.Empty3);
            bw.Write(obj.Int5);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Short5);
            bw.Write(obj.Long1);
        }
    }

    public static class QuestTextHelper
    {
        public static QuestText DeSerialize(BinaryReader br)
        {
            var obj = new QuestText
            {
                QuestId = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                QuestDescription = br.ReadInt64(),
                ArrayFlag1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                ArrayFlag2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Empty5 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestText tbl)
        {
            if (tbl is not QuestText obj) return;
            bw.Write(obj.QuestId);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.QuestDescription);
            bw.Write(obj.ArrayFlag1);
            bw.Write(obj.Count1);
            bw.Write(obj.ArrayFlag2);
            bw.Write(obj.Count2);
            bw.Write(obj.Empty5);
        }
    }

    public static class QuestSubTextHelper
    {
        public static QuestSubText DeSerialize(BinaryReader br)
        {
            var obj = new QuestSubText
            {
                Short1 = br.ReadInt16(),
                Short2 = br.ReadInt16(),
                Short3 = br.ReadInt16(),
                Short4 = br.ReadInt16(),
                Short5 = br.ReadInt16(),
                Short6 = br.ReadInt16(),
                Short7 = br.ReadInt16(),
                Short8 = br.ReadInt16(),
                Short9 = br.ReadInt16(),
                Short10 = br.ReadInt16(),
                Short11 = br.ReadInt16(),
                Short12 = br.ReadInt16(),
                Short13 = br.ReadInt16(),
                Short14 = br.ReadInt16(),
                Short15 = br.ReadInt16(),
                Short16 = br.ReadInt16(),
                Short17 = br.ReadInt16(),
                Short18 = br.ReadInt16(),
                Short19 = br.ReadInt16(),
                Short20 = br.ReadInt16(),
                Short21 = br.ReadInt16(),
                Short22 = br.ReadInt16(),
                Short23 = br.ReadInt16(),
                Short24 = br.ReadInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestSubText tbl)
        {
            if (tbl is not QuestSubText obj) return;
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Short3);
            bw.Write(obj.Short4);
            bw.Write(obj.Short5);
            bw.Write(obj.Short6);
            bw.Write(obj.Short7);
            bw.Write(obj.Short8);
            bw.Write(obj.Short9);
            bw.Write(obj.Short10);
            bw.Write(obj.Short11);
            bw.Write(obj.Short12);
            bw.Write(obj.Short13);
            bw.Write(obj.Short14);
            bw.Write(obj.Short15);
            bw.Write(obj.Short16);
            bw.Write(obj.Short17);
            bw.Write(obj.Short18);
            bw.Write(obj.Short19);
            bw.Write(obj.Short20);
            bw.Write(obj.Short21);
            bw.Write(obj.Short22);
            bw.Write(obj.Short23);
            bw.Write(obj.Short24);
        }
    }

    public static class QuestReportMessageHelper
    {
        public static QuestReportMessage DeSerialize(BinaryReader br)
        {
            var obj = new QuestReportMessage
            {
                Id = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestReportMessage tbl)
        {
            if (tbl is not QuestReportMessage obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Text);
        }
    }

    public static class QuestCheckMessageHelper
    {
        public static QuestCheckMessage DeSerialize(BinaryReader br)
        {
            var obj = new QuestCheckMessage
            {
                Id = br.ReadInt64(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestCheckMessage tbl)
        {
            if (tbl is not QuestCheckMessage obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Text);
        }
    }

    public static class RecruitmentMemberHelper
    {
        public static RecruitmentMember DeSerialize(BinaryReader br)
        {
            var obj = new RecruitmentMember
            {
                Id = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Recruitment = br.ReadInt64(),
                CharName = br.ReadInt64(),
                Age = br.ReadInt64(),
                Home = br.ReadInt64(),
                Occupation = br.ReadInt64(),
                Int2 = br.ReadUInt32(),
                Int3 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, RecruitmentMember tbl)
        {
            if (tbl is not RecruitmentMember obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.Recruitment);
            bw.Write(obj.CharName);
            bw.Write(obj.Age);
            bw.Write(obj.Home);
            bw.Write(obj.Occupation);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
        }
    }

    public static class QuestReportBackTextureHelper
    {
        public static QuestReportBackTexture DeSerialize(BinaryReader br)
        {
            var obj = new QuestReportBackTexture
            {
                Id = br.ReadInt64(),
                Texture1 = br.ReadInt64(),
                Texture2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, QuestReportBackTexture tbl)
        {
            if (tbl is not QuestReportBackTexture obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Texture1);
            bw.Write(obj.Texture2);
        }
    }

    public static class StampCharaListHelper
    {
        public static StampCharaList DeSerialize(BinaryReader br)
        {
            var obj = new StampCharaList
            {
                Id = br.ReadUInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Int1 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, StampCharaList tbl)
        {
            if (tbl is not StampCharaList obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Int1);
        }
    }
}