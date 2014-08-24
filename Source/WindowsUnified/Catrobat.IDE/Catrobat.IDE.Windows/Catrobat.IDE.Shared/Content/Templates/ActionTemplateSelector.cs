using Windows.UI.Xaml;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls;

namespace Catrobat.IDE.WindowsPhone.IDE.Content.Templates
{
    public class ActionTemplateSelector : DataTemplateSelector
    {
        //#### Scripts #######################################################################

        public DataTemplate StartScript { get; set; }
        public DataTemplate TappedScript { get; set; }
        public DataTemplate BroadcastReceivedScript { get; set; }

        //#### Bricks #######################################################################

        public DataTemplate BroadcastSendBrick { get; set; }
        public DataTemplate BroadcastSendBlockingBrick { get; set; }
        public DataTemplate ChangeBrightnessBrick { get; set; }
        public DataTemplate ChangeTransparencyBrick { get; set; }
        public DataTemplate ChangeSizeBrick { get; set; }
        public DataTemplate ChangeVolumeBrick { get; set; }
        public DataTemplate ChangePositionXBrick { get; set; }
        public DataTemplate ChangePositionYBrick { get; set; }
        public DataTemplate ResetGraphicPropertiesBrick { get; set; }
        public DataTemplate BringToFrontBrick { get; set; }
        public DataTemplate ForeverBrick { get; set; }
        public DataTemplate AnimatePositionBrick { get; set; }
        public DataTemplate DecreaseZOrderBrick { get; set; }
        public DataTemplate HideBrick { get; set; }
        public DataTemplate BounceBrick { get; set; }
        public DataTemplate EndRepeatBrick { get; set; }
        public DataTemplate EndForeverBrick { get; set; }
        public DataTemplate MoveBrick { get; set; }
        public DataTemplate NextLookBrick { get; set; }
        public DataTemplate CommentBrick { get; set; }
        public DataTemplate SetNxtMotorSpeedBrick { get; set; }
        public DataTemplate StopNxtMotorBrick { get; set; }
        public DataTemplate ChangeNxtMotorAngleBrick { get; set; }
        public DataTemplate PlayNxtToneBrick { get; set; }
        public DataTemplate SetPositionBrick { get; set; }
        public DataTemplate PlaySoundBrick { get; set; }
        public DataTemplate SetRotationBrick { get; set; }
        public DataTemplate LookAtBrick { get; set; }
        public DataTemplate RepeatBrick { get; set; }
        public DataTemplate SetBrightnessBrick { get; set; }
        public DataTemplate SetLookBrick { get; set; }
        public DataTemplate SetTransparencyBrick { get; set; }
        public DataTemplate SetSizeBrick { get; set; }
        public DataTemplate SetVolumeBrick { get; set; }
        public DataTemplate SetPositionXBrick { get; set; }
        public DataTemplate SetPositionYBrick { get; set; }
        public DataTemplate ShowBrick { get; set; }
        public DataTemplate SpeakBrick { get; set; }
        public DataTemplate StopSoundsBrick { get; set; }
        public DataTemplate TurnLeftBrick { get; set; }
        public DataTemplate TurnRightBrick { get; set; }
        public DataTemplate DelayBrick { get; set; }
        public DataTemplate SetVariableBrick { get; set; }
        public DataTemplate ChangeVariableBrick { get; set; }
        public DataTemplate IfBrick { get; set; }
        public DataTemplate ElseBrick { get; set; }
        public DataTemplate EndIfBrick { get; set; }
        public DataTemplate UnknownBrick { get; set; }
        public DataTemplate EmptyDummyBrick { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var scriptBrick = item as Model;
            if (scriptBrick != null)
            {
                // Scripts
                if (scriptBrick is StartScript) return StartScript;
                if (scriptBrick is TappedScript) return TappedScript;
                if (scriptBrick is BroadcastReceivedScript) return BroadcastReceivedScript;

                // Bricks
                if (scriptBrick is BroadcastSendBrick) return BroadcastSendBrick;
                if (scriptBrick is BroadcastSendBlockingBrick) return BroadcastSendBlockingBrick;
                if (scriptBrick is ChangeBrightnessBrick) return ChangeBrightnessBrick;
                if (scriptBrick is ChangeTransparencyBrick) return ChangeTransparencyBrick;
                if (scriptBrick is ChangeSizeBrick) return ChangeSizeBrick;
                if (scriptBrick is ChangeVolumeBrick) return ChangeVolumeBrick;
                if (scriptBrick is ChangePositionXBrick) return ChangePositionXBrick;
                if (scriptBrick is ChangePositionYBrick) return ChangePositionYBrick;
                if (scriptBrick is ResetGraphicPropertiesBrick) return ResetGraphicPropertiesBrick;
                if (scriptBrick is BringToFrontBrick) return BringToFrontBrick;
                if (scriptBrick is ForeverBrick) return ForeverBrick;
                if (scriptBrick is AnimatePositionBrick) return AnimatePositionBrick;
                if (scriptBrick is DecreaseZOrderBrick) return DecreaseZOrderBrick;
                if (scriptBrick is HideBrick) return HideBrick;
                if (scriptBrick is BounceBrick) return BounceBrick;
                if (scriptBrick is EndForeverBrick) return EndForeverBrick;
                if (scriptBrick is EndRepeatBrick) return EndRepeatBrick;
                if (scriptBrick is MoveBrick) return MoveBrick;
                if (scriptBrick is NextLookBrick) return NextLookBrick;
                if (scriptBrick is CommentBrick) return CommentBrick;
                if (scriptBrick is SetNxtMotorSpeedBrick) return SetNxtMotorSpeedBrick;
                if (scriptBrick is StopNxtMotorBrick) return StopNxtMotorBrick;
                if (scriptBrick is ChangeNxtMotorAngleBrick) return ChangeNxtMotorAngleBrick;
                if (scriptBrick is PlayNxtToneBrick) return PlayNxtToneBrick;
                if (scriptBrick is SetPositionBrick) return SetPositionBrick;
                if (scriptBrick is PlaySoundBrick) return PlaySoundBrick;
                if (scriptBrick is SetRotationBrick) return SetRotationBrick;
                if (scriptBrick is LookAtBrick) return LookAtBrick;
                if (scriptBrick is RepeatBrick) return RepeatBrick;
                if (scriptBrick is SetBrightnessBrick) return SetBrightnessBrick;
                if (scriptBrick is SetLookBrick) return SetLookBrick;
                if (scriptBrick is SetTransparencyBrick) return SetTransparencyBrick;
                if (scriptBrick is SetSizeBrick) return SetSizeBrick;
                if (scriptBrick is SetVolumeBrick) return SetVolumeBrick;
                if (scriptBrick is SetPositionXBrick) return SetPositionXBrick;
                if (scriptBrick is SetPositionYBrick) return SetPositionYBrick;
                if (scriptBrick is ShowBrick) return ShowBrick;
                if (scriptBrick is SpeakBrick) return SpeakBrick;
                if (scriptBrick is StopSoundsBrick) return StopSoundsBrick;
                if (scriptBrick is TurnLeftBrick) return TurnLeftBrick;
                if (scriptBrick is TurnRightBrick) return TurnRightBrick;
                if (scriptBrick is DelayBrick) return DelayBrick;
                if (scriptBrick is SetVariableBrick) return SetVariableBrick;
                if (scriptBrick is ChangeVariableBrick) return ChangeVariableBrick;
                if (scriptBrick is IfBrick) return IfBrick;
                if (scriptBrick is ElseBrick) return ElseBrick;
                if (scriptBrick is EndIfBrick) return EndIfBrick;
                if (scriptBrick is EmptyDummyBrick) return EmptyDummyBrick;
                return UnknownBrick;
            }

            return null;
        }
    }
}