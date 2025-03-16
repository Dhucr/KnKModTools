using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class StatusTableHelper
    {
        public static StatusTable DeSerialize(BinaryReader br)
        {
            StatusTable obj = TBLHelper.DeSerialize<StatusTable>(br);
            // 处理SubHeader关联数组: StatusParams
            SubHeader? subHeader_StatusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "StatusParam");
            if (subHeader_StatusParams != null)
            {
                br.BaseStream.Seek(subHeader_StatusParams.DataOffset, SeekOrigin.Begin);
                obj.StatusParams = new StatusParam[subHeader_StatusParams.NodeCount];
                for (var i = 0; i < subHeader_StatusParams.NodeCount; i++)
                {
                    obj.StatusParams[i] = StatusParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.StatusParams = Array.Empty<StatusParam>();
            }
            obj.NodeDatas.Add(subHeader_StatusParams, obj.StatusParams);

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
            StatusTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not StatusTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: StatusParams
            SubHeader? subHeader_StatusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "StatusParam");
            if (subHeader_StatusParams != null)
            {
                bw.BaseStream.Seek(subHeader_StatusParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_StatusParams.NodeCount; i++)
                {
                    StatusParamHelper.Serialize(bw, obj.StatusParams[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class StatusParamHelper
    {
        public static StatusParam DeSerialize(BinaryReader br)
        {
            var obj = new StatusParam
            {
                AiFile = br.ReadInt64(),
                Flag = br.ReadInt64(),
                Data = br.ReadUInt64(),
                File1 = br.ReadInt64(),
                File2 = br.ReadInt64(),
                File3 = br.ReadInt64(),
                File4 = br.ReadInt64(),
                Unknown = br.ReadInt64(),
                File5 = br.ReadInt64(),
                Int1 = br.ReadUInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Float5 = br.ReadSingle(),
                Float6 = br.ReadSingle(),
                Float7 = br.ReadSingle(),
                Float8 = br.ReadSingle(),
                Float9 = br.ReadSingle(),
                Float10 = br.ReadSingle(),
                Float11 = br.ReadSingle(),
                Float12 = br.ReadSingle(),
                Int2 = br.ReadInt32(),
                Float13 = br.ReadSingle(),
                Float14 = br.ReadSingle(),
                Float15 = br.ReadSingle(),
                Float16 = br.ReadSingle(),
                Float17 = br.ReadSingle(),
                Float18 = br.ReadSingle(),
                Float19 = br.ReadSingle(),
                Float20 = br.ReadSingle(),
                Float21 = br.ReadSingle(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Int5 = br.ReadInt32(),
                Float23 = br.ReadSingle(),
                Float24 = br.ReadSingle(),
                Int6 = br.ReadInt32(),
                Int7 = br.ReadInt32(),
                Int8 = br.ReadInt32(),
                Float25 = br.ReadSingle(),
                Int9 = br.ReadInt32(),
                Int10 = br.ReadInt32(),
                Float26 = br.ReadSingle(),
                Int11 = br.ReadInt32(),
                Float27 = br.ReadSingle(),
                Int12 = br.ReadInt32(),
                Int13 = br.ReadInt32(),
                Int14 = br.ReadInt32(),
                Int15 = br.ReadInt32(),
                Float28 = br.ReadSingle(),
                Level = br.ReadInt32(),
                Float29 = br.ReadSingle(),
                Int16 = br.ReadInt32(),
                Float30 = br.ReadSingle(),
                Int17 = br.ReadInt32(),
                Float31 = br.ReadSingle(),
                Int18 = br.ReadInt32(),
                Float32 = br.ReadSingle(),
                Int19 = br.ReadInt32(),
                Float33 = br.ReadSingle(),
                Int20 = br.ReadInt32(),
                Float34 = br.ReadSingle(),
                Int21 = br.ReadInt32(),
                Int22 = br.ReadInt32(),
                Int23 = br.ReadInt32(),
                Int24 = br.ReadInt32(),
                Int25 = br.ReadInt32(),
                Int26 = br.ReadInt32(),
                Int27 = br.ReadInt32(),
                Int28 = br.ReadInt32(),
                CorrosionVulnerability = br.ReadByte(),
                FreezeVulnerability = br.ReadByte(),
                BurnVulnerability = br.ReadByte(),
                SealVulnerability = br.ReadByte(),
                MuteVulnerability = br.ReadByte(),
                BlindVulnerability = br.ReadByte(),
                FearVulnerability = br.ReadByte(),
                DeathblowVulnerability = br.ReadByte(),
                DazzleVulnerability = br.ReadByte(),
                StatVulnerability = br.ReadByte(),
                UnknownVulnerability = br.ReadByte(),
                DelayVulnerability = br.ReadByte(),
                EarthVulnerability = br.ReadByte(),
                WaterVulnerability = br.ReadByte(),
                FireVulnerability = br.ReadByte(),
                WindVulnerability = br.ReadByte(),
                TimeVulnerability = br.ReadByte(),
                SpaceVulnerability = br.ReadByte(),
                MirageVulnerability = br.ReadByte(),
                Byte1 = br.ReadByte(),
                Short1 = br.ReadUInt16(),
                Short2 = br.ReadUInt16(),
                Short3 = br.ReadUInt16(),
                Short4 = br.ReadUInt16(),
                Short5 = br.ReadUInt16(),
                Short6 = br.ReadUInt16(),
                Short7 = br.ReadUInt16(),
                Short8 = br.ReadUInt16(),
                Short9 = br.ReadUInt16(),
                Short10 = br.ReadUInt16(),
                Short11 = br.ReadUInt16(),
                Short12 = br.ReadUInt16(),
                Short13 = br.ReadUInt16(),
                Short14 = br.ReadUInt16(),
                Short15 = br.ReadUInt16(),
                Short16 = br.ReadUInt16(),
                Short17 = br.ReadUInt16(),
                Short18 = br.ReadUInt16(),
                Short19 = br.ReadUInt16(),
                Short20 = br.ReadUInt16(),
                Short21 = br.ReadUInt16(),
                Short22 = br.ReadUInt16(),
                Short23 = br.ReadUInt16(),
                Short24 = br.ReadUInt16(),
                Int29 = br.ReadInt32(),
                Byte2 = br.ReadByte(),
                Byte3 = br.ReadByte(),
                Short25 = br.ReadUInt16(),
                Int30 = br.ReadInt32(),
                Byte4 = br.ReadByte(),
                Byte5 = br.ReadByte(),
                Byte6 = br.ReadByte(),
                Byte7 = br.ReadByte(),
                Int31 = br.ReadInt32(),
                Name = br.ReadInt64(),
                Description = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, StatusParam obj)
        {
            bw.Write(obj.AiFile);
            bw.Write(obj.Flag);
            bw.Write(obj.Data);
            bw.Write(obj.File1);
            bw.Write(obj.File2);
            bw.Write(obj.File3);
            bw.Write(obj.File4);
            bw.Write(obj.Unknown);
            bw.Write(obj.File5);
            bw.Write(obj.Int1);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
            bw.Write(obj.Float4);
            bw.Write(obj.Float5);
            bw.Write(obj.Float6);
            bw.Write(obj.Float7);
            bw.Write(obj.Float8);
            bw.Write(obj.Float9);
            bw.Write(obj.Float10);
            bw.Write(obj.Float11);
            bw.Write(obj.Float12);
            bw.Write(obj.Int2);
            bw.Write(obj.Float13);
            bw.Write(obj.Float14);
            bw.Write(obj.Float15);
            bw.Write(obj.Float16);
            bw.Write(obj.Float17);
            bw.Write(obj.Float18);
            bw.Write(obj.Float19);
            bw.Write(obj.Float20);
            bw.Write(obj.Float21);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Int5);
            bw.Write(obj.Float23);
            bw.Write(obj.Float24);
            bw.Write(obj.Int6);
            bw.Write(obj.Int7);
            bw.Write(obj.Int8);
            bw.Write(obj.Float25);
            bw.Write(obj.Int9);
            bw.Write(obj.Int10);
            bw.Write(obj.Float26);
            bw.Write(obj.Int11);
            bw.Write(obj.Float27);
            bw.Write(obj.Int12);
            bw.Write(obj.Int13);
            bw.Write(obj.Int14);
            bw.Write(obj.Int15);
            bw.Write(obj.Float28);
            bw.Write(obj.Level);
            bw.Write(obj.Float29);
            bw.Write(obj.Int16);
            bw.Write(obj.Float30);
            bw.Write(obj.Int17);
            bw.Write(obj.Float31);
            bw.Write(obj.Int18);
            bw.Write(obj.Float32);
            bw.Write(obj.Int19);
            bw.Write(obj.Float33);
            bw.Write(obj.Int20);
            bw.Write(obj.Float34);
            bw.Write(obj.Int21);
            bw.Write(obj.Int22);
            bw.Write(obj.Int23);
            bw.Write(obj.Int24);
            bw.Write(obj.Int25);
            bw.Write(obj.Int26);
            bw.Write(obj.Int27);
            bw.Write(obj.Int28);
            bw.Write(obj.CorrosionVulnerability);
            bw.Write(obj.FreezeVulnerability);
            bw.Write(obj.BurnVulnerability);
            bw.Write(obj.SealVulnerability);
            bw.Write(obj.MuteVulnerability);
            bw.Write(obj.BlindVulnerability);
            bw.Write(obj.FearVulnerability);
            bw.Write(obj.DeathblowVulnerability);
            bw.Write(obj.DazzleVulnerability);
            bw.Write(obj.StatVulnerability);
            bw.Write(obj.UnknownVulnerability);
            bw.Write(obj.DelayVulnerability);
            bw.Write(obj.EarthVulnerability);
            bw.Write(obj.WaterVulnerability);
            bw.Write(obj.FireVulnerability);
            bw.Write(obj.WindVulnerability);
            bw.Write(obj.TimeVulnerability);
            bw.Write(obj.SpaceVulnerability);
            bw.Write(obj.MirageVulnerability);
            bw.Write(obj.Byte1);
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
            bw.Write(obj.Int29);
            bw.Write(obj.Byte2);
            bw.Write(obj.Byte3);
            bw.Write(obj.Short25);
            bw.Write(obj.Int30);
            bw.Write(obj.Byte4);
            bw.Write(obj.Byte5);
            bw.Write(obj.Byte6);
            bw.Write(obj.Byte7);
            bw.Write(obj.Int31);
            bw.Write(obj.Name);
            bw.Write(obj.Description);
        }
    }
}