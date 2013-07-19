using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class BrickList : DataObject
    {
        public ObservableCollection<Brick> Bricks { get; private set; }

        public BrickList()
        {
            Bricks = new ObservableCollection<Brick>();
        }

        public BrickList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            ReadHelper.CurrentBrickList = this;

            Bricks = new ObservableCollection<Brick>();
            foreach (XElement element in xRoot.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "broadcastBrick":
                        Bricks.Add(new BroadcastBrick());
                        break;

                    case "broadcastWaitBrick":
                        Bricks.Add(new BroadcastWaitBrick());
                        break;

                    case "changeBrightnessByNBrick":
                        Bricks.Add(new ChangeBrightnessBrick());
                        break;

                    case "changeGhostEffectByNBrick":
                        Bricks.Add(new ChangeGhostEffectBrick());
                        break;

                    case "changeSizeByNBrick":
                        Bricks.Add(new ChangeSizeByNBrick());
                        break;

                    case "changeVariableBrick":
                        Bricks.Add(new ChangeVariableBrick());
                        break;

                    case "changeVolumeByNBrick":
                        Bricks.Add(new ChangeVolumeByBrick());
                        break;

                    case "changeXByNBrick":
                        Bricks.Add(new ChangeXByBrick());
                        break;

                    case "changeYByNBrick":
                        Bricks.Add(new ChangeYByBrick());
                        break;

                    case "clearGraphicEffectBrick":
                        Bricks.Add(new ClearGraphicEffectBrick());
                        break;

                    case "comeToFrontBrick":
                        Bricks.Add(new ComeToFrontBrick());
                        break;

                    case "foreverBrick":
                        Bricks.Add(new ForeverBrick());
                        break;

                    case "glideToBrick":
                        Bricks.Add(new GlideToBrick());
                        break;

                    case "goNStepsBackBrick":
                        Bricks.Add(new GoNStepsBackBrick());
                        break;

                    case "hideBrick":
                        Bricks.Add(new HideBrick());
                        break;

                    case "ifLogicBeginBrick":
                        Bricks.Add(new HideBrick());
                        break;

                    case "ifLogicElseBrick":
                        Bricks.Add(new HideBrick());
                        break;

                    case "ifLogicEndBrick":
                        Bricks.Add(new HideBrick());
                        break;

                    case "ifOnEdgeBounceBrick":
                        Bricks.Add(new IfOnEdgeBounceBrick());
                        break;

                    case "legoNxtMotorActionBrick":
                        Bricks.Add(new NxtMotorActionBrick());
                        break;

                    case "legoNxtMotorStopBrick":
                        Bricks.Add(new NxtMotorStopBrick());
                        break;

                    case "legoNxtMotorTurnAngleBrick":
                        Bricks.Add(new NxtMotorTurnAngleBrick());
                        break;

                    case "legoNxtPlayToneBrick":
                        Bricks.Add(new NxtPlayToneBrick());
                        break;

                    case "loopEndBrick":
                        Bricks.Add(new LoopEndBrick());
                        break;

                    case "moveNStepsBrick":
                        Bricks.Add(new MoveNStepsBrick());
                        break;

                    case "nextLookBrick":
                        Bricks.Add(new NextCostumeBrick());
                        break;

                    case "noteBrick":
                        Bricks.Add(new NoteBrick());
                        break;

                    case "placeAtBrick":
                        Bricks.Add(new PlaceAtBrick());
                        break;

                    case "playSoundBrick":
                        Bricks.Add(new PlaySoundBrick());
                        break;

                    case "pointInDirectionBrick":
                        Bricks.Add(new PointInDirectionBrick());
                        break;

                    case "pointToBrick":
                        Bricks.Add(new PointToBrick());
                        break;

                    case "repeatBrick":
                        Bricks.Add(new RepeatBrick());
                        break;

                    case "setBrightnessBrick":
                        Bricks.Add(new SetBrightnessBrick());
                        break;

                    case "setLookBrick":
                        Bricks.Add(new SetCostumeBrick());
                        break;

                    case "setGhostEffectBrick":
                        Bricks.Add(new SetGhostEffectBrick());
                        break;

                    case "setSizeToBrick":
                        Bricks.Add(new SetSizeToBrick());
                        break;

                    case "setVariableBrick":
                        Bricks.Add(new SetVariableBrick());
                        break;

                    case "setVolumeToBrick":
                        Bricks.Add(new SetVolumeToBrick());
                        break;

                    case "setXBrick":
                        Bricks.Add(new SetXBrick());
                        break;

                    case "setYBrick":
                        Bricks.Add(new SetYBrick());
                        break;

                    case "showBrick":
                        Bricks.Add(new ShowBrick());
                        break;

                    case "speakBrick":
                        Bricks.Add(new SpeakBrick());
                        break;

                    case "stopAllSoundsBrick":
                        Bricks.Add(new StopAllSoundsBrick());
                        break;

                    case "turnLeftBrick":
                        Bricks.Add(new TurnLeftBrick());
                        break;

                    case "turnRightBrick":
                        Bricks.Add(new TurnRightBrick());
                        break;

                    case "waitBrick":
                        Bricks.Add(new WaitBrick());
                        break;

                    default:
                        // Unknown brick
                        break;
                }
            }

            var enumerator = Bricks.GetEnumerator();
            foreach (XElement element in xRoot.Elements())
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXML(element);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("brickList");

            foreach (Brick brick in Bricks)
            {
                xRoot.Add(brick.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newBrickList = new BrickList();
            ReadHelper.CurrentBrickList = newBrickList;

            foreach (Brick brick in Bricks)
            {
                newBrickList.Bricks.Add(brick.Copy() as Brick);
            }

            return newBrickList;
        }

        public void CopyReference(BrickList copiedFrom)
        {
            var pos = 0;
            ReadHelper.CurrentBrickList = this;

            foreach (Brick brick in Bricks)
            {
                if (brick is PointToBrick)
                {
                    var pointToBrick = brick as PointToBrick;
                    pointToBrick.CopyReference(copiedFrom.Bricks[pos] as PointToBrick);
                }
                else if (brick is LoopBeginBrick)
                {
                    var loopBeginBrick = brick as LoopBeginBrick;
                    loopBeginBrick.CopyReference(copiedFrom.Bricks[pos] as LoopBeginBrick);
                }
                else if (brick is LoopEndBrick)
                {
                    var loopEndBrick = brick as LoopEndBrick;
                    loopEndBrick.CopyReference(copiedFrom.Bricks[pos] as LoopEndBrick);
                }
                pos++;
            }
        }
    }
}