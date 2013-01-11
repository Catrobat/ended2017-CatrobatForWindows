using System.Windows;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDEWindowsPhone7.Controls.DynamicDataTemplates;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Scripts
{
  public class ScriptBrickContentTemplateSelector : DataTemplateSelector
  {

    //#### Scripts #######################################################################

    public DataTemplate StartScript
    {
      get;
      set;
    }

    public DataTemplate WhenScript
    {
      get;
      set;
    }

    public DataTemplate BroadcastScript
    {
      get;
      set;
    }

    //#### Bricks #######################################################################

    public DataTemplate BroadcastBrick
    {
      get;
      set;
    }

    public DataTemplate BroadcastReceiverBrick
    {
      get;
      set;
    }

    public DataTemplate BroadcastWaitBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeBrightnessBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeGhostEffectBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeSizeByNBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeVolumeByBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeXByBrick
    {
      get;
      set;
    }

    public DataTemplate ChangeYByBrick
    {
      get;
      set;
    }

    public DataTemplate ClearGraphicEffectBrick
    {
      get;
      set;
    }

    public DataTemplate ComeToFrontBrick
    {
      get;
      set;
    }

    public DataTemplate ForeverBrick
    {
      get;
      set;
    }

    public DataTemplate GlideToBrick
    {
      get;
      set;
    }

    public DataTemplate GoNStepsBackBrick
    {
      get;
      set;
    }

    public DataTemplate HideBrick
    {
      get;
      set;
    }

    public DataTemplate IfOnEdgeBounceBrick
    {
      get;
      set;
    }

    public DataTemplate LoopEndBrick
    {
      get;
      set;
    }

    public DataTemplate MoveNStepsBrick
    {
      get;
      set;
    }

    public DataTemplate NextCostumeBrick
    {
      get;
      set;
    }

    public DataTemplate NoteBrick
    {
      get;
      set;
    }

    public DataTemplate NxtMotorActionBrick
    {
      get;
      set;
    }

    public DataTemplate NxtMotorStopBrick
    {
      get;
      set;
    }

    public DataTemplate NxtMotorTurnAngleBrick
    {
      get;
      set;
    }

    public DataTemplate NxtPlayToneBrick
    {
      get;
      set;
    }

    public DataTemplate PlaceAtBrick
    {
      get;
      set;
    }

    public DataTemplate PlaySoundBrick
    {
      get;
      set;
    }

    public DataTemplate PointInDirectionBrick
    {
      get;
      set;
    }

    public DataTemplate PointToBrick
    {
      get;
      set;
    }

    public DataTemplate RepeatBrick
    {
      get;
      set;
    }

    public DataTemplate SetBrightnessBrick
    {
      get;
      set;
    }

    public DataTemplate SetCostumeBrick
    {
      get;
      set;
    }

    public DataTemplate SetGhostEffectBrick
    {
      get;
      set;
    }

    public DataTemplate SetSizeToBrick
    {
      get;
      set;
    }

    public DataTemplate SetVolumeToBrick
    {
      get;
      set;
    }

    public DataTemplate SetXBrick
    {
      get;
      set;
    }

    public DataTemplate SetYBrick
    {
      get;
      set;
    }

    public DataTemplate ShowBrick
    {
      get;
      set;
    }

    public DataTemplate SpeakBrick
    {
      get;
      set;
    }

    public DataTemplate StopAllSoundsBrick
    {
      get;
      set;
    }

    public DataTemplate TurnLeftBrick
    {
      get;
      set;
    }

    public DataTemplate TurnRightBrick
    {
      get;
      set;
    }

    public DataTemplate WaitBrick
    {
      get;
      set;
    }

    public DataTemplate UnknownBrick
    {
      get;
      set;
    }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      DataObject scriptBrick = item as DataObject;
      if (scriptBrick != null)
      {
        // Scripts

        if (scriptBrick is StartScript)
          return StartScript;

        if (scriptBrick is WhenScript)
          return WhenScript;

        if (scriptBrick is BroadcastScript)
          return BroadcastScript;

        // Bricks
        if (scriptBrick is BroadcastBrick)
          return BroadcastBrick;

        if (scriptBrick is BroadcastWaitBrick)
          return BroadcastWaitBrick;

        if (scriptBrick is ChangeBrightnessBrick)
          return ChangeBrightnessBrick;

        if (scriptBrick is ChangeGhostEffectBrick)
          return ChangeGhostEffectBrick;

        if (scriptBrick is ChangeSizeByNBrick)
          return ChangeSizeByNBrick;

        if (scriptBrick is ChangeVolumeByBrick)
          return ChangeVolumeByBrick;

        if (scriptBrick is ChangeXByBrick)
          return ChangeXByBrick;

        if (scriptBrick is ChangeYByBrick)
          return ChangeYByBrick;

        if (scriptBrick is ClearGraphicEffectBrick)
          return ClearGraphicEffectBrick;

        if (scriptBrick is ComeToFrontBrick)
          return ComeToFrontBrick;

        if (scriptBrick is ForeverBrick)
          return ForeverBrick;

        if (scriptBrick is GlideToBrick)
          return GlideToBrick;

        if (scriptBrick is GoNStepsBackBrick)
          return GoNStepsBackBrick;

        if (scriptBrick is HideBrick)
          return HideBrick;

        if (scriptBrick is IfOnEdgeBounceBrick)
          return IfOnEdgeBounceBrick;

        if (scriptBrick is LoopEndBrick)
          return LoopEndBrick;

        if (scriptBrick is MoveNStepsBrick)
          return MoveNStepsBrick;

        if (scriptBrick is NextCostumeBrick)
          return NextCostumeBrick;

        if (scriptBrick is NoteBrick)
          return NoteBrick;

        if (scriptBrick is NxtMotorActionBrick)
          return NxtMotorActionBrick;

        if (scriptBrick is NxtMotorStopBrick)
          return NxtMotorStopBrick;

        if (scriptBrick is NxtMotorTurnAngleBrick)
          return NxtMotorTurnAngleBrick;

        if (scriptBrick is NxtPlayToneBrick)
          return NxtPlayToneBrick;

        if (scriptBrick is PlaceAtBrick)
          return PlaceAtBrick;

        if (scriptBrick is PlaySoundBrick)
          return PlaySoundBrick;

        if (scriptBrick is PointInDirectionBrick)
          return PointInDirectionBrick;

        if (scriptBrick is PointToBrick)
          return PointToBrick;

        if (scriptBrick is RepeatBrick)
          return RepeatBrick;

        if (scriptBrick is SetBrightnessBrick)
          return SetBrightnessBrick;

        if (scriptBrick is SetCostumeBrick)
          return SetCostumeBrick;

        if (scriptBrick is SetGhostEffectBrick)
          return SetGhostEffectBrick;

        if (scriptBrick is SetSizeToBrick)
          return SetSizeToBrick;

        if (scriptBrick is SetVolumeToBrick)
          return SetVolumeToBrick;

        if (scriptBrick is SetXBrick)
          return SetXBrick;

        if (scriptBrick is SetYBrick)
          return SetYBrick;

        if (scriptBrick is ShowBrick)
          return ShowBrick;

        if (scriptBrick is SpeakBrick)
          return SpeakBrick;

        if (scriptBrick is StopAllSoundsBrick)
          return StopAllSoundsBrick;

        if (scriptBrick is TurnLeftBrick)
          return TurnLeftBrick;

        if (scriptBrick is TurnRightBrick)
          return TurnRightBrick;

        if (scriptBrick is WaitBrick)
          return WaitBrick;

        return UnknownBrick;
      }

      return base.SelectTemplate(item, container);
    }
  }
}
