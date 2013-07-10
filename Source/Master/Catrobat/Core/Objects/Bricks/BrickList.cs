using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class BrickList : DataObject
    {
        public ObservableCollection<Brick> Bricks { get; private set; }

        private Sprite _sprite;
        public Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                RaisePropertyChanged();
            }
        }


        public BrickList(Sprite parent)
        {
            Bricks = new ObservableCollection<Brick>();
            _sprite = parent;
        }

        public BrickList(XElement xElement, Sprite parent)
        {
            _sprite = parent;
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
                        Bricks.Add(new BroadcastBrick(_sprite));
                        break;

                    case "broadcastWaitBrick":
                        Bricks.Add(new BroadcastWaitBrick(_sprite));
                        break;

                    case "changeBrightnessByNBrick":
                        Bricks.Add(new ChangeBrightnessBrick(_sprite));
                        break;

                    case "changeGhostEffectByNBrick":
                        Bricks.Add(new ChangeGhostEffectBrick(_sprite));
                        break;

                    case "changeSizeByNBrick":
                        Bricks.Add(new ChangeSizeByNBrick(_sprite));
                        break;

                    case "changeVariableBrick":
                        Bricks.Add(new ChangeVariableBrick(_sprite));
                        break;

                    case "changeVolumeByNBrick":
                        Bricks.Add(new ChangeVolumeByBrick(_sprite));
                        break;

                    case "changeXByNBrick":
                        Bricks.Add(new ChangeXByBrick(_sprite));
                        break;

                    case "changeYByNBrick":
                        Bricks.Add(new ChangeYByBrick(_sprite));
                        break;

                    case "clearGraphicEffectBrick":
                        Bricks.Add(new ClearGraphicEffectBrick(_sprite));
                        break;

                    case "comeToFrontBrick":
                        Bricks.Add(new ComeToFrontBrick(_sprite));
                        break;

                    case "foreverBrick":
                        Bricks.Add(new ForeverBrick(_sprite));
                        break;

                    case "glideToBrick":
                        Bricks.Add(new GlideToBrick(_sprite));
                        break;

                    case "goNStepsBackBrick":
                        Bricks.Add(new GoNStepsBackBrick(_sprite));
                        break;

                    case "hideBrick":
                        Bricks.Add(new HideBrick(_sprite));
                        break;

                    case "ifLogicBeginBrick":
                        Bricks.Add(new HideBrick(_sprite));
                        break;

                    case "ifLogicElseBrick":
                        Bricks.Add(new HideBrick(_sprite));
                        break;

                    case "ifLogicEndBrick":
                        Bricks.Add(new HideBrick(_sprite));
                        break;

                    case "ifOnEdgeBounceBrick":
                        Bricks.Add(new IfOnEdgeBounceBrick(_sprite));
                        break;

                    case "legoNxtMotorActionBrick":
                        Bricks.Add(new NxtMotorActionBrick(_sprite));
                        break;

                    case "legoNxtMotorStopBrick":
                        Bricks.Add(new NxtMotorStopBrick(_sprite));
                        break;

                    case "legoNxtMotorTurnAngleBrick":
                        Bricks.Add(new NxtMotorTurnAngleBrick(_sprite));
                        break;

                    case "legoNxtPlayToneBrick":
                        Bricks.Add(new NxtPlayToneBrick(_sprite));
                        break;

                    case "loopEndBrick":
                        Bricks.Add(new LoopEndBrick(_sprite));
                        break;

                    case "moveNStepsBrick":
                        Bricks.Add(new MoveNStepsBrick(_sprite));
                        break;

                    case "nextLookBrick":
                        Bricks.Add(new NextCostumeBrick(_sprite));
                        break;

                    case "noteBrick":
                        Bricks.Add(new NoteBrick(_sprite));
                        break;

                    case "placeAtBrick":
                        Bricks.Add(new PlaceAtBrick(_sprite));
                        break;

                    case "playSoundBrick":
                        Bricks.Add(new PlaySoundBrick(_sprite));
                        break;

                    case "pointInDirectionBrick":
                        Bricks.Add(new PointInDirectionBrick(_sprite));
                        break;

                    case "pointToBrick":
                        Bricks.Add(new PointToBrick(_sprite));
                        break;

                    case "repeatBrick":
                        Bricks.Add(new RepeatBrick(_sprite));
                        break;

                    case "setBrightnessBrick":
                        Bricks.Add(new SetBrightnessBrick(_sprite));
                        break;

                    case "setLookBrick":
                        Bricks.Add(new SetCostumeBrick(_sprite));
                        break;

                    case "setGhostEffectBrick":
                        Bricks.Add(new SetGhostEffectBrick(_sprite));
                        break;

                    case "setSizeToBrick":
                        Bricks.Add(new SetSizeToBrick(_sprite));
                        break;

                    case "setVariableBrick":
                        Bricks.Add(new SetVariableBrick(_sprite));
                        break;

                    case "setVolumeToBrick":
                        Bricks.Add(new SetVolumeToBrick(_sprite));
                        break;

                    case "setXBrick":
                        Bricks.Add(new SetXBrick(_sprite));
                        break;

                    case "setYBrick":
                        Bricks.Add(new SetYBrick(_sprite));
                        break;

                    case "showBrick":
                        Bricks.Add(new ShowBrick(_sprite));
                        break;

                    case "speakBrick":
                        Bricks.Add(new SpeakBrick(_sprite));
                        break;

                    case "stopAllSoundsBrick":
                        Bricks.Add(new StopAllSoundsBrick(_sprite));
                        break;

                    case "turnLeftBrick":
                        Bricks.Add(new TurnLeftBrick(_sprite));
                        break;

                    case "turnRightBrick":
                        Bricks.Add(new TurnRightBrick(_sprite));
                        break;

                    case "waitBrick":
                        Bricks.Add(new WaitBrick(_sprite));
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

        public DataObject Copy(Sprite parent)
        {
            var newBrickList = new BrickList(parent);
            ReadHelper.CurrentBrickList = newBrickList;

            foreach (Brick brick in Bricks)
            {
                newBrickList.Bricks.Add(brick.Copy(parent) as Brick);
            }

            return newBrickList;
        }

        public void CopyReference(BrickList copiedFrom, Sprite parent)
        {
            var pos = 0;
            ReadHelper.CurrentBrickList = this;

            foreach (Brick brick in Bricks)
            {
                if (brick is PointToBrick)
                {
                    var pointToBrick = brick as PointToBrick;
                    pointToBrick.CopyReference(copiedFrom.Bricks[pos] as PointToBrick, parent);
                }
                else if (brick is LoopBeginBrick)
                {
                    var loopBeginBrick = brick as LoopBeginBrick;
                    loopBeginBrick.CopyReference(copiedFrom.Bricks[pos] as LoopBeginBrick, parent);
                }
                else if (brick is LoopEndBrick)
                {
                    var loopEndBrick = brick as LoopEndBrick;
                    loopEndBrick.CopyReference(copiedFrom.Bricks[pos] as LoopEndBrick, parent);
                }
                pos++;
            }
        }
    }
}