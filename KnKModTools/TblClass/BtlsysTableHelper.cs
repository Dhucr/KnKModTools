using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class BtlsysTableHelper
    {
        public static BtlsysTable DeSerialize(BinaryReader br)
        {
            BtlsysTable obj = TBLHelper.DeSerialize<BtlsysTable>(br);
            // 处理SubHeader关联数组: ATBonusParams
            SubHeader? subHeader_ATBonusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ATBonusParam");
            if (subHeader_ATBonusParams != null)
            {
                br.BaseStream.Seek(subHeader_ATBonusParams.DataOffset, SeekOrigin.Begin);
                obj.ATBonusParams = new ATBonusParam[subHeader_ATBonusParams.NodeCount];
                for (var i = 0; i < subHeader_ATBonusParams.NodeCount; i++)
                {
                    obj.ATBonusParams[i] = ATBonusParamHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ATBonusParams = Array.Empty<ATBonusParam>();
            }
            obj.NodeDatas.Add(subHeader_ATBonusParams, obj.ATBonusParams);
            // 处理SubHeader关联数组: ATBonusSets
            SubHeader? subHeader_ATBonusSets = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ATBonusSet");
            if (subHeader_ATBonusSets != null)
            {
                br.BaseStream.Seek(subHeader_ATBonusSets.DataOffset, SeekOrigin.Begin);
                obj.ATBonusSets = new ATBonusSet[subHeader_ATBonusSets.NodeCount];
                for (var i = 0; i < subHeader_ATBonusSets.NodeCount; i++)
                {
                    obj.ATBonusSets[i] = ATBonusSetHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.ATBonusSets = Array.Empty<ATBonusSet>();
            }
            obj.NodeDatas.Add(subHeader_ATBonusSets, obj.ATBonusSets);
            // 处理SubHeader关联数组: BattleLevelFields
            SubHeader? subHeader_BattleLevelFields = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleLevelField");
            if (subHeader_BattleLevelFields != null)
            {
                br.BaseStream.Seek(subHeader_BattleLevelFields.DataOffset, SeekOrigin.Begin);
                obj.BattleLevelFields = new BattleLevelField[subHeader_BattleLevelFields.NodeCount];
                for (var i = 0; i < subHeader_BattleLevelFields.NodeCount; i++)
                {
                    obj.BattleLevelFields[i] = BattleLevelFieldHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BattleLevelFields = Array.Empty<BattleLevelField>();
            }
            obj.NodeDatas.Add(subHeader_BattleLevelFields, obj.BattleLevelFields);
            // 处理SubHeader关联数组: BattleLevelTurns
            SubHeader? subHeader_BattleLevelTurns = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleLevelTurn");
            if (subHeader_BattleLevelTurns != null)
            {
                br.BaseStream.Seek(subHeader_BattleLevelTurns.DataOffset, SeekOrigin.Begin);
                obj.BattleLevelTurns = new BattleLevelTurn[subHeader_BattleLevelTurns.NodeCount];
                for (var i = 0; i < subHeader_BattleLevelTurns.NodeCount; i++)
                {
                    obj.BattleLevelTurns[i] = BattleLevelTurnHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BattleLevelTurns = Array.Empty<BattleLevelTurn>();
            }
            obj.NodeDatas.Add(subHeader_BattleLevelTurns, obj.BattleLevelTurns);
            // 处理SubHeader关联数组: BattleEnemyLevelStatusAdjusts
            SubHeader? subHeader_BattleEnemyLevelStatusAdjusts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleEnemyLevelStatusAdjust");
            if (subHeader_BattleEnemyLevelStatusAdjusts != null)
            {
                br.BaseStream.Seek(subHeader_BattleEnemyLevelStatusAdjusts.DataOffset, SeekOrigin.Begin);
                obj.BattleEnemyLevelStatusAdjusts = new BattleEnemyLevelStatusAdjust[subHeader_BattleEnemyLevelStatusAdjusts.NodeCount];
                for (var i = 0; i < subHeader_BattleEnemyLevelStatusAdjusts.NodeCount; i++)
                {
                    obj.BattleEnemyLevelStatusAdjusts[i] = BattleEnemyLevelStatusAdjustHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BattleEnemyLevelStatusAdjusts = Array.Empty<BattleEnemyLevelStatusAdjust>();
            }
            obj.NodeDatas.Add(subHeader_BattleEnemyLevelStatusAdjusts, obj.BattleEnemyLevelStatusAdjusts);
            // 处理SubHeader关联数组: BattleEnemyLevelAdjusts
            SubHeader? subHeader_BattleEnemyLevelAdjusts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleEnemyLevelAdjust");
            if (subHeader_BattleEnemyLevelAdjusts != null)
            {
                br.BaseStream.Seek(subHeader_BattleEnemyLevelAdjusts.DataOffset, SeekOrigin.Begin);
                obj.BattleEnemyLevelAdjusts = new BattleEnemyLevelAdjust[subHeader_BattleEnemyLevelAdjusts.NodeCount];
                for (var i = 0; i < subHeader_BattleEnemyLevelAdjusts.NodeCount; i++)
                {
                    obj.BattleEnemyLevelAdjusts[i] = BattleEnemyLevelAdjustHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.BattleEnemyLevelAdjusts = Array.Empty<BattleEnemyLevelAdjust>();
            }
            obj.NodeDatas.Add(subHeader_BattleEnemyLevelAdjusts, obj.BattleEnemyLevelAdjusts);
            // 处理SubHeader关联数组: TacticalBonuses
            SubHeader? subHeader_TacticalBonuses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TacticalBonus");
            if (subHeader_TacticalBonuses != null)
            {
                br.BaseStream.Seek(subHeader_TacticalBonuses.DataOffset, SeekOrigin.Begin);
                obj.TacticalBonuses = new TacticalBonus[subHeader_TacticalBonuses.NodeCount];
                for (var i = 0; i < subHeader_TacticalBonuses.NodeCount; i++)
                {
                    obj.TacticalBonuses[i] = TacticalBonusHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.TacticalBonuses = Array.Empty<TacticalBonus>();
            }
            obj.NodeDatas.Add(subHeader_TacticalBonuses, obj.TacticalBonuses);

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
            BtlsysTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not BtlsysTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: ATBonusParams
            SubHeader? subHeader_ATBonusParams = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ATBonusParam");
            if (subHeader_ATBonusParams != null)
            {
                bw.BaseStream.Seek(subHeader_ATBonusParams.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ATBonusParams.NodeCount; i++)
                {
                    ATBonusParamHelper.Serialize(bw, obj.ATBonusParams[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: ATBonusSets
            SubHeader? subHeader_ATBonusSets = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "ATBonusSet");
            if (subHeader_ATBonusSets != null)
            {
                bw.BaseStream.Seek(subHeader_ATBonusSets.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_ATBonusSets.NodeCount; i++)
                {
                    ATBonusSetHelper.Serialize(bw, obj.ATBonusSets[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BattleLevelFields
            SubHeader? subHeader_BattleLevelFields = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleLevelField");
            if (subHeader_BattleLevelFields != null)
            {
                bw.BaseStream.Seek(subHeader_BattleLevelFields.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BattleLevelFields.NodeCount; i++)
                {
                    BattleLevelFieldHelper.Serialize(bw, obj.BattleLevelFields[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BattleLevelTurns
            SubHeader? subHeader_BattleLevelTurns = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleLevelTurn");
            if (subHeader_BattleLevelTurns != null)
            {
                bw.BaseStream.Seek(subHeader_BattleLevelTurns.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BattleLevelTurns.NodeCount; i++)
                {
                    BattleLevelTurnHelper.Serialize(bw, obj.BattleLevelTurns[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BattleEnemyLevelStatusAdjusts
            SubHeader? subHeader_BattleEnemyLevelStatusAdjusts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleEnemyLevelStatusAdjust");
            if (subHeader_BattleEnemyLevelStatusAdjusts != null)
            {
                bw.BaseStream.Seek(subHeader_BattleEnemyLevelStatusAdjusts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BattleEnemyLevelStatusAdjusts.NodeCount; i++)
                {
                    BattleEnemyLevelStatusAdjustHelper.Serialize(bw, obj.BattleEnemyLevelStatusAdjusts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: BattleEnemyLevelAdjusts
            SubHeader? subHeader_BattleEnemyLevelAdjusts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "BattleEnemyLevelAdjust");
            if (subHeader_BattleEnemyLevelAdjusts != null)
            {
                bw.BaseStream.Seek(subHeader_BattleEnemyLevelAdjusts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_BattleEnemyLevelAdjusts.NodeCount; i++)
                {
                    BattleEnemyLevelAdjustHelper.Serialize(bw, obj.BattleEnemyLevelAdjusts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: TacticalBonuses
            SubHeader? subHeader_TacticalBonuses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "TacticalBonus");
            if (subHeader_TacticalBonuses != null)
            {
                bw.BaseStream.Seek(subHeader_TacticalBonuses.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_TacticalBonuses.NodeCount; i++)
                {
                    TacticalBonusHelper.Serialize(bw, obj.TacticalBonuses[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class ATBonusParamHelper
    {
        public static ATBonusParam DeSerialize(BinaryReader br)
        {
            var obj = new ATBonusParam
            {
                ID = br.ReadByte(),
                ReferenceID = br.ReadByte(),
                Int1 = br.ReadInt32(),
                Short1 = br.ReadUInt16(),
                Flag = br.ReadInt64(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Int5 = br.ReadInt32(),
                AtBonusName = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ATBonusParam tbl)
        {
            if (tbl is not ATBonusParam obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.ReferenceID);
            bw.Write(obj.Int1);
            bw.Write(obj.Short1);
            bw.Write(obj.Flag);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Int5);
            bw.Write(obj.AtBonusName);
        }
    }

    public static class ATBonusSetHelper
    {
        public static ATBonusSet DeSerialize(BinaryReader br)
        {
            var obj = new ATBonusSet
            {
                ID = br.ReadUInt32(),
                PlayerOrFoeValue = br.ReadByte(),
                GeneralAtBonusChance = br.ReadByte(),
                HpHealSBonusChance = br.ReadByte(),
                EpHealSBonusChance = br.ReadByte(),
                CpHealSBonusChance = br.ReadByte(),
                HpHealLBonusChance = br.ReadByte(),
                EpHealLBonusChance = br.ReadByte(),
                CpHealLBonusChance = br.ReadByte(),
                UnknownBonusChance1 = br.ReadByte(),
                UnknownBonusChance2 = br.ReadByte(),
                UnknownBonusChance3 = br.ReadByte(),
                UnknownBonusChance4 = br.ReadByte(),
                UnknownBonusChance5 = br.ReadByte(),
                UnknownBonusChance6 = br.ReadByte(),
                UnknownBonusChance7 = br.ReadByte(),
                UnknownBonusChance8 = br.ReadByte(),
                UnknownBonusChance9 = br.ReadByte(),
                UnknownBonusChance10 = br.ReadByte(),
                UnknownBonusChance11 = br.ReadByte(),
                Byte1 = br.ReadByte()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, ATBonusSet tbl)
        {
            if (tbl is not ATBonusSet obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.PlayerOrFoeValue);
            bw.Write(obj.GeneralAtBonusChance);
            bw.Write(obj.HpHealSBonusChance);
            bw.Write(obj.EpHealSBonusChance);
            bw.Write(obj.CpHealSBonusChance);
            bw.Write(obj.HpHealLBonusChance);
            bw.Write(obj.EpHealLBonusChance);
            bw.Write(obj.CpHealLBonusChance);
            bw.Write(obj.UnknownBonusChance1);
            bw.Write(obj.UnknownBonusChance2);
            bw.Write(obj.UnknownBonusChance3);
            bw.Write(obj.UnknownBonusChance4);
            bw.Write(obj.UnknownBonusChance5);
            bw.Write(obj.UnknownBonusChance6);
            bw.Write(obj.UnknownBonusChance7);
            bw.Write(obj.UnknownBonusChance8);
            bw.Write(obj.UnknownBonusChance9);
            bw.Write(obj.UnknownBonusChance10);
            bw.Write(obj.UnknownBonusChance11);
            bw.Write(obj.Byte1);
        }
    }

    public static class BattleLevelFieldHelper
    {
        public static BattleLevelField DeSerialize(BinaryReader br)
        {
            var obj = new BattleLevelField
            {
                DifficultyID = br.ReadUInt32(),
                PlayerDamageMultiplier = br.ReadUInt32(),
                EnemyDamageMultiplier = br.ReadUInt32(),
                PlayerStunDamageMultiplier = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BattleLevelField tbl)
        {
            if (tbl is not BattleLevelField obj) return;
            bw.Write(obj.DifficultyID);
            bw.Write(obj.PlayerDamageMultiplier);
            bw.Write(obj.EnemyDamageMultiplier);
            bw.Write(obj.PlayerStunDamageMultiplier);
        }
    }

    public static class BattleLevelTurnHelper
    {
        public static BattleLevelTurn DeSerialize(BinaryReader br)
        {
            var obj = new BattleLevelTurn
            {
                DifficultyID = br.ReadUInt32(),
                PlayerDamageMultiplier = br.ReadUInt32(),
                EnemyDamageMultiplier = br.ReadUInt32(),
                HpOrUnknownMulti1 = br.ReadUInt32(),
                HpOrUnknownMulti2 = br.ReadUInt32(),
                EnemySpdMultiplier = br.ReadUInt32(),
                Int1 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BattleLevelTurn tbl)
        {
            if (tbl is not BattleLevelTurn obj) return;
            bw.Write(obj.DifficultyID);
            bw.Write(obj.PlayerDamageMultiplier);
            bw.Write(obj.EnemyDamageMultiplier);
            bw.Write(obj.HpOrUnknownMulti1);
            bw.Write(obj.HpOrUnknownMulti2);
            bw.Write(obj.EnemySpdMultiplier);
            bw.Write(obj.Int1);
        }
    }

    public static class BattleEnemyLevelStatusAdjustHelper
    {
        public static BattleEnemyLevelStatusAdjust DeSerialize(BinaryReader br)
        {
            var obj = new BattleEnemyLevelStatusAdjust
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BattleEnemyLevelStatusAdjust tbl)
        {
            if (tbl is not BattleEnemyLevelStatusAdjust obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
        }
    }

    public static class BattleEnemyLevelAdjustHelper
    {
        public static BattleEnemyLevelAdjust DeSerialize(BinaryReader br)
        {
            var obj = new BattleEnemyLevelAdjust
            {
                Long1 = br.ReadInt64(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, BattleEnemyLevelAdjust tbl)
        {
            if (tbl is not BattleEnemyLevelAdjust obj) return;
            bw.Write(obj.Long1);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
        }
    }

    public static class TacticalBonusHelper
    {
        public static TacticalBonus DeSerialize(BinaryReader br)
        {
            var obj = new TacticalBonus
            {
                ID = br.ReadUInt32(),
                ExpBonus = br.ReadSingle(),
                Byte1 = br.ReadByte(),
                Byte2 = br.ReadByte(),
                Empty1 = br.ReadInt32(),
                Empty2 = br.ReadInt16(),
                Text = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, TacticalBonus tbl)
        {
            if (tbl is not TacticalBonus obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.ExpBonus);
            bw.Write(obj.Byte1);
            bw.Write(obj.Byte2);
            bw.Write(obj.Empty1);
            bw.Write(obj.Empty2);
            bw.Write(obj.Text);
        }
    }
}