using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sidekick.Windows.Settings;

namespace Sidekick.Windows.LeagueOverlay
{
    /// <summary>
    /// Interaction logic for LeagueOverlayView.xaml
    /// </summary>
    public partial class LeagueOverlayView : Window
    {
        private Dictionary<TabItem, int[]> tabPageSizeDictionary;
        private TabItem CurrentPage;

        public LeagueOverlayView()
        {
            InitializeComponent();
            UpdateBetrayalUIText();
            UpdateIncursionUIText();
            SettingsController.GetSettingsInstance().CurrentUILanguageProvider.UILanguageChanged += UpdateBetrayalUIText;
            SettingsController.GetSettingsInstance().CurrentUILanguageProvider.UILanguageChanged += UpdateIncursionUIText;
            tabPageSizeDictionary = new Dictionary<TabItem, int[]>()
            {
                { tabItemIncursion, new[] { 980, 1050 } },
                { tabItemDelve, new[] { 500, 500 } },
                { tabItemBetrayal, new[] { 520, 1200 } },
                { tabItemBlight, new[] { 500, 500 } },
                { tabItemMetamorph, new[] { 500, 500 } },
            };
            tabControlLeagueOverlay.SelectionChanged += TabControlLeagueOverlay_SelectionChanged;
            CurrentPage = tabItemIncursion;
            ToolTipService.ShowDurationProperty.OverrideMetadata(
                        typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));       // Tooltip opened indefinitly until mouse is moved
        }

        private void TabControlLeagueOverlay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPage = tabControlLeagueOverlay.SelectedItem as TabItem;
            SetCurrentPageWindowSize();
        }

        private void SetCurrentPageWindowSize()
        {
            if(CurrentPage != null)
            {
                if(!tabPageSizeDictionary.TryGetValue(CurrentPage, out var windowSize))
                {
                    throw new Exception("Window Size for TabPage is not defined correctly");
                }

                Height = windowSize[0];
                Width = windowSize[1];
                tabControlLeagueOverlay.Height = windowSize[0];
                tabControlLeagueOverlay.Width = windowSize[1];
            }
        }

        private void UpdateIncursionUIText()
        {
            var settings = SettingsController.GetSettingsInstance();

            labelIncursionHeaderContains.Content = settings.CurrentUILanguageProvider.Language.IncursionHeaderContains;
            labelIncursionHeaderModifiers.Content = settings.CurrentUILanguageProvider.Language.IncursionHeaderModifiers;

            labelIncursionLegendVeryValuable.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendVeryValuable;
            labelIncursionLegendValuable.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendValuable;
            labelIncursionLegendNotValuable.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendNotValuable;
            labelIncursionLegendLessValuable.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendLessValuable;

            textBlockGuardhouse.Text = settings.CurrentUILanguageProvider.Language.IncursionGuardhouse;
            textBlockBarracks.Text = settings.CurrentUILanguageProvider.Language.IncursionBarracks;
            textBlockHallOfWar.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfWar;
            textBlockGuardhouseModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionGuardhouseModifiers;

            textBlockWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionWorkshop;
            textBlockWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionWorkshopContains;
            textBlockWorkshopModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionWorkshopModifiers;
            textBlockEngineeringDepartment.Text = settings.CurrentUILanguageProvider.Language.IncursionEngineeringDepartment;
            textBlockFactory.Text = settings.CurrentUILanguageProvider.Language.IncursionFactory;

            textBlockExplosivesRoom.Text = settings.CurrentUILanguageProvider.Language.IncursionExplosivesRoom;
            textBlockExplosivesRoomContains.Text = settings.CurrentUILanguageProvider.Language.IncursionExplosivesRoomContains;
            textBlockDemolitionLab.Text = settings.CurrentUILanguageProvider.Language.IncursionDemolitionLab;
            textBlockShrineOfUnmaking.Text = settings.CurrentUILanguageProvider.Language.IncursionShrineOfUnmaking;

            textBlockSplinterReasearchLab.Text = settings.CurrentUILanguageProvider.Language.IncursionSplinterResearchLab;
            textBlockSplinterResearchLabContains.Text = settings.CurrentUILanguageProvider.Language.IncursionSplinterResearchLabContains;
            textBlockBreachContainmentChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionBreachContainmentChamber;
            textBlockHouseOfTheOthers.Text = settings.CurrentUILanguageProvider.Language.IncursionHouseOfOthers;

            textBlockVault.Text = settings.CurrentUILanguageProvider.Language.IncursionVault;
            textBlockVaultContains.Text = settings.CurrentUILanguageProvider.Language.IncursionVaultContains;
            textBlockTreasury.Text = settings.CurrentUILanguageProvider.Language.IncursionTreasury;
            textBlockWealthOfTheVaal.Text = settings.CurrentUILanguageProvider.Language.IncursionWealthOfTheVaal;

            textBlockSparringRoom.Text = settings.CurrentUILanguageProvider.Language.IncursionSparringRoom;
            textBlockSparringRoomContains.Text = settings.CurrentUILanguageProvider.Language.IncursionSparringRoomContains;
            textBlockSparringRoomModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionSparringRoomModifiers;
            textBlockArenaOfValour.Text = settings.CurrentUILanguageProvider.Language.IncursionArenaOfValour;
            textBlockHallOfChampions.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfChampions;

            textBlockArmourersWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionArmourersWorkshop;
            textBlockArmourersWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionArmourersWorkshopContains;
            textBlockArmourersWorkshopModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionArmourersWorkshopModifiers;
            textBlockArmoury.Text = settings.CurrentUILanguageProvider.Language.IncursionArmoury;
            textBlockChamberOfIron.Text = settings.CurrentUILanguageProvider.Language.IncursionChamberOfIron;

            textBlockJewellersWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionJewellersWorkshop;
            textBlockJewellersWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionJewellersWorkshopContains;
            textBlockJewelleryForge.Text = settings.CurrentUILanguageProvider.Language.IncursionJewelleryForge;
            textBlockGlitteringHalls.Text = settings.CurrentUILanguageProvider.Language.IncursionGlitteringHalls;

            textBlockSurveyorsStudy.Text = settings.CurrentUILanguageProvider.Language.IncursionSurveyorsStudy;
            textBlockSurveyorsStudyContains.Text = settings.CurrentUILanguageProvider.Language.IncursionSurveyorsStudyContains;
            textBlockOfficeOfCartography.Text = settings.CurrentUILanguageProvider.Language.IncursionOfficeOfCartography;
            textBlockAtlasOfWorlds.Text = settings.CurrentUILanguageProvider.Language.IncursionAtlasOfWorlds;

            textBlockGemcuttersWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionGemcuttersWorkshop;
            textBlockGemcuttersWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionGemcuttersWorkshopContains;
            textBlockDepartmentOfThaumaturgy.Text = settings.CurrentUILanguageProvider.Language.IncursionDepartmentOfThaumaturgy;
            textBlockDoryanisInstitute.Text = settings.CurrentUILanguageProvider.Language.IncursionDoryanisInstitute;
            textBlockGemcuttersWorkshopContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionDoubleGemCorruptionTooltip;

            textBlockTormentCell.Text = settings.CurrentUILanguageProvider.Language.IncursionTormentCells;
            textBlockTormentCellsContain.Text = settings.CurrentUILanguageProvider.Language.IncursionTormentCellsContains;
            textBlockTortureCages.Text = settings.CurrentUILanguageProvider.Language.IncursionTortureCages;
            textBlockSadistsDen.Text = settings.CurrentUILanguageProvider.Language.IncursionSadistsDen;

            textBlockStrongboxChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionStrongboxChamber;
            textBlockStrongboxChamberContains.Text = settings.CurrentUILanguageProvider.Language.IncursionStrongboxChamberContains;
            textBlockHallOfLocks.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfLocks;
            textBlockCourtOfSealedDeath.Text = settings.CurrentUILanguageProvider.Language.IncursionCourtOfTheSealedDeath;

            textBlockHallOfMettle.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfMettle;
            textBlockHallOfMettleContains.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfMettleContains;
            textBlockHallOfHeroes.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfHeroes;
            textBlockHallOfLegends.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfLegends;

            textBlockSacrificalChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionSacrificalChamber;
            textBlockSacrificalChamberContains.Text = settings.CurrentUILanguageProvider.Language.IncursionSacrificalChamberContains;
            textBlockHallOfOfferings.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfOfferings;
            textBlockApexOfAscension.Text = settings.CurrentUILanguageProvider.Language.IncursionApexOfAscension;

            textBlockStorageRoom.Text = settings.CurrentUILanguageProvider.Language.IncursionStorageRoom;
            textBlockStorageRoomContains.Text = settings.CurrentUILanguageProvider.Language.IncursionStorageRoomContains;
            textBlockWarehouses.Text = settings.CurrentUILanguageProvider.Language.IncursionWarehouses;
            textBlockMuseumOfArtifacts.Text = settings.CurrentUILanguageProvider.Language.IncursionMuseumOfArtifacts;

            textBlockCorruptionChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionCorruptionChamber;
            textBlockCorruptionChamberContains.Text = settings.CurrentUILanguageProvider.Language.IncursionCorruptionChamberContains;
            textBlockCorruptionChamberModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionCorruptionChamberModifiers;
            textBlockCatalystOfCorruption.Text = settings.CurrentUILanguageProvider.Language.IncursionCatalystOfCorruption;
            textBlockLocusOfCorruption.Text = settings.CurrentUILanguageProvider.Language.IncursionLocuOfCorruption;
            textBlockCorruptionChamberContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionDoubleCorruptionTooltip;

            textBlockShrineOfEmpowerment.Text = settings.CurrentUILanguageProvider.Language.IncursionShrineOfEmpowerment;
            textBlockShrineOfEmpowermentContains.Text = settings.CurrentUILanguageProvider.Language.IncursionShrineOfEmpowermentContains;
            textBlockShrineOfEmpowermentModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionShrineOfEmpowermentModifiers;
            textBlockSanctumOfUnity.Text = settings.CurrentUILanguageProvider.Language.IncursionSanctumOfUnity;
            textBlockTempleNexus.Text = settings.CurrentUILanguageProvider.Language.IncursionTempleNexus;

            textBlockTempestGenerator.Text = settings.CurrentUILanguageProvider.Language.IncursionTempestGenerator;
            textBlockTempestGeneratorContains.Text = settings.CurrentUILanguageProvider.Language.IncursionTempestGeneratorContains;
            textBlockTempestGeneratorModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionTempestGeneratorModifiers;
            textBlockHurricaneEngine.Text = settings.CurrentUILanguageProvider.Language.IncursionHurricaneEngine;
            textBlockStormOfCorruption.Text = settings.CurrentUILanguageProvider.Language.IncursionStormOfCorruption;
            textBlockTempestGeneratorContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionTopotanteModTooltip;

            textBlockPosionGarden.Text = settings.CurrentUILanguageProvider.Language.IncursionPoisionGarden;
            textBlockPosiionGardenContains.Text = settings.CurrentUILanguageProvider.Language.IncursionPosionGardenContains;
            textBlockPosionGardenModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionPoisonGardenModifiers;
            textBlockCultivarChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionCultivarChamber;
            textBlockToxicGrive.Text = settings.CurrentUILanguageProvider.Language.IncursionToxicGrove;
            textBlockPosiionGardenContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionTacatiModTooltip;

            textBlockTrapWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionTrapWorkshop;
            textBlockTrapWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionTrapWorkshopContains;
            textBlockTrapWorkshopModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionTrapWorkshopModifiers;
            textBlockTempleDefenseWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionTempleDefenseWorkshop;
            textBlockDefenseResearchLab.Text = settings.CurrentUILanguageProvider.Language.IncursionDefenseResearchLab;
            textBlockTrapWorkshopContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionMatalTooltip;

            textBlockPoolsOfRestoration.Text = settings.CurrentUILanguageProvider.Language.IncursionPoolsOfRestoration;
            textBlockPoolsOfRestorationContains.Text = settings.CurrentUILanguageProvider.Language.IncursionPoolsOfRestorationContains;
            textBlockPoolsOfRestorationModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionPoolsOfRestorationModifiers;
            textBlockSanctumOfVitality.Text = settings.CurrentUILanguageProvider.Language.IncursionSanctumOfVitality;
            textBlockSanctumOfImmortality.Text = settings.CurrentUILanguageProvider.Language.IncursionSanctumOfImmortality;
            textBlockPoolsOfRestorationContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionGuateliztzModTooltip;

            textBlockFlameWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionFlameWorkshop;
            textBlockFlameWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionFlameWorkshopContains;
            textBlockFlameWorkshopModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionFlameWorkshopModifiers;
            textBlockOmnitectForge.Text = settings.CurrentUILanguageProvider.Language.IncursionOmnitectForge;
            textBlockCrucibleOfFlame.Text = settings.CurrentUILanguageProvider.Language.IncursionCrucibleOfFlame;
            textBlockFlameWorkshopContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionPuhuarteModTooltuip;

            textBlockLightningWorkshop.Text = settings.CurrentUILanguageProvider.Language.IncursionLightningWorkshop;
            textBlockLightningWorkshopContains.Text = settings.CurrentUILanguageProvider.Language.IncursionLightningWorkshopContains;
            textBlockLightningWorkshopModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionLightningWorkshopModifiers;
            textBlockOmnitectReactorPlant.Text = settings.CurrentUILanguageProvider.Language.IncursionOmnitectReactorPlant;
            textBlockConduitOfLightning.Text = settings.CurrentUILanguageProvider.Language.IncursionConduitOfLightning;
            textBlockLightningWorkshopContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionXopecModTooltip;

            textBlockHatchery.Text = settings.CurrentUILanguageProvider.Language.IncursionHatchery;
            textBlockHatcherModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionHatcheryModifiers;
            textBlockHatcheryContains.Text = settings.CurrentUILanguageProvider.Language.IncursionHatcheryContains;
            textBlockAutomationLab.Text = settings.CurrentUILanguageProvider.Language.IncursionAutomationLab;
            textBlockHybridisationChamber.Text = settings.CurrentUILanguageProvider.Language.IncursionHybridisationChamber;
            textBlockHatcheryContains.ToolTip = settings.CurrentUILanguageProvider.Language.IncursionCitaqualotlModTooltip;

            textBlockRoyalMeetingRoom.Text = settings.CurrentUILanguageProvider.Language.IncursionRoyalMeetingRoom;
            textBlockRoyalMeetingRoomContains.Text = settings.CurrentUILanguageProvider.Language.IncursionRoyalMeetingRoomContains;
            textBlockRoyalMeetingRoomModifiers.Text = settings.CurrentUILanguageProvider.Language.IncursionRoyalMeetingRoomModifiers;
            textBlockHallOfLords.Text = settings.CurrentUILanguageProvider.Language.IncursionHallOfLords;
            textBlockThroneOfAtziri.Text = settings.CurrentUILanguageProvider.Language.IncursionThroneOfAtziri;
        }

        private void UpdateBetrayalUIText()
        {
            var settings = SettingsController.GetSettingsInstance();

            textBlockAislingTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalAislingTransportaion;
            textBlockAislingFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalAislingFortification;
            textBlockAislingResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalAislingResearch;
            textBlockAislingResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalAislingResearchTooltip;
            textBlockAislingIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalAislingIntervention;

            textBlockCameriaFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalCameriaFortification;
            textBlockCameriaIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalCameriaIntervention;
            textBlockCameriaResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalCameriaResearch;
            textBlockCameriaTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalCameriaTransportation;
            textBlockCameriaTransportation.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalCameriaTransportationTooltip;

            textBlockElreonFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalElreonFortification;
            textBlockElreonIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalElreonIntervention;
            textBlockElreonResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalElreonResearch;
            textBlockElreonTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalElreonTransportation;

            textBlockGraviciusFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalGraviciusFortification;
            textBlockGraviciusIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalGraviciusIntervention;
            textBlockGraviciusResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalGraviciusResearch;
            textBlockGraviciusTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalGraviciusTransportation;

            textBlockGuffFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalGuffFortification;
            textBlockGuffFortification.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalGuffFortificationTooltip;
            textBlockGuffIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalGuffIntervention;
            textBlockGuffIntervention.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalGuffInterventionTooltip;
            textBlockGuffResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalGuffResearch;
            textBlockGuffResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalGuffResearchTooltip;
            textBlockGuffTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalGuffTransportation;
            textBlockGuffTransportation.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalGuffTransportationTooltip;

            textBlockHakuFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalHakuFortification;
            textBlockHakuIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalHakuIntervention;
            textBlockHakuResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalHakuResearch;
            textBlockHakuTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalHakuTransportation;

            textBlockHillockFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalHillockFortification;
            textBlockHillockFortification.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalHillockFortificationTooltip;
            textBlockHillockIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalHillockIntervention;
            textBlockHillockIntervention.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalHillockInterventionTooltip;
            textBlockHillockResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalHillockResearch;
            textBlockHillockResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalHillockResearchTooltip;
            textBlockHillockTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalHillockTransportation;
            textBlockHillockTransportation.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalHillockTransportationTooltip;

            textBlockItThatFledFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalItThatFledFortification;
            textBlockItThatFledIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalItThatFledIntervention;
            textBlockItThatFledResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalItThatFledResearch;
            textBlockItThatFledResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalItThatFledResearchTooltip;
            textBlockItThatFledTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalItThatFledTransportation;

            textBlockJanusFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalJanusFortification;
            textBlockJanusIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalJanusIntervention;
            textBlockJanusResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalJanusResearch;
            textBlockJanusTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalJanusTransportaion;

            textBlockJorginFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalJorginFortification;
            textBlockJorginIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalJorginIntervention;
            textBlockJorginResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalJorginResearch;
            textBlockJorginResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalJorginResearchTooltip;
            textBlockJorginTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalJorginTransportation;

            textBlockKorrelFortifcation.Text = settings.CurrentUILanguageProvider.Language.BetrayalKorrelFortification;
            textBlockKorrelIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalKorrelIntervention;
            textBlockKorrelResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalKorrellResearch;
            textBlockKorrelTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalKorrellTransportation;

            textBlockLeoFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalLeoFortification;
            textBlockLeoIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalLeoIntervention;
            textBlockLeoResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalLeoResearch;
            textBlockLeoResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalLeoResearchTooltip;
            textBlockLeoTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalLeoTransportation;

            textBlockRikerFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalRikerFortification;
            textBlockRikerIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalRikerIntervention;
            textBlockRikerResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalRikerResearch;
            textBlockRikerTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalRikerTransportation;

            textBlockRinFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalRinFortification;
            textBlockRinIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalRinIntervention;
            textBlockRinResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalRinResearch;
            textBlockRinTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalRinTransportation;

            textBlockToraFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalToraFortification;
            textBlockToraFortification.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalToraFortificationTooltip;
            textBlockToraIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalToraIntervention;
            textBlockToraResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalToraResearch;
            textBlockToraResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalToraResearchTooltip;
            textBlockToraTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalToraTransportation;
            textBlockToraTransportation.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalToraTransportationTooltip;

            textBlockVaganFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalVaganFortification;
            textBlockVaganIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalVaganIntervention;
            textBlockVaganResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalVaganResearch;
            textBlockVaganTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalVaganTransportation;

            textBlockVoriciFortification.Text = settings.CurrentUILanguageProvider.Language.BetrayalVoriciFortification;
            textBlockVoriciIntervention.Text = settings.CurrentUILanguageProvider.Language.BetrayalVoriciIntervention;
            textBlockVoriciResearch.Text = settings.CurrentUILanguageProvider.Language.BetrayalVoriciResearch;
            textBlockVoriciResearch.ToolTip = settings.CurrentUILanguageProvider.Language.BetrayalVoriceResearchTooltip;
            textBlockVoriciTransportation.Text = settings.CurrentUILanguageProvider.Language.BetrayalVoriciTransportation;

            labelLegendHighValue.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendVeryValuable;
            labelLegendGoodValue.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendValuable;
            labelLegendNormalValue.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendLessValuable;
            labelLegendNoValue.Content = settings.CurrentUILanguageProvider.Language.LeagueLegendNotValuable;
        }
    }
}