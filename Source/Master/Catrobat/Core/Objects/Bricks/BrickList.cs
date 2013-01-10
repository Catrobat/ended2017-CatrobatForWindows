using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class BrickList : DataObject
    {
        private Sprite sprite;

        public BrickList(Sprite parent)
        {
            Bricks = new ObservableCollection<Brick>();
            sprite = parent;
        }

        public BrickList(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<Brick> Bricks { get; set; }

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if (sprite == value)
                    return;

                sprite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            ReadHelper.currentBrickList = this;

            Bricks = new ObservableCollection<Brick>();
            foreach (XElement element in xRoot.Elements())
                switch (element.Name.LocalName)
                {
                    case "broadcastBrick":
                        Bricks.Add(new BroadcastBrick(sprite));
                        break;

                    case "broadcastWaitBrick":
                        Bricks.Add(new BroadcastWaitBrick(sprite));
                        break;

                    case "changeBrightnessBrick":
                        Bricks.Add(new ChangeBrightnessBrick(sprite));
                        break;

                    case "changeGhostEffectBrick":
                        Bricks.Add(new ChangeGhostEffectBrick(sprite));
                        break;

                    case "changeSizeByNBrick":
                        Bricks.Add(new ChangeSizeByNBrick(sprite));
                        break;

                    case "changeVolumeByBrick":
                        Bricks.Add(new ChangeVolumeByBrick(sprite));
                        break;

                    case "changeXByBrick":
                        Bricks.Add(new ChangeXByBrick(sprite));
                        break;

                    case "changeYByBrick":
                        Bricks.Add(new ChangeYByBrick(sprite));
                        break;

                    case "clearGraphicEffectBrick":
                        Bricks.Add(new ClearGraphicEffectBrick(sprite));
                        break;

                    case "comeToFrontBrick":
                        Bricks.Add(new ComeToFrontBrick(sprite));
                        break;

                    case "foreverBrick":
                        Bricks.Add(new ForeverBrick(sprite));
                        break;

                    case "glideToBrick":
                        Bricks.Add(new GlideToBrick(sprite));
                        break;

                    case "goNStepsBackBrick":
                        Bricks.Add(new GoNStepsBackBrick(sprite));
                        break;

                    case "hideBrick":
                        Bricks.Add(new HideBrick(sprite));
                        break;

                    case "ifOnEdgeBounceBrick":
                        Bricks.Add(new IfOnEdgeBounceBrick(sprite));
                        break;

                    case "loopEndBrick":
                        Bricks.Add(new LoopEndBrick(sprite));
                        break;

                    case "moveNStepsBrick":
                        Bricks.Add(new MoveNStepsBrick(sprite));
                        break;

                    case "nextCostumeBrick":
                        Bricks.Add(new NextCostumeBrick(sprite));
                        break;

                    case "noteBrick":
                        Bricks.Add(new NoteBrick(sprite));
                        break;

                    case "nxtMotorActionBrick":
                        Bricks.Add(new NxtMotorActionBrick(sprite));
                        break;

                    case "nxtMotorStopBrick":
                        Bricks.Add(new NxtMotorStopBrick(sprite));
                        break;

                    case "nxtMotorTurnAngleBrick":
                        Bricks.Add(new NxtMotorTurnAngleBrick(sprite));
                        break;

                    case "nxtPlayToneBrick":
                        Bricks.Add(new NxtPlayToneBrick(sprite));
                        break;

                    case "placeAtBrick":
                        Bricks.Add(new PlaceAtBrick(sprite));
                        break;

                    case "playSoundBrick":
                        Bricks.Add(new PlaySoundBrick(sprite));
                        break;

                    case "pointInDirectionBrick":
                        Bricks.Add(new PointInDirectionBrick(sprite));
                        break;

                    case "pointToBrick":
                        Bricks.Add(new PointToBrick(sprite));
                        break;

                    case "repeatBrick":
                        Bricks.Add(new RepeatBrick(sprite));
                        break;

                    case "setBrightnessBrick":
                        Bricks.Add(new SetBrightnessBrick(sprite));
                        break;

                    case "setCostumeBrick":
                        Bricks.Add(new SetCostumeBrick(sprite));
                        break;

                    case "setGhostEffectBrick":
                        Bricks.Add(new SetGhostEffectBrick(sprite));
                        break;

                    case "setSizeToBrick":
                        Bricks.Add(new SetSizeToBrick(sprite));
                        break;

                    case "setVolumeToBrick":
                        Bricks.Add(new SetVolumeToBrick(sprite));
                        break;

                    case "setXBrick":
                        Bricks.Add(new SetXBrick(sprite));
                        break;

                    case "setYBrick":
                        Bricks.Add(new SetYBrick(sprite));
                        break;

                    case "showBrick":
                        Bricks.Add(new ShowBrick(sprite));
                        break;

                    case "speakBrick":
                        Bricks.Add(new SpeakBrick(sprite));
                        break;

                    case "stopAllSoundsBrick":
                        Bricks.Add(new StopAllSoundsBrick(sprite));
                        break;

                    case "turnLeftBrick":
                        Bricks.Add(new TurnLeftBrick(sprite));
                        break;

                    case "turnRightBrick":
                        Bricks.Add(new TurnRightBrick(sprite));
                        break;

                    case "waitBrick":
                        Bricks.Add(new WaitBrick(sprite));
                        break;

                    default:
                        // Unknown brick
                        break;
                }

            IEnumerator<Brick> enumerator = Bricks.GetEnumerator();
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
                xRoot.Add(brick.CreateXML());

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newBrickList = new BrickList(parent);
            ReadHelper.currentBrickList = newBrickList;

            foreach (Brick brick in Bricks)
                newBrickList.Bricks.Add(brick.Copy(parent) as Brick);

            return newBrickList;
        }

        public void CopyReference(BrickList copiedFrom, Sprite parent)
        {
            int pos = 0;
            ReadHelper.currentBrickList = this;

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