using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class CostumeTableHelper
    {
        public static CostumeTable DeSerialize(BinaryReader br)
        {
            CostumeTable obj = TBLHelper.DeSerialize<CostumeTable>(br);
            // 处理SubHeader关联数组: CostumeParams
            SubHeader? subHeader_CostumeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CostumeParam");
            if (subHeader_CostumeParams != null)
            {
                br.BaseStream.Seek(subHeader_CostumeParams.DataOffset, SeekOrigin.Begin);
                obj.CostumeParams = new CostumeParam[subHeader_CostumeParams.NodeCount];
                for (var i = 0; i < subHeader_CostumeParams.NodeCount; i++)
                {
                    obj.CostumeParams[i] = CostumeParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.CostumeParams = Array.Empty<CostumeParam>();
            }
            obj.NodeDatas.Add(subHeader_CostumeParams, obj.CostumeParams);
            // 处理SubHeader关联数组: CostumeAttachOffsets
            SubHeader? subHeader_CostumeAttachOffsets = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CostumeAttachOffset");
            if (subHeader_CostumeAttachOffsets != null)
            {
                br.BaseStream.Seek(subHeader_CostumeAttachOffsets.DataOffset, SeekOrigin.Begin);
                obj.CostumeAttachOffsets = new CostumeAttachOffset[subHeader_CostumeAttachOffsets.NodeCount];
                for (var i = 0; i < subHeader_CostumeAttachOffsets.NodeCount; i++)
                {
                    obj.CostumeAttachOffsets[i] = CostumeAttachOffsetHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.CostumeAttachOffsets = Array.Empty<CostumeAttachOffset>();
            }
            obj.NodeDatas.Add(subHeader_CostumeAttachOffsets, obj.CostumeAttachOffsets);

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
            CostumeTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not CostumeTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: CostumeParams
            SubHeader? subHeader_CostumeParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CostumeParam");
            if (subHeader_CostumeParams != null)
            {
                bw.BaseStream.Seek(subHeader_CostumeParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_CostumeParams.NodeCount; i++)
                {
                    CostumeParamHelper.Serialize(bw, obj.CostumeParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: CostumeAttachOffsets
            SubHeader? subHeader_CostumeAttachOffsets = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "CostumeAttachOffset");
            if (subHeader_CostumeAttachOffsets != null)
            {
                bw.BaseStream.Seek(subHeader_CostumeAttachOffsets.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_CostumeAttachOffsets.NodeCount; i++)
                {
                    CostumeAttachOffsetHelper.Serialize(bw, obj.CostumeAttachOffsets[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class CostumeParamHelper
    {
        public static CostumeParam DeSerialize(BinaryReader br)
        {
            var obj = new CostumeParam
            {
                CharacterID = br.ReadInt16(),
                Shrt1 = br.ReadInt16(),
                ItemID = br.ReadInt16(),
                Shrt3 = br.ReadInt16(),
                Text = br.ReadInt64(),
                Name = br.ReadInt64(),
                Int3 = br.ReadUInt32(),
                Int4 = br.ReadUInt32(),
                AttachName = br.ReadInt64(),
                Text4 = br.ReadInt64(),
                Text5 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, CostumeParam tbl)
        {
            if (tbl is not CostumeParam obj) return;
            bw.Write(obj.CharacterID);
            bw.Write(obj.Shrt1);
            bw.Write(obj.ItemID);
            bw.Write(obj.Shrt3);
            bw.Write(obj.Text);
            bw.Write(obj.Name);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.AttachName);
            bw.Write(obj.Text4);
            bw.Write(obj.Text5);
        }
    }

    public static class CostumeAttachOffsetHelper
    {
        public static CostumeAttachOffset DeSerialize(BinaryReader br)
        {
            var obj = new CostumeAttachOffset
            {
                Int0 = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Text = br.ReadInt64(),
                Float0 = br.ReadSingle(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Float5 = br.ReadSingle(),
                Float6 = br.ReadSingle(),
                Float7 = br.ReadSingle(),
                Float8 = br.ReadSingle(),
                Float9 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, CostumeAttachOffset tbl)
        {
            if (tbl is not CostumeAttachOffset obj) return;
            bw.Write(obj.Int0);
            bw.Write(obj.Int1);
            bw.Write(obj.Text);
            bw.Write(obj.Float0);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Float4);
            bw.Write(obj.Float5);
            bw.Write(obj.Float6);
            bw.Write(obj.Float7);
            bw.Write(obj.Float8);
            bw.Write(obj.Float9);
        }
    }
}