using System.Windows;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Phone.Controls.DynamicDataTemplates;

namespace Catrobat.IDE.Phone.Views.Editor.Scripts
{
    public class ScriptBrickContentTemplateSelector : DataTemplateSelector
    {
        //#### Scripts #######################################################################

        public DataTemplate StartScript { get; set; }

        public DataTemplate WhenScript { get; set; }

        public DataTemplate BroadcastScript { get; set; }

        //#### Bricks #######################################################################

        public DataTemplate BroadcastBrick { get; set; }

        public DataTemplate BroadcastReceiverBrick { get; set; }

        public DataTemplate BroadcastWaitBrick { get; set; }

        public DataTemplate ChangeBrightnessBrick { get; set; }

        public DataTemplate ChangeGhostEffectBrick { get; set; }

        public DataTemplate ChangeSizeByNBrick { get; set; }

        public DataTemplate ChangeVolumeByBrick { get; set; }

        public DataTemplate ChangeXByBrick { get; set; }

        public DataTemplate ChangeYByBrick { get; set; }

        public DataTemplate ClearGraphicEffectBrick { get; set; }

        public DataTemplate ComeToFrontBrick { get; set; }

        public DataTemplate ForeverBrick { get; set; }

        public DataTemplate GlideToBrick { get; set; }

        public DataTemplate GoNStepsBackBrick { get; set; }

        public DataTemplate HideBrick { get; set; }

        public DataTemplate IfOnEdgeBounceBrick { get; set; }

        public DataTemplate RepeatLoopEndBrick { get; set; }

        public DataTemplate ForeverLoopEndBrick { get; set; }

        public DataTemplate MoveNStepsBrick { get; set; }

        public DataTemplate NextCostumeBrick { get; set; }

        public DataTemplate NoteBrick { get; set; }

        public DataTemplate NxtMotorActionBrick { get; set; }

        public DataTemplate NxtMotorStopBrick { get; set; }

        public DataTemplate NxtMotorTurnAngleBrick { get; set; }

        public DataTemplate NxtPlayToneBrick { get; set; }

        public DataTemplate PlaceAtBrick { get; set; }

        public DataTemplate PlaySoundBrick { get; set; }

        public DataTemplate PointInDirectionBrick { get; set; }

        public DataTemplate PointToBrick { get; set; }

        public DataTemplate RepeatBrick { get; set; }

        public DataTemplate SetBrightnessBrick { get; set; }

        public DataTemplate SetCostumeBrick { get; set; }

        public DataTemplate SetGhostEffectBrick { get; set; }

        public DataTemplate SetSizeToBrick { get; set; }

        public DataTemplate SetVolumeToBrick { get; set; }

        public DataTemplate SetXBrick { get; set; }

        public DataTemplate SetYBrick { get; set; }

        public DataTemplate ShowBrick { get; set; }

        public DataTemplate SpeakBrick { get; set; }

        public DataTemplate StopAllSoundsBrick { get; set; }

        public DataTemplate TurnLeftBrick { get; set; }

        public DataTemplate TurnRightBrick { get; set; }

        public DataTemplate WaitBrick { get; set; }

        public DataTemplate SetVariableBrick { get; set; }

        public DataTemplate ChangeVariableBrick { get; set; }

        public DataTemplate IfLogicBeginBrick { get; set; }

        public DataTemplate IfLogicElseBrick { get; set; }

        public DataTemplate IfLogicEndBrick { get; set; }

        public DataTemplate UnknownBrick { get; set; }

        public DataTemplate EmptyDummyBrick { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var scriptBrick = item as XmlObject;
            if (scriptBrick != null)
            {
                // Scripts

                if (scriptBrick is XmlStartScript)
                {
                    return StartScript;
                }

                if (scriptBrick is XmlWhenScript)
                {
                    return WhenScript;
                }

                if (scriptBrick is XmlBroadcastScript)
                {
                    return BroadcastScript;
                }

                // Bricks
                if (scriptBrick is XmlBroadcastBrick)
                {
                    return BroadcastBrick;
                }

                if (scriptBrick is XmlBroadcastWaitBrick)
                {
                    return BroadcastWaitBrick;
                }

                if (scriptBrick is XmlChangeBrightnessBrick)
                {
                    return ChangeBrightnessBrick;
                }

                if (scriptBrick is XmlChangeGhostEffectBrick)
                {
                    return ChangeGhostEffectBrick;
                }

                if (scriptBrick is XmlChangeSizeByNBrick)
                {
                    return ChangeSizeByNBrick;
                }

                if (scriptBrick is XmlChangeVolumeByBrick)
                {
                    return ChangeVolumeByBrick;
                }

                if (scriptBrick is XmlChangeXByBrick)
                {
                    return ChangeXByBrick;
                }

                if (scriptBrick is XmlChangeYByBrick)
                {
                    return ChangeYByBrick;
                }

                if (scriptBrick is XmlClearGraphicEffectBrick)
                {
                    return ClearGraphicEffectBrick;
                }

                if (scriptBrick is XmlComeToFrontBrick)
                {
                    return ComeToFrontBrick;
                }

                if (scriptBrick is XmlForeverBrick)
                {
                    return ForeverBrick;
                }

                if (scriptBrick is XmlGlideToBrick)
                {
                    return GlideToBrick;
                }

                if (scriptBrick is XmlGoNStepsBackBrick)
                {
                    return GoNStepsBackBrick;
                }

                if (scriptBrick is XmlHideBrick)
                {
                    return HideBrick;
                }

                if (scriptBrick is XmlIfOnEdgeBounceBrick)
                {
                    return IfOnEdgeBounceBrick;
                }

                if (scriptBrick is XmlForeverLoopEndBrick)
                {
                    return ForeverLoopEndBrick;
                }

                if (scriptBrick is XmlRepeatLoopEndBrick)
                {
                    return RepeatLoopEndBrick;
                }

                if (scriptBrick is XmlMoveNStepsBrick)
                {
                    return MoveNStepsBrick;
                }

                if (scriptBrick is XmlNextCostumeBrick)
                {
                    return NextCostumeBrick;
                }

                if (scriptBrick is XmlNoteBrick)
                {
                    return NoteBrick;
                }

                if (scriptBrick is XmlNxtMotorActionBrick)
                {
                    return NxtMotorActionBrick;
                }

                if (scriptBrick is XmlNxtMotorStopBrick)
                {
                    return NxtMotorStopBrick;
                }

                if (scriptBrick is XmlNxtMotorTurnAngleBrick)
                {
                    return NxtMotorTurnAngleBrick;
                }

                if (scriptBrick is XmlNxtPlayToneBrick)
                {
                    return NxtPlayToneBrick;
                }

                if (scriptBrick is XmlPlaceAtBrick)
                {
                    return PlaceAtBrick;
                }

                if (scriptBrick is XmlPlaySoundBrick)
                {
                    return PlaySoundBrick;
                }

                if (scriptBrick is XmlPointInDirectionBrick)
                {
                    return PointInDirectionBrick;
                }

                if (scriptBrick is XmlPointToBrick)
                {
                    return PointToBrick;
                }

                if (scriptBrick is XmlRepeatBrick)
                {
                    return RepeatBrick;
                }

                if (scriptBrick is XmlSetBrightnessBrick)
                {
                    return SetBrightnessBrick;
                }

                if (scriptBrick is XmlSetCostumeBrick)
                {
                    return SetCostumeBrick;
                }

                if (scriptBrick is XmlSetGhostEffectBrick)
                {
                    return SetGhostEffectBrick;
                }

                if (scriptBrick is XmlSetSizeToBrick)
                {
                    return SetSizeToBrick;
                }

                if (scriptBrick is XmlSetVolumeToBrick)
                {
                    return SetVolumeToBrick;
                }

                if (scriptBrick is XmlSetXBrick)
                {
                    return SetXBrick;
                }

                if (scriptBrick is XmlSetYBrick)
                {
                    return SetYBrick;
                }

                if (scriptBrick is XmlShowBrick)
                {
                    return ShowBrick;
                }

                if (scriptBrick is XmlSpeakBrick)
                {
                    return SpeakBrick;
                }

                if (scriptBrick is XmlStopAllSoundsBrick)
                {
                    return StopAllSoundsBrick;
                }

                if (scriptBrick is XmlTurnLeftBrick)
                {
                    return TurnLeftBrick;
                }

                if (scriptBrick is XmlTurnRightBrick)
                {
                    return TurnRightBrick;
                }

                if (scriptBrick is XmlWaitBrick)
                {
                    return WaitBrick;
                }

                if (scriptBrick is XmlSetVariableBrick)
                    return SetVariableBrick;

                if (scriptBrick is XmlChangeVariableBrick)
                    return ChangeVariableBrick;

                if (scriptBrick is XmlIfLogicBeginBrick)
                    return IfLogicBeginBrick;

                if (scriptBrick is XmlIfLogicElseBrick)
                    return IfLogicElseBrick;

                if (scriptBrick is XmlIfLogicEndBrick)
                    return IfLogicEndBrick;

                if (scriptBrick is EmptyDummyBrick)
                    return EmptyDummyBrick;

                return UnknownBrick;
            }

            return base.SelectTemplate(item, container);
        }
    }
}