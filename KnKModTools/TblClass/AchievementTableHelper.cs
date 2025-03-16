using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class AchievementTableHelper
    {
        public static AchievementTable DeSerialize(BinaryReader br)
        {
            AchievementTable obj = TBLHelper.DeSerialize<AchievementTable>(br);
            // 处理SubHeader关联数组: AchievementTableDatas
            SubHeader? subHeader_AchievementTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AchievementTableData");
            if (subHeader_AchievementTableDatas != null)
            {
                br.BaseStream.Seek(subHeader_AchievementTableDatas.DataOffset, SeekOrigin.Begin);
                obj.AchievementTableDatas = new AchievementTableData[subHeader_AchievementTableDatas.NodeCount];
                for (var i = 0; i < subHeader_AchievementTableDatas.NodeCount; i++)
                {
                    obj.AchievementTableDatas[i] = AchievementTableDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.AchievementTableDatas = Array.Empty<AchievementTableData>();
            }
            obj.NodeDatas.Add(subHeader_AchievementTableDatas, obj.AchievementTableDatas);

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
            AchievementTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not AchievementTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: AchievementTableDatas
            SubHeader? subHeader_AchievementTableDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "AchievementTableData");
            if (subHeader_AchievementTableDatas != null)
            {
                bw.BaseStream.Seek(subHeader_AchievementTableDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_AchievementTableDatas.NodeCount; i++)
                {
                    AchievementTableDataHelper.Serialize(bw, obj.AchievementTableDatas[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class AchievementTableDataHelper
    {
        public static AchievementTableData DeSerialize(BinaryReader br)
        {
            var obj = new AchievementTableData
            {
                AchievementCategory = br.ReadUInt32(),
                AchievementId = br.ReadUInt32(),
                AchievementObjective1 = br.ReadUInt32(),
                AchievementObjectiveParam1 = br.ReadUInt32(),
                Long1 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Arr1Count = br.ReadInt32(),
                Short1 = br.ReadInt16(),
                Short2 = br.ReadInt16(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Long2 = br.ReadInt64(),
                Empty = br.ReadInt64(),
                Flag = br.ReadInt64(),
                AchievementName = br.ReadInt64(),
                AchievementDescription = br.ReadInt64(),
                Long3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, AchievementTableData tbl)
        {
            if (tbl is not AchievementTableData obj) return;
            bw.Write(obj.AchievementCategory);
            bw.Write(obj.AchievementId);
            bw.Write(obj.AchievementObjective1);
            bw.Write(obj.AchievementObjectiveParam1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Arr1Count);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Long2);
            bw.Write(obj.Empty);
            bw.Write(obj.Flag);
            bw.Write(obj.AchievementName);
            bw.Write(obj.AchievementDescription);
            bw.Write(obj.Long3);
        }
    }
}