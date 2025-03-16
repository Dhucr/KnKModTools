using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class TBLData
    {
        public static readonly Dictionary<TBL, List<IDataPointer>> TBLPointerMap = [];

        public static readonly Dictionary<string, Func<BinaryReader, TBL>> TBLoadMap = new()
        {
            {"t_itemhelp", ItemHelpTableHelper.DeSerialize},
            {"t_item", ItemTableHelper.DeSerialize},
            {"t_condition_info", ConditionInfoTableHelper.DeSerialize},
            {"t_shard_skill", ShardSkillTableHelper.DeSerialize},
            {"t_skill", SkillTableHelper.DeSerialize},
            {"t_attr", AttrTableHelper.DeSerialize},
            {"t_hollowcore", HollowCoreTableHelper.DeSerialize},
            {"t_text", TextTableHelper.DeSerialize},
            {"t_skill_level", SkillLevelTableHelper.DeSerialize},
            {"t_artsdriver", ArtsdriverTableHelper.DeSerialize},
            {"t_shop", ShopTableHelper.DeSerialize},
            {"t_name", NameTableHelper.DeSerialize},
            {"t_status", StatusTableHelper.DeSerialize},
            {"t_quest", QuestTableHelper.DeSerialize},
            {"t_quartz_line", QuartzLineTableHelper.DeSerialize},
            {"t_books", BookTableHelper.DeSerialize},
            {"t_achievement", AchievementTableHelper.DeSerialize},
            {"t_archive", ArchiveTableHelper.DeSerialize},
            {"t_btlsys", BtlsysTableHelper.DeSerialize},
            {"t_chapter", ChapterTableHelper.DeSerialize},
            {"t_chrdata", ChrdataTableHelper.DeSerialize},
            {"t_connect", ConnectTableHelper.DeSerialize},
            {"t_constant_value", ConstantValueTableHelper.DeSerialize},
            {"t_costume", CostumeTableHelper.DeSerialize},
            {"t_dlc", DLCTableHelper.DeSerialize},
            {"t_effect", EffectTableHelper.DeSerialize},
            {"t_eventbox", EventBoxTableHelper.DeSerialize},
            {"t_evtable", EvTableHelper.DeSerialize},
            {"t_gourmet", GourmetTableHelper.DeSerialize},
            {"t_help", HelpTableHelper.DeSerialize},
            {"t_lookpoint", LookPointTableHelper.DeSerialize},
            {"t_marker", MarkerTableHelper.DeSerialize},
            {"t_mapjump", MapJumpTableHelper.DeSerialize},
            {"t_place", PlaceTableHelper.DeSerialize},
            {"t_tbox", TBoxTableHelper.DeSerialize},
            {"t_topic", TopicTableHelper.DeSerialize},
            {"t_tips", TipsTableHelper.DeSerialize},
            {"t_notemenu", NoteMenuTableHelper.DeSerialize},
            {"t_free_dungeon", FreeDungeonTableHelper.DeSerialize}
        };

        public static readonly Dictionary<string, Action<BinaryWriter, TBL>> TBLSaveMap = new()
        {
            {"t_itemhelp", ItemHelpTableHelper.Serialize},
            {"t_item", ItemTableHelper.Serialize},
            {"t_condition_info", ConditionInfoTableHelper.Serialize},
            {"t_shard_skill", ShardSkillTableHelper.Serialize},
            {"t_skill", SkillTableHelper.Serialize},
            {"t_attr", AttrTableHelper.Serialize},
            {"t_hollowcore", HollowCoreTableHelper.Serialize},
            {"t_text", TextTableHelper.Serialize},
            {"t_skill_level", SkillLevelTableHelper.Serialize},
            {"t_artsdriver", ArtsdriverTableHelper.Serialize},
            {"t_shop", ShopTableHelper.Serialize},
            {"t_name", NameTableHelper.Serialize},
            {"t_status", StatusTableHelper.Serialize},
            {"t_quest", QuestTableHelper.Serialize},
            {"t_quartz_line", QuartzLineTableHelper.Serialize},
            {"t_books", BookTableHelper.Serialize},
            {"t_achievement", AchievementTableHelper.Serialize},
            {"t_archive", ArchiveTableHelper.Serialize},
            {"t_btlsys", BtlsysTableHelper.Serialize},
            {"t_chapter", ChapterTableHelper.Serialize},
            {"t_chrdata", ChrdataTableHelper.Serialize},
            {"t_connect", ConnectTableHelper.Serialize},
            {"t_constant_value", ConstantValueTableHelper.Serialize},
            {"t_costume", CostumeTableHelper.Serialize},
            {"t_dlc", DLCTableHelper.Serialize},
            {"t_effect", EffectTableHelper.Serialize},
            {"t_eventbox", EventBoxTableHelper.Serialize},
            {"t_evtable", EvTableHelper.Serialize},
            {"t_gourmet", GourmetTableHelper.Serialize},
            {"t_help", HelpTableHelper.Serialize},
            {"t_lookpoint", LookPointTableHelper.Serialize},
            {"t_marker", MarkerTableHelper.Serialize},
            {"t_mapjump", MapJumpTableHelper.Serialize},
            {"t_place", PlaceTableHelper.Serialize},
            {"t_tbox", TBoxTableHelper.Serialize},
            {"t_topic", TopicTableHelper.Serialize},
            {"t_tips", TipsTableHelper.Serialize},
            {"t_notemenu", NoteMenuTableHelper.Serialize},
            {"t_free_dungeon", FreeDungeonTableHelper.Serialize}
        };
    }
}