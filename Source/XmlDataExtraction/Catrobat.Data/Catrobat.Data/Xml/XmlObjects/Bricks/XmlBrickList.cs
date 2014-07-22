using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.Data.Xml.XmlObjects.Bricks.Costumes;
using Catrobat.Data.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.Data.Xml.XmlObjects.Bricks.Properties;
using Catrobat.Data.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.Data.Xml.XmlObjects.Bricks.Variables;

namespace Catrobat.Data.Xml.XmlObjects.Bricks
{
    public class XmlBrickList : XmlObject
    {
        public List<XmlBrick> Bricks { get; set; }

        public XmlBrickList()
        {
            Bricks = new List<XmlBrick>();
        }

        public XmlBrickList(XElement xElement)
        {
            Bricks = new List<XmlBrick>();
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            foreach (XElement element in xRoot.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "broadcastBrick":
                        Bricks.Add(new XmlBroadcastBrick());
                        break;

                    case "broadcastWaitBrick":
                        Bricks.Add(new XmlBroadcastWaitBrick());
                        break;

                    case "changeBrightnessByNBrick":
                        Bricks.Add(new XmlChangeBrightnessBrick());
                        break;

                    case "changeGhostEffectByNBrick":
                        Bricks.Add(new XmlChangeGhostEffectBrick());
                        break;

                    case "changeSizeByNBrick":
                        Bricks.Add(new XmlChangeSizeByNBrick());
                        break;

                    case "changeVariableBrick":
                        Bricks.Add(new XmlChangeVariableBrick());
                        break;

                    case "changeVolumeByNBrick":
                        Bricks.Add(new XmlChangeVolumeByBrick());
                        break;

                    case "changeXByNBrick":
                        Bricks.Add(new XmlChangeXByBrick());
                        break;

                    case "changeYByNBrick":
                        Bricks.Add(new XmlChangeYByBrick());
                        break;

                    case "clearGraphicEffectBrick":
                        Bricks.Add(new XmlClearGraphicEffectBrick());
                        break;

                    case "comeToFrontBrick":
                        Bricks.Add(new XmlComeToFrontBrick());
                        break;

                    case "foreverBrick":
                        Bricks.Add(new XmlForeverBrick());
                        break;

                    case "loopEndlessBrick":
                        Bricks.Add(new XmlForeverLoopEndBrick());
                        break;

                    case "glideToBrick":
                        Bricks.Add(new XmlGlideToBrick());
                        break;

                    case "goNStepsBackBrick":
                        Bricks.Add(new XmlGoNStepsBackBrick());
                        break;

                    case "hideBrick":
                        Bricks.Add(new XmlHideBrick());
                        break;

                    case "ifLogicBeginBrick":
                        Bricks.Add(new XmlIfLogicBeginBrick());
                        break;

                    case "ifLogicElseBrick":
                        Bricks.Add(new XmlIfLogicElseBrick());
                        break;

                    case "ifLogicEndBrick":
                        Bricks.Add(new XmlIfLogicEndBrick());
                        break;

                    case "ifOnEdgeBounceBrick":
                        Bricks.Add(new XmlIfOnEdgeBounceBrick());
                        break;

                    case "legoNxtMotorActionBrick":
                        Bricks.Add(new XmlNxtMotorActionBrick());
                        break;

                    case "legoNxtMotorStopBrick":
                        Bricks.Add(new XmlNxtMotorStopBrick());
                        break;

                    case "legoNxtMotorTurnAngleBrick":
                        Bricks.Add(new XmlNxtMotorTurnAngleBrick());
                        break;

                    case "legoNxtPlayToneBrick":
                        Bricks.Add(new XmlNxtPlayToneBrick());
                        break;

                    case "moveNStepsBrick":
                        Bricks.Add(new XmlMoveNStepsBrick());
                        break;

                    case "nextLookBrick":
                        Bricks.Add(new XmlNextCostumeBrick());
                        break;

                    case "noteBrick":
                        Bricks.Add(new XmlNoteBrick());
                        break;

                    case "placeAtBrick":
                        Bricks.Add(new XmlPlaceAtBrick());
                        break;

                    case "playSoundBrick":
                        Bricks.Add(new XmlPlaySoundBrick());
                        break;

                    case "pointInDirectionBrick":
                        Bricks.Add(new XmlPointInDirectionBrick());
                        break;

                    case "pointToBrick":
                        Bricks.Add(new XmlPointToBrick());
                        break;

                    case "repeatBrick":
                        Bricks.Add(new XmlRepeatBrick());
                        break;

                    case "loopEndBrick":
                        Bricks.Add(new XmlRepeatLoopEndBrick());
                        break;

                    case "setBrightnessBrick":
                        Bricks.Add(new XmlSetBrightnessBrick());
                        break;

                    case "setLookBrick":
                        Bricks.Add(new XmlSetCostumeBrick());
                        break;

                    case "setGhostEffectBrick":
                        Bricks.Add(new XmlSetGhostEffectBrick());
                        break;

                    case "setSizeToBrick":
                        Bricks.Add(new XmlSetSizeToBrick());
                        break;

                    case "setVariableBrick":
                        Bricks.Add(new XmlSetVariableBrick());
                        break;

                    case "setVolumeToBrick":
                        Bricks.Add(new XmlSetVolumeToBrick());
                        break;

                    case "setXBrick":
                        Bricks.Add(new XmlSetXBrick());
                        break;

                    case "setYBrick":
                        Bricks.Add(new XmlSetYBrick());
                        break;

                    case "showBrick":
                        Bricks.Add(new XmlShowBrick());
                        break;

                    case "speakBrick":
                        Bricks.Add(new XmlSpeakBrick());
                        break;

                    case "stopAllSoundsBrick":
                        Bricks.Add(new XmlStopAllSoundsBrick());
                        break;

                    case "turnLeftBrick":
                        Bricks.Add(new XmlTurnLeftBrick());
                        break;

                    case "turnRightBrick":
                        Bricks.Add(new XmlTurnRightBrick());
                        break;

                    case "waitBrick":
                        Bricks.Add(new XmlWaitBrick());
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
                enumerator.Current.LoadFromXml(element);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("brickList");

            foreach (XmlBrick brick in Bricks)
            {
                xRoot.Add(brick.CreateXml());
            }

            return xRoot;
        }
    }
}