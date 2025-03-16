using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class BookTableHelper
    {
        public static BookTable DeSerialize(BinaryReader br)
        {
            BookTable obj = TBLHelper.DeSerialize<BookTable>(br);
            // 处理SubHeader关联数组: BooksTitles
            SubHeader? subHeader_BooksTitles = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksTitle");
            if (subHeader_BooksTitles != null)
            {
                br.BaseStream.Seek(subHeader_BooksTitles.DataOffset, SeekOrigin.Begin);
                obj.BooksTitles = new BooksTitle[subHeader_BooksTitles.NodeCount];
                for (var i = 0; i < subHeader_BooksTitles.NodeCount; i++)
                {
                    obj.BooksTitles[i] = BooksTitleHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BooksTitles = Array.Empty<BooksTitle>();
            }
            obj.NodeDatas.Add(subHeader_BooksTitles, obj.BooksTitles);
            TBL.SubHeaderMap.Add("BooksTitle", obj.BooksTitles);
            // 处理SubHeader关联数组: BooksTexts
            SubHeader? subHeader_BooksTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksText");
            if (subHeader_BooksTexts != null)
            {
                br.BaseStream.Seek(subHeader_BooksTexts.DataOffset, SeekOrigin.Begin);
                obj.BooksTexts = new BooksText[subHeader_BooksTexts.NodeCount];
                for (var i = 0; i < subHeader_BooksTexts.NodeCount; i++)
                {
                    obj.BooksTexts[i] = BooksTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BooksTexts = Array.Empty<BooksText>();
            }
            obj.NodeDatas.Add(subHeader_BooksTexts, obj.BooksTexts);
            // 处理SubHeader关联数组: BooksTitleReplaces
            SubHeader? subHeader_BooksTitleReplaces = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksTitleReplace");
            if (subHeader_BooksTitleReplaces != null)
            {
                br.BaseStream.Seek(subHeader_BooksTitleReplaces.DataOffset, SeekOrigin.Begin);
                obj.BooksTitleReplaces = new BooksTitleReplace[subHeader_BooksTitleReplaces.NodeCount];
                for (var i = 0; i < subHeader_BooksTitleReplaces.NodeCount; i++)
                {
                    obj.BooksTitleReplaces[i] = BooksTitleReplaceHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BooksTitleReplaces = Array.Empty<BooksTitleReplace>();
            }
            obj.NodeDatas.Add(subHeader_BooksTitleReplaces, obj.BooksTitleReplaces);

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
            BookTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not BookTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: BooksTitles
            SubHeader? subHeader_BooksTitles = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksTitle");
            if (subHeader_BooksTitles != null)
            {
                bw.BaseStream.Seek(subHeader_BooksTitles.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BooksTitles.NodeCount; i++)
                {
                    BooksTitleHelper.Serialize(bw, obj.BooksTitles[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BooksTexts
            SubHeader? subHeader_BooksTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksText");
            if (subHeader_BooksTexts != null)
            {
                bw.BaseStream.Seek(subHeader_BooksTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BooksTexts.NodeCount; i++)
                {
                    BooksTextHelper.Serialize(bw, obj.BooksTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BooksTitleReplaces
            SubHeader? subHeader_BooksTitleReplaces = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BooksTitleReplace");
            if (subHeader_BooksTitleReplaces != null)
            {
                bw.BaseStream.Seek(subHeader_BooksTitleReplaces.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BooksTitleReplaces.NodeCount; i++)
                {
                    BooksTitleReplaceHelper.Serialize(bw, obj.BooksTitleReplaces[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class BooksTitleHelper
    {
        public static BooksTitle DeSerialize(BinaryReader br)
        {
            var obj = new BooksTitle
            {
                Id = br.ReadInt16(),
                Int1 = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                Title = br.ReadInt64(),
                FileName = br.ReadInt64(),
                Long2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BooksTitle tbl)
        {
            if (tbl is not BooksTitle obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.Empty1);
            bw.Write(obj.Title);
            bw.Write(obj.FileName);
            bw.Write(obj.Long2);
        }
    }

    public static class BooksTextHelper
    {
        public static BooksText DeSerialize(BinaryReader br)
        {
            var obj = new BooksText
            {
                BookId = br.ReadInt16(),
                PageId = br.ReadInt32(),
                Empty1 = br.ReadInt16(),
                BookText = br.ReadInt64(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BooksText tbl)
        {
            if (tbl is not BooksText obj) return;
            bw.Write(obj.BookId);
            bw.Write(obj.PageId);
            bw.Write(obj.Empty1);
            bw.Write(obj.BookText);
            bw.Write(obj.Long1);
        }
    }

    public static class BooksTitleReplaceHelper
    {
        public static BooksTitleReplace DeSerialize(BinaryReader br)
        {
            var obj = new BooksTitleReplace
            {
                Id = br.ReadUInt16(),
                Int1 = br.ReadInt32(),
                Short1 = br.ReadInt16(),
                BookName = br.ReadInt64(),
                FileName = br.ReadInt64(),
                Short2 = br.ReadUInt16(),
                Short3 = br.ReadUInt16(),
                Empty = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BooksTitleReplace tbl)
        {
            if (tbl is not BooksTitleReplace obj) return;
            bw.Write(obj.Id);
            bw.Write(obj.Int1);
            bw.Write(obj.Short1);
            bw.Write(obj.BookName);
            bw.Write(obj.FileName);
            bw.Write(obj.Short2);
            bw.Write(obj.Short3);
            bw.Write(obj.Empty);
        }
    }
}