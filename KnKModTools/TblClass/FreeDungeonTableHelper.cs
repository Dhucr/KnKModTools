using System.IO;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public static class FreeDungeonTableHelper
    {
        public static FreeDungeonTable DeSerialize(BinaryReader br)
        {
            FreeDungeonTable obj = TBLHelper.DeSerialize<FreeDungeonTable>(br);
            // 处理SubHeader关联数组: FreeDungeonDatas
            SubHeader? subHeader_FreeDungeonDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonData");
            if (subHeader_FreeDungeonDatas != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonDatas.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonDatas = new FreeDungeonData[subHeader_FreeDungeonDatas.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonDatas.NodeCount; i++)
                {
                    obj.FreeDungeonDatas[i] = FreeDungeonDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonDatas = Array.Empty<FreeDungeonData>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonDatas, obj.FreeDungeonDatas);
            // 处理SubHeader关联数组: FreeDungeonFloorCompositionDatas
            SubHeader? subHeader_FreeDungeonFloorCompositionDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonFloorCompositionData");
            if (subHeader_FreeDungeonFloorCompositionDatas != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonFloorCompositionDatas.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonFloorCompositionDatas = new FreeDungeonFloorCompositionData[subHeader_FreeDungeonFloorCompositionDatas.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonFloorCompositionDatas.NodeCount; i++)
                {
                    obj.FreeDungeonFloorCompositionDatas[i] = FreeDungeonFloorCompositionDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonFloorCompositionDatas = Array.Empty<FreeDungeonFloorCompositionData>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonFloorCompositionDatas, obj.FreeDungeonFloorCompositionDatas);
            // 处理SubHeader关联数组: FreeDungeonNullSquaresPattrens
            SubHeader? subHeader_FreeDungeonNullSquaresPattrens = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonNullSquaresPattren");
            if (subHeader_FreeDungeonNullSquaresPattrens != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonNullSquaresPattrens.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonNullSquaresPattrens = new FreeDungeonNullSquaresPattren[subHeader_FreeDungeonNullSquaresPattrens.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonNullSquaresPattrens.NodeCount; i++)
                {
                    obj.FreeDungeonNullSquaresPattrens[i] = FreeDungeonNullSquaresPattrenHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonNullSquaresPattrens = Array.Empty<FreeDungeonNullSquaresPattren>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonNullSquaresPattrens, obj.FreeDungeonNullSquaresPattrens);
            // 处理SubHeader关联数组: FreeDungeonSquaresPattrenDatas
            SubHeader? subHeader_FreeDungeonSquaresPattrenDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSquaresPattrenData");
            if (subHeader_FreeDungeonSquaresPattrenDatas != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonSquaresPattrenDatas.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonSquaresPattrenDatas = new FreeDungeonSquaresPattrenData[subHeader_FreeDungeonSquaresPattrenDatas.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonSquaresPattrenDatas.NodeCount; i++)
                {
                    obj.FreeDungeonSquaresPattrenDatas[i] = FreeDungeonSquaresPattrenDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonSquaresPattrenDatas = Array.Empty<FreeDungeonSquaresPattrenData>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonSquaresPattrenDatas, obj.FreeDungeonSquaresPattrenDatas);
            // 处理SubHeader关联数组: FreeDungeonSections
            SubHeader? subHeader_FreeDungeonSections = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSection");
            if (subHeader_FreeDungeonSections != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonSections.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonSections = new FreeDungeonSection[subHeader_FreeDungeonSections.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonSections.NodeCount; i++)
                {
                    obj.FreeDungeonSections[i] = FreeDungeonSectionHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonSections = Array.Empty<FreeDungeonSection>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonSections, obj.FreeDungeonSections);
            // 处理SubHeader关联数组: FreeDungeonSwapMasses
            SubHeader? subHeader_FreeDungeonSwapMasses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSwapMass");
            if (subHeader_FreeDungeonSwapMasses != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonSwapMasses.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonSwapMasses = new FreeDungeonSwapMass[subHeader_FreeDungeonSwapMasses.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonSwapMasses.NodeCount; i++)
                {
                    obj.FreeDungeonSwapMasses[i] = FreeDungeonSwapMassHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonSwapMasses = Array.Empty<FreeDungeonSwapMass>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonSwapMasses, obj.FreeDungeonSwapMasses);
            // 处理SubHeader关联数组: FreeDungeonRewards
            SubHeader? subHeader_FreeDungeonRewards = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonReward");
            if (subHeader_FreeDungeonRewards != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonRewards.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonRewards = new FreeDungeonReward[subHeader_FreeDungeonRewards.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonRewards.NodeCount; i++)
                {
                    obj.FreeDungeonRewards[i] = FreeDungeonRewardHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonRewards = Array.Empty<FreeDungeonReward>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonRewards, obj.FreeDungeonRewards);
            // 处理SubHeader关联数组: FreeDungeonSupportPoss
            SubHeader? subHeader_FreeDungeonSupportPoss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSupportPos");
            if (subHeader_FreeDungeonSupportPoss != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonSupportPoss.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonSupportPoss = new FreeDungeonSupportPos[subHeader_FreeDungeonSupportPoss.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonSupportPoss.NodeCount; i++)
                {
                    obj.FreeDungeonSupportPoss[i] = FreeDungeonSupportPosHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonSupportPoss = Array.Empty<FreeDungeonSupportPos>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonSupportPoss, obj.FreeDungeonSupportPoss);
            // 处理SubHeader关联数组: FreeDungeonAvatarCharas
            SubHeader? subHeader_FreeDungeonAvatarCharas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonAvatarChara");
            if (subHeader_FreeDungeonAvatarCharas != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonAvatarCharas.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonAvatarCharas = new FreeDungeonAvatarChara[subHeader_FreeDungeonAvatarCharas.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonAvatarCharas.NodeCount; i++)
                {
                    obj.FreeDungeonAvatarCharas[i] = FreeDungeonAvatarCharaHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonAvatarCharas = Array.Empty<FreeDungeonAvatarChara>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonAvatarCharas, obj.FreeDungeonAvatarCharas);
            // 处理SubHeader关联数组: FreeDungeonMainMenuLists
            SubHeader? subHeader_FreeDungeonMainMenuLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonMainMenuList");
            if (subHeader_FreeDungeonMainMenuLists != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonMainMenuLists.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonMainMenuLists = new FreeDungeonMainMenuList[subHeader_FreeDungeonMainMenuLists.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonMainMenuLists.NodeCount; i++)
                {
                    obj.FreeDungeonMainMenuLists[i] = FreeDungeonMainMenuListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonMainMenuLists = Array.Empty<FreeDungeonMainMenuList>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonMainMenuLists, obj.FreeDungeonMainMenuLists);
            // 处理SubHeader关联数组: FreeDungeonShopListItemDatas
            SubHeader? subHeader_FreeDungeonShopListItemDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonShopListItemData");
            if (subHeader_FreeDungeonShopListItemDatas != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonShopListItemDatas.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonShopListItemDatas = new FreeDungeonShopListItemData[subHeader_FreeDungeonShopListItemDatas.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonShopListItemDatas.NodeCount; i++)
                {
                    obj.FreeDungeonShopListItemDatas[i] = FreeDungeonShopListItemDataHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonShopListItemDatas = Array.Empty<FreeDungeonShopListItemData>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonShopListItemDatas, obj.FreeDungeonShopListItemDatas);
            // 处理SubHeader关联数组: FreeDungeonShopTypeLists
            SubHeader? subHeader_FreeDungeonShopTypeLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonShopTypeList");
            if (subHeader_FreeDungeonShopTypeLists != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonShopTypeLists.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonShopTypeLists = new FreeDungeonShopTypeList[subHeader_FreeDungeonShopTypeLists.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonShopTypeLists.NodeCount; i++)
                {
                    obj.FreeDungeonShopTypeLists[i] = FreeDungeonShopTypeListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonShopTypeLists = Array.Empty<FreeDungeonShopTypeList>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonShopTypeLists, obj.FreeDungeonShopTypeLists);
            // 处理SubHeader关联数组: FreeDungeonTBoxes
            SubHeader? subHeader_FreeDungeonTBoxes = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonTBox");
            if (subHeader_FreeDungeonTBoxes != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonTBoxes.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonTBoxes = new FreeDungeonTBox[subHeader_FreeDungeonTBoxes.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonTBoxes.NodeCount; i++)
                {
                    obj.FreeDungeonTBoxes[i] = FreeDungeonTBoxHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonTBoxes = Array.Empty<FreeDungeonTBox>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonTBoxes, obj.FreeDungeonTBoxes);
            // 处理SubHeader关联数组: FreeDungeonBreakObjs
            SubHeader? subHeader_FreeDungeonBreakObjs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonBreakObj");
            if (subHeader_FreeDungeonBreakObjs != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonBreakObjs.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonBreakObjs = new FreeDungeonBreakObj[subHeader_FreeDungeonBreakObjs.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonBreakObjs.NodeCount; i++)
                {
                    obj.FreeDungeonBreakObjs[i] = FreeDungeonBreakObjHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonBreakObjs = Array.Empty<FreeDungeonBreakObj>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonBreakObjs, obj.FreeDungeonBreakObjs);
            // 处理SubHeader关联数组: FreeDungeonRandomMasses
            SubHeader? subHeader_FreeDungeonRandomMasses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonRandomMass");
            if (subHeader_FreeDungeonRandomMasses != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonRandomMasses.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonRandomMasses = new FreeDungeonRandomMass[subHeader_FreeDungeonRandomMasses.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonRandomMasses.NodeCount; i++)
                {
                    obj.FreeDungeonRandomMasses[i] = FreeDungeonRandomMassHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonRandomMasses = Array.Empty<FreeDungeonRandomMass>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonRandomMasses, obj.FreeDungeonRandomMasses);
            // 处理SubHeader关联数组: FreeDungeonClearTargetTexts
            SubHeader? subHeader_FreeDungeonClearTargetTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonClearTargetText");
            if (subHeader_FreeDungeonClearTargetTexts != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonClearTargetTexts.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonClearTargetTexts = new FreeDungeonClearTargetText[subHeader_FreeDungeonClearTargetTexts.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonClearTargetTexts.NodeCount; i++)
                {
                    obj.FreeDungeonClearTargetTexts[i] = FreeDungeonClearTargetTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonClearTargetTexts = Array.Empty<FreeDungeonClearTargetText>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonClearTargetTexts, obj.FreeDungeonClearTargetTexts);
            // 处理SubHeader关联数组: FreeDungeonMassEffectTexts
            SubHeader? subHeader_FreeDungeonMassEffectTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonMassEffectText");
            if (subHeader_FreeDungeonMassEffectTexts != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonMassEffectTexts.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonMassEffectTexts = new FreeDungeonMassEffectText[subHeader_FreeDungeonMassEffectTexts.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonMassEffectTexts.NodeCount; i++)
                {
                    obj.FreeDungeonMassEffectTexts[i] = FreeDungeonMassEffectTextHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonMassEffectTexts = Array.Empty<FreeDungeonMassEffectText>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonMassEffectTexts, obj.FreeDungeonMassEffectTexts);
            // 处理SubHeader关联数组: FreeDungeonTBoxMassContents
            SubHeader? subHeader_FreeDungeonTBoxMassContents = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonTBoxMassContents");
            if (subHeader_FreeDungeonTBoxMassContents != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonTBoxMassContents.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonTBoxMassContents = new FreeDungeonTBoxMassContents[subHeader_FreeDungeonTBoxMassContents.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonTBoxMassContents.NodeCount; i++)
                {
                    obj.FreeDungeonTBoxMassContents[i] = FreeDungeonTBoxMassContentsHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonTBoxMassContents = Array.Empty<FreeDungeonTBoxMassContents>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonTBoxMassContents, obj.FreeDungeonTBoxMassContents);
            // 处理SubHeader关联数组: FreeDungeonCraftBuildItemLists
            SubHeader? subHeader_FreeDungeonCraftBuildItemLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonCraftBuildItemList");
            if (subHeader_FreeDungeonCraftBuildItemLists != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonCraftBuildItemLists.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonCraftBuildItemLists = new FreeDungeonCraftBuildItemList[subHeader_FreeDungeonCraftBuildItemLists.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonCraftBuildItemLists.NodeCount; i++)
                {
                    obj.FreeDungeonCraftBuildItemLists[i] = FreeDungeonCraftBuildItemListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonCraftBuildItemLists = Array.Empty<FreeDungeonCraftBuildItemList>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonCraftBuildItemLists, obj.FreeDungeonCraftBuildItemLists);
            // 处理SubHeader关联数组: FreeDungeonFactoryExpansionLists
            SubHeader? subHeader_FreeDungeonFactoryExpansionLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonFactoryExpansionList");
            if (subHeader_FreeDungeonFactoryExpansionLists != null)
            {
                br.BaseStream.Seek(subHeader_FreeDungeonFactoryExpansionLists.DataOffset, SeekOrigin.Begin);
                obj.FreeDungeonFactoryExpansionLists = new FreeDungeonFactoryExpansionList[subHeader_FreeDungeonFactoryExpansionLists.NodeCount];
                for (var i = 0; i < subHeader_FreeDungeonFactoryExpansionLists.NodeCount; i++)
                {
                    obj.FreeDungeonFactoryExpansionLists[i] = FreeDungeonFactoryExpansionListHelper.DeSerialize(br);
                }
            }
            else
            {
                obj.FreeDungeonFactoryExpansionLists = Array.Empty<FreeDungeonFactoryExpansionList>();
            }
            obj.NodeDatas.Add(subHeader_FreeDungeonFactoryExpansionLists, obj.FreeDungeonFactoryExpansionLists);

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
            FreeDungeonTable.SManager = obj.Manager;

            return obj;
        }

        public static void Serialize(BinaryWriter bw, TBL tbl)
        {
            if (tbl is not FreeDungeonTable obj) return;
            TBLHelper.Serialize(bw, obj);
            // 处理SubHeader关联数组的序列化: FreeDungeonDatas
            SubHeader? subHeader_FreeDungeonDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonData");
            if (subHeader_FreeDungeonDatas != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonDatas.NodeCount; i++)
                {
                    FreeDungeonDataHelper.Serialize(bw, obj.FreeDungeonDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonFloorCompositionDatas
            SubHeader? subHeader_FreeDungeonFloorCompositionDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonFloorCompositionData");
            if (subHeader_FreeDungeonFloorCompositionDatas != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonFloorCompositionDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonFloorCompositionDatas.NodeCount; i++)
                {
                    FreeDungeonFloorCompositionDataHelper.Serialize(bw, obj.FreeDungeonFloorCompositionDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonNullSquaresPattrens
            SubHeader? subHeader_FreeDungeonNullSquaresPattrens = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonNullSquaresPattren");
            if (subHeader_FreeDungeonNullSquaresPattrens != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonNullSquaresPattrens.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonNullSquaresPattrens.NodeCount; i++)
                {
                    FreeDungeonNullSquaresPattrenHelper.Serialize(bw, obj.FreeDungeonNullSquaresPattrens[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonSquaresPattrenDatas
            SubHeader? subHeader_FreeDungeonSquaresPattrenDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSquaresPattrenData");
            if (subHeader_FreeDungeonSquaresPattrenDatas != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonSquaresPattrenDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonSquaresPattrenDatas.NodeCount; i++)
                {
                    FreeDungeonSquaresPattrenDataHelper.Serialize(bw, obj.FreeDungeonSquaresPattrenDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonSections
            SubHeader? subHeader_FreeDungeonSections = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSection");
            if (subHeader_FreeDungeonSections != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonSections.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonSections.NodeCount; i++)
                {
                    FreeDungeonSectionHelper.Serialize(bw, obj.FreeDungeonSections[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonSwapMasses
            SubHeader? subHeader_FreeDungeonSwapMasses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSwapMass");
            if (subHeader_FreeDungeonSwapMasses != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonSwapMasses.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonSwapMasses.NodeCount; i++)
                {
                    FreeDungeonSwapMassHelper.Serialize(bw, obj.FreeDungeonSwapMasses[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonRewards
            SubHeader? subHeader_FreeDungeonRewards = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonReward");
            if (subHeader_FreeDungeonRewards != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonRewards.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonRewards.NodeCount; i++)
                {
                    FreeDungeonRewardHelper.Serialize(bw, obj.FreeDungeonRewards[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonSupportPoss
            SubHeader? subHeader_FreeDungeonSupportPoss = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonSupportPos");
            if (subHeader_FreeDungeonSupportPoss != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonSupportPoss.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonSupportPoss.NodeCount; i++)
                {
                    FreeDungeonSupportPosHelper.Serialize(bw, obj.FreeDungeonSupportPoss[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonAvatarCharas
            SubHeader? subHeader_FreeDungeonAvatarCharas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonAvatarChara");
            if (subHeader_FreeDungeonAvatarCharas != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonAvatarCharas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonAvatarCharas.NodeCount; i++)
                {
                    FreeDungeonAvatarCharaHelper.Serialize(bw, obj.FreeDungeonAvatarCharas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonMainMenuLists
            SubHeader? subHeader_FreeDungeonMainMenuLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonMainMenuList");
            if (subHeader_FreeDungeonMainMenuLists != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonMainMenuLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonMainMenuLists.NodeCount; i++)
                {
                    FreeDungeonMainMenuListHelper.Serialize(bw, obj.FreeDungeonMainMenuLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonShopListItemDatas
            SubHeader? subHeader_FreeDungeonShopListItemDatas = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonShopListItemData");
            if (subHeader_FreeDungeonShopListItemDatas != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonShopListItemDatas.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonShopListItemDatas.NodeCount; i++)
                {
                    FreeDungeonShopListItemDataHelper.Serialize(bw, obj.FreeDungeonShopListItemDatas[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonShopTypeLists
            SubHeader? subHeader_FreeDungeonShopTypeLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonShopTypeList");
            if (subHeader_FreeDungeonShopTypeLists != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonShopTypeLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonShopTypeLists.NodeCount; i++)
                {
                    FreeDungeonShopTypeListHelper.Serialize(bw, obj.FreeDungeonShopTypeLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonTBoxes
            SubHeader? subHeader_FreeDungeonTBoxes = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonTBox");
            if (subHeader_FreeDungeonTBoxes != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonTBoxes.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonTBoxes.NodeCount; i++)
                {
                    FreeDungeonTBoxHelper.Serialize(bw, obj.FreeDungeonTBoxes[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonBreakObjs
            SubHeader? subHeader_FreeDungeonBreakObjs = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonBreakObj");
            if (subHeader_FreeDungeonBreakObjs != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonBreakObjs.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonBreakObjs.NodeCount; i++)
                {
                    FreeDungeonBreakObjHelper.Serialize(bw, obj.FreeDungeonBreakObjs[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonRandomMasses
            SubHeader? subHeader_FreeDungeonRandomMasses = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonRandomMass");
            if (subHeader_FreeDungeonRandomMasses != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonRandomMasses.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonRandomMasses.NodeCount; i++)
                {
                    FreeDungeonRandomMassHelper.Serialize(bw, obj.FreeDungeonRandomMasses[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonClearTargetTexts
            SubHeader? subHeader_FreeDungeonClearTargetTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonClearTargetText");
            if (subHeader_FreeDungeonClearTargetTexts != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonClearTargetTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonClearTargetTexts.NodeCount; i++)
                {
                    FreeDungeonClearTargetTextHelper.Serialize(bw, obj.FreeDungeonClearTargetTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonMassEffectTexts
            SubHeader? subHeader_FreeDungeonMassEffectTexts = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonMassEffectText");
            if (subHeader_FreeDungeonMassEffectTexts != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonMassEffectTexts.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonMassEffectTexts.NodeCount; i++)
                {
                    FreeDungeonMassEffectTextHelper.Serialize(bw, obj.FreeDungeonMassEffectTexts[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonTBoxMassContents
            SubHeader? subHeader_FreeDungeonTBoxMassContents = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonTBoxMassContents");
            if (subHeader_FreeDungeonTBoxMassContents != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonTBoxMassContents.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonTBoxMassContents.NodeCount; i++)
                {
                    FreeDungeonTBoxMassContentsHelper.Serialize(bw, obj.FreeDungeonTBoxMassContents[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonCraftBuildItemLists
            SubHeader? subHeader_FreeDungeonCraftBuildItemLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonCraftBuildItemList");
            if (subHeader_FreeDungeonCraftBuildItemLists != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonCraftBuildItemLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonCraftBuildItemLists.NodeCount; i++)
                {
                    FreeDungeonCraftBuildItemListHelper.Serialize(bw, obj.FreeDungeonCraftBuildItemLists[i]);
                }
            }

            // 处理SubHeader关联数组的序列化: FreeDungeonFactoryExpansionLists
            SubHeader? subHeader_FreeDungeonFactoryExpansionLists = obj.Nodes
                .FirstOrDefault(n => n.DisplayName == "FreeDungeonFactoryExpansionList");
            if (subHeader_FreeDungeonFactoryExpansionLists != null)
            {
                bw.BaseStream.Seek(subHeader_FreeDungeonFactoryExpansionLists.DataOffset, SeekOrigin.Begin);
                for (var i = 0; i < subHeader_FreeDungeonFactoryExpansionLists.NodeCount; i++)
                {
                    FreeDungeonFactoryExpansionListHelper.Serialize(bw, obj.FreeDungeonFactoryExpansionLists[i]);
                }
            }

            obj.Handler.Writer = bw;
            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });
        }
    }

    public static class FreeDungeonDataHelper
    {
        public static FreeDungeonData DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonData
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Long1 = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt32(),
                Short1 = br.ReadInt16(),
                Short2 = br.ReadInt16(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt32(),
                Short3 = br.ReadInt16(),
                Short4 = br.ReadInt16(),
                Arr4 = br.ReadInt64(),
                Count4 = br.ReadInt64(),
                Arr5 = br.ReadInt64(),
                Count5 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Int5 = br.ReadInt32(),
                Int6 = br.ReadInt32(),
                Int7 = br.ReadInt32(),
                Int8 = br.ReadInt32(),
                Int9 = br.ReadInt32(),
                Int10 = br.ReadInt32(),
                Long2 = br.ReadInt64(),
                Int11 = br.ReadInt32(),
                Int12 = br.ReadInt32(),
                Int13 = br.ReadInt32(),
                Int14 = br.ReadInt32(),
                Long3 = br.ReadInt64(),
                Int15 = br.ReadInt32(),
                Long4 = br.ReadInt64(),
                Int16 = br.ReadInt32(),
                Int17 = br.ReadInt32(),
                Int18 = br.ReadInt32(),
                Int19 = br.ReadInt32(),
                Int20 = br.ReadInt32(),
                Int21 = br.ReadInt32(),
                Int22 = br.ReadInt32(),
                Int23 = br.ReadInt32(),
                Int24 = br.ReadInt32(),
                Int25 = br.ReadInt32(),
                Int26 = br.ReadInt32(),
                Int27 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonData tbl)
        {
            if (tbl is not FreeDungeonData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
            bw.Write(obj.Short3);
            bw.Write(obj.Short4);
            bw.Write(obj.Arr4);
            bw.Write(obj.Count4);
            bw.Write(obj.Arr5);
            bw.Write(obj.Count5);
            bw.Write(obj.Text1);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Int5);
            bw.Write(obj.Int6);
            bw.Write(obj.Int7);
            bw.Write(obj.Int8);
            bw.Write(obj.Int9);
            bw.Write(obj.Int10);
            bw.Write(obj.Long2);
            bw.Write(obj.Int11);
            bw.Write(obj.Int12);
            bw.Write(obj.Int13);
            bw.Write(obj.Int14);
            bw.Write(obj.Long3);
            bw.Write(obj.Int15);
            bw.Write(obj.Long4);
            bw.Write(obj.Int16);
            bw.Write(obj.Int17);
            bw.Write(obj.Int18);
            bw.Write(obj.Int19);
            bw.Write(obj.Int20);
            bw.Write(obj.Int21);
            bw.Write(obj.Int22);
            bw.Write(obj.Int23);
            bw.Write(obj.Int24);
            bw.Write(obj.Int25);
            bw.Write(obj.Int26);
            bw.Write(obj.Int27);
        }
    }

    public static class FreeDungeonFloorCompositionDataHelper
    {
        public static FreeDungeonFloorCompositionData DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonFloorCompositionData
            {
                ID = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64(),
                Arr4 = br.ReadInt64(),
                Count4 = br.ReadInt64(),
                Arr5 = br.ReadInt64(),
                Count5 = br.ReadInt64(),
                Arr6 = br.ReadInt64(),
                Count6 = br.ReadInt64(),
                Arr7 = br.ReadInt64(),
                Count7 = br.ReadInt64(),
                Arr8 = br.ReadInt64(),
                Count8 = br.ReadInt64(),
                Arr9 = br.ReadInt64(),
                Count9 = br.ReadInt64(),
                Arr10 = br.ReadInt64(),
                Count10 = br.ReadInt64(),
                Arr11 = br.ReadInt64(),
                Count11 = br.ReadInt64(),
                Arr12 = br.ReadInt64(),
                Count12 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonFloorCompositionData tbl)
        {
            if (tbl is not FreeDungeonFloorCompositionData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
            bw.Write(obj.Arr4);
            bw.Write(obj.Count4);
            bw.Write(obj.Arr5);
            bw.Write(obj.Count5);
            bw.Write(obj.Arr6);
            bw.Write(obj.Count6);
            bw.Write(obj.Arr7);
            bw.Write(obj.Count7);
            bw.Write(obj.Arr8);
            bw.Write(obj.Count8);
            bw.Write(obj.Arr9);
            bw.Write(obj.Count9);
            bw.Write(obj.Arr10);
            bw.Write(obj.Count10);
            bw.Write(obj.Arr11);
            bw.Write(obj.Count11);
            bw.Write(obj.Arr12);
            bw.Write(obj.Count12);
        }
    }

    public static class FreeDungeonNullSquaresPattrenHelper
    {
        public static FreeDungeonNullSquaresPattren DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonNullSquaresPattren
            {
                Int1 = br.ReadUInt32(),
                Int2 = br.ReadUInt32(),
                Int3 = br.ReadUInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonNullSquaresPattren tbl)
        {
            if (tbl is not FreeDungeonNullSquaresPattren obj) return;
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
        }
    }

    public static class FreeDungeonSquaresPattrenDataHelper
    {
        public static FreeDungeonSquaresPattrenData DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonSquaresPattrenData
            {
                ID = br.ReadUInt32(),
                Int1 = br.ReadInt32(),
                Long1 = br.ReadInt64(),
                Arr = br.ReadInt64(),
                Count = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Long2 = br.ReadInt64(),
                Long3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonSquaresPattrenData tbl)
        {
            if (tbl is not FreeDungeonSquaresPattrenData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Long2);
            bw.Write(obj.Long3);
        }
    }

    public static class FreeDungeonSectionHelper
    {
        public static FreeDungeonSection DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonSection
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64(),
                Arr4 = br.ReadInt64(),
                Count4 = br.ReadInt64(),
                Arr5 = br.ReadInt64(),
                Count5 = br.ReadInt64(),
                Arr6 = br.ReadInt64(),
                Count6 = br.ReadInt64(),
                Arr7 = br.ReadInt64(),
                Count7 = br.ReadInt64(),
                Arr8 = br.ReadInt64(),
                Count8 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonSection tbl)
        {
            if (tbl is not FreeDungeonSection obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
            bw.Write(obj.Arr4);
            bw.Write(obj.Count4);
            bw.Write(obj.Arr5);
            bw.Write(obj.Count5);
            bw.Write(obj.Arr6);
            bw.Write(obj.Count6);
            bw.Write(obj.Arr7);
            bw.Write(obj.Count7);
            bw.Write(obj.Arr8);
            bw.Write(obj.Count8);
        }
    }

    public static class FreeDungeonSwapMassHelper
    {
        public static FreeDungeonSwapMass DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonSwapMass
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonSwapMass tbl)
        {
            if (tbl is not FreeDungeonSwapMass obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
        }
    }

    public static class FreeDungeonRewardHelper
    {
        public static FreeDungeonReward DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonReward
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Arr = br.ReadInt64(),
                Count = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonReward tbl)
        {
            if (tbl is not FreeDungeonReward obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }

    public static class FreeDungeonSupportPosHelper
    {
        public static FreeDungeonSupportPos DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonSupportPos
            {
                Int1 = br.ReadInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle(),
                Float4 = br.ReadSingle(),
                Float5 = br.ReadSingle(),
                Float6 = br.ReadSingle(),
                Float7 = br.ReadSingle(),
                Float8 = br.ReadSingle(),
                Float9 = br.ReadSingle(),
                Float10 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonSupportPos tbl)
        {
            if (tbl is not FreeDungeonSupportPos obj) return;
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
        }
    }

    public static class FreeDungeonAvatarCharaHelper
    {
        public static FreeDungeonAvatarChara DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonAvatarChara
            {
                ID = br.ReadInt64(),
                Float1 = br.ReadSingle(),
                Empty1 = br.ReadInt32(),
                Float2 = br.ReadSingle(),
                Empty2 = br.ReadInt32(),
                Float3 = br.ReadSingle(),
                Empty3 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonAvatarChara tbl)
        {
            if (tbl is not FreeDungeonAvatarChara obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Float1);
            bw.Write(obj.Empty1);
            bw.Write(obj.Float2);
            bw.Write(obj.Empty2);
            bw.Write(obj.Float3);
            bw.Write(obj.Empty3);
        }
    }

    public static class FreeDungeonMainMenuListHelper
    {
        public static FreeDungeonMainMenuList DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonMainMenuList
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonMainMenuList tbl)
        {
            if (tbl is not FreeDungeonMainMenuList obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
        }
    }

    public static class FreeDungeonShopListItemDataHelper
    {
        public static FreeDungeonShopListItemData DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonShopListItemData
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Arr1 = br.ReadInt64(),
                Count1 = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Long1 = br.ReadInt64(),
                Arr2 = br.ReadInt64(),
                Count2 = br.ReadInt64(),
                Arr3 = br.ReadInt64(),
                Count3 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonShopListItemData tbl)
        {
            if (tbl is not FreeDungeonShopListItemData obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Arr1);
            bw.Write(obj.Count1);
            bw.Write(obj.Text1);
            bw.Write(obj.Long1);
            bw.Write(obj.Arr2);
            bw.Write(obj.Count2);
            bw.Write(obj.Arr3);
            bw.Write(obj.Count3);
            bw.Write(obj.Text2);
        }
    }

    public static class FreeDungeonShopTypeListHelper
    {
        public static FreeDungeonShopTypeList DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonShopTypeList
            {
                ID = br.ReadInt64(),
                Arr = br.ReadInt64(),
                Count = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64(),
                Text3 = br.ReadInt64(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonShopTypeList tbl)
        {
            if (tbl is not FreeDungeonShopTypeList obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
            bw.Write(obj.Text3);
            bw.Write(obj.Long1);
        }
    }

    public static class FreeDungeonTBoxHelper
    {
        public static FreeDungeonTBox DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonTBox
            {
                ID = br.ReadInt64(),
                Arr = br.ReadInt64(),
                Count = br.ReadInt64(),
                Long1 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonTBox tbl)
        {
            if (tbl is not FreeDungeonTBox obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Arr);
            bw.Write(obj.Count);
            bw.Write(obj.Long1);
        }
    }

    public static class FreeDungeonBreakObjHelper
    {
        public static FreeDungeonBreakObj DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonBreakObj
            {
                ID = br.ReadInt64(),
                Text = br.ReadInt64(),
                Short1 = br.ReadInt16(),
                Short2 = br.ReadInt16(),
                Short3 = br.ReadInt16(),
                Short4 = br.ReadInt16()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonBreakObj tbl)
        {
            if (tbl is not FreeDungeonBreakObj obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text);
            bw.Write(obj.Short1);
            bw.Write(obj.Short2);
            bw.Write(obj.Short3);
            bw.Write(obj.Short4);
        }
    }

    public static class FreeDungeonRandomMassHelper
    {
        public static FreeDungeonRandomMass DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonRandomMass
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32(),
                Int5 = br.ReadInt32(),
                Int6 = br.ReadInt32(),
                Int7 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonRandomMass tbl)
        {
            if (tbl is not FreeDungeonRandomMass obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
            bw.Write(obj.Int5);
            bw.Write(obj.Int6);
            bw.Write(obj.Int7);
        }
    }

    public static class FreeDungeonClearTargetTextHelper
    {
        public static FreeDungeonClearTargetText DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonClearTargetText
            {
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonClearTargetText tbl)
        {
            if (tbl is not FreeDungeonClearTargetText obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }

    public static class FreeDungeonMassEffectTextHelper
    {
        public static FreeDungeonMassEffectText DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonMassEffectText
            {
                ID = br.ReadInt64(),
                Text1 = br.ReadInt64(),
                Text2 = br.ReadInt64()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonMassEffectText tbl)
        {
            if (tbl is not FreeDungeonMassEffectText obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Text1);
            bw.Write(obj.Text2);
        }
    }

    public static class FreeDungeonTBoxMassContentsHelper
    {
        public static FreeDungeonTBoxMassContents DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonTBoxMassContents
            {
                ID = br.ReadInt32(),
                Float1 = br.ReadSingle(),
                Float2 = br.ReadSingle(),
                Float3 = br.ReadSingle()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonTBoxMassContents tbl)
        {
            if (tbl is not FreeDungeonTBoxMassContents obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Float1);
            bw.Write(obj.Float2);
            bw.Write(obj.Float3);
        }
    }

    public static class FreeDungeonCraftBuildItemListHelper
    {
        public static FreeDungeonCraftBuildItemList DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonCraftBuildItemList
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonCraftBuildItemList tbl)
        {
            if (tbl is not FreeDungeonCraftBuildItemList obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
        }
    }

    public static class FreeDungeonFactoryExpansionListHelper
    {
        public static FreeDungeonFactoryExpansionList DeSerialize(BinaryReader br)
        {
            var obj = new FreeDungeonFactoryExpansionList
            {
                ID = br.ReadInt32(),
                Int1 = br.ReadInt32(),
                Int2 = br.ReadInt32(),
                Int3 = br.ReadInt32(),
                Int4 = br.ReadInt32()
            };
            return obj;
        }

        public static void Serialize(BinaryWriter bw, FreeDungeonFactoryExpansionList tbl)
        {
            if (tbl is not FreeDungeonFactoryExpansionList obj) return;
            bw.Write(obj.ID);
            bw.Write(obj.Int1);
            bw.Write(obj.Int2);
            bw.Write(obj.Int3);
            bw.Write(obj.Int4);
        }
    }
}