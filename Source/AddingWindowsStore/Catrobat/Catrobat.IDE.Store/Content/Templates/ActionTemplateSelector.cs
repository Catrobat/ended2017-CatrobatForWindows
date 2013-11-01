using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;

namespace Catrobat.IDE.Store.Content.Templates
{
    public class ActionTemplateSelector : DataTemplateSelector
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

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var action = item as DataObject;
            if (action != null)
            {
                // Scripts

                if (action is StartScript)
                {
                    return StartScript;
                }

                if (action is WhenScript)
                {
                    return WhenScript;
                }

                if (action is BroadcastScript)
                {
                    return BroadcastScript;
                }

                // Bricks
                if (action is BroadcastBrick)
                {
                    return BroadcastBrick;
                }

                if (action is BroadcastWaitBrick)
                {
                    return BroadcastWaitBrick;
                }

                if (action is ChangeBrightnessBrick)
                {
                    return ChangeBrightnessBrick;
                }

                if (action is ChangeGhostEffectBrick)
                {
                    return ChangeGhostEffectBrick;
                }

                if (action is ChangeSizeByNBrick)
                {
                    return ChangeSizeByNBrick;
                }

                if (action is ChangeVolumeByBrick)
                {
                    return ChangeVolumeByBrick;
                }

                if (action is ChangeXByBrick)
                {
                    return ChangeXByBrick;
                }

                if (action is ChangeYByBrick)
                {
                    return ChangeYByBrick;
                }

                if (action is ClearGraphicEffectBrick)
                {
                    return ClearGraphicEffectBrick;
                }

                if (action is ComeToFrontBrick)
                {
                    return ComeToFrontBrick;
                }

                if (action is ForeverBrick)
                {
                    return ForeverBrick;
                }

                if (action is GlideToBrick)
                {
                    return GlideToBrick;
                }

                if (action is GoNStepsBackBrick)
                {
                    return GoNStepsBackBrick;
                }

                if (action is HideBrick)
                {
                    return HideBrick;
                }

                if (action is IfOnEdgeBounceBrick)
                {
                    return IfOnEdgeBounceBrick;
                }

                if (action is ForeverLoopEndBrick)
                {
                    return ForeverLoopEndBrick;
                }

                if (action is RepeatLoopEndBrick)
                {
                    return RepeatLoopEndBrick;
                }

                if (action is MoveNStepsBrick)
                {
                    return MoveNStepsBrick;
                }

                if (action is NextCostumeBrick)
                {
                    return NextCostumeBrick;
                }

                if (action is NoteBrick)
                {
                    return NoteBrick;
                }

                if (action is NxtMotorActionBrick)
                {
                    return NxtMotorActionBrick;
                }

                if (action is NxtMotorStopBrick)
                {
                    return NxtMotorStopBrick;
                }

                if (action is NxtMotorTurnAngleBrick)
                {
                    return NxtMotorTurnAngleBrick;
                }

                if (action is NxtPlayToneBrick)
                {
                    return NxtPlayToneBrick;
                }

                if (action is PlaceAtBrick)
                {
                    return PlaceAtBrick;
                }

                if (action is PlaySoundBrick)
                {
                    return PlaySoundBrick;
                }

                if (action is PointInDirectionBrick)
                {
                    return PointInDirectionBrick;
                }

                if (action is PointToBrick)
                {
                    return PointToBrick;
                }

                if (action is RepeatBrick)
                {
                    return RepeatBrick;
                }

                if (action is SetBrightnessBrick)
                {
                    return SetBrightnessBrick;
                }

                if (action is SetCostumeBrick)
                {
                    return SetCostumeBrick;
                }

                if (action is SetGhostEffectBrick)
                {
                    return SetGhostEffectBrick;
                }

                if (action is SetSizeToBrick)
                {
                    return SetSizeToBrick;
                }

                if (action is SetVolumeToBrick)
                {
                    return SetVolumeToBrick;
                }

                if (action is SetXBrick)
                {
                    return SetXBrick;
                }

                if (action is SetYBrick)
                {
                    return SetYBrick;
                }

                if (action is ShowBrick)
                {
                    return ShowBrick;
                }

                if (action is SpeakBrick)
                {
                    return SpeakBrick;
                }

                if (action is StopAllSoundsBrick)
                {
                    return StopAllSoundsBrick;
                }

                if (action is TurnLeftBrick)
                {
                    return TurnLeftBrick;
                }

                if (action is TurnRightBrick)
                {
                    return TurnRightBrick;
                }

                if (action is WaitBrick)
                {
                    return WaitBrick;
                }

                if (action is SetVariableBrick)
                    return SetVariableBrick;

                if (action is ChangeVariableBrick)
                    return ChangeVariableBrick;

                if (action is IfLogicBeginBrick)
                    return IfLogicBeginBrick;

                if (action is IfLogicElseBrick)
                    return IfLogicElseBrick;

                if (action is IfLogicEndBrick)
                    return IfLogicEndBrick;

                if (action is EmptyDummyBrick)
                    return EmptyDummyBrick;

                return UnknownBrick;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
