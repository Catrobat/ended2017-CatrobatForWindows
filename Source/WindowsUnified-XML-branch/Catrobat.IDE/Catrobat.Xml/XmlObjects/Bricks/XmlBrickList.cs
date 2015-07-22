using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    public class XmlBrickList : XmlObjectNode
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
                //switch (element.Name.LocalName)
                switch(element.Attribute(XmlConstants.Type).Value.ToString())
                {
                    case XmlConstants.XmlBroadcastBrickType://"broadcastBrick":
                        Bricks.Add(new XmlBroadcastBrick());
                        break;

                    case XmlConstants.XmlBroadcastWaitBrickType://"broadcastWaitBrick":
                        Bricks.Add(new XmlBroadcastWaitBrick());
                        break;

                    case XmlConstants.XmlChangeBrightnessBrickType://"changeBrightnessByNBrick":
                        Bricks.Add(new XmlChangeBrightnessBrick());
                        break;

                    case XmlConstants.XmlChangeGhostEffectBrickType://"changeGhostEffectByNBrick":
                        Bricks.Add(new XmlChangeGhostEffectBrick());
                        break;

                    case XmlConstants.XmlChangeSizeByNBrickType://"changeSizeByNBrick":
                        Bricks.Add(new XmlChangeSizeByNBrick());
                        break;

                    case XmlConstants.XmlChangeVariableBrickType://"changeVariableBrick":
                        Bricks.Add(new XmlChangeVariableBrick());
                        break;

                    case XmlConstants.XmlChangeVolumeByBricksType://"changeVolumeByNBrick":
                        Bricks.Add(new XmlChangeVolumeByBrick());
                        break;

                    case XmlConstants.XmlChangeXByBrickType://"changeXByNBrick":
                        Bricks.Add(new XmlChangeXByBrick());
                        break;

                    case XmlConstants.XmlChangeYByBrickType://"changeYByNBrick":
                        Bricks.Add(new XmlChangeYByBrick());
                        break;

                    case XmlConstants.XmlClearGraphicEffectBrickType://"clearGraphicEffectBrick":
                        Bricks.Add(new XmlClearGraphicEffectBrick());
                        break;

                    case XmlConstants.XmlComeToFrontBrickType://"comeToFrontBrick":
                        Bricks.Add(new XmlComeToFrontBrick());
                        break;

                    case XmlConstants.XmlForeverBrickType://"foreverBrick":
                        Bricks.Add(new XmlForeverBrick());
                        break;

                    case XmlConstants.XmlLoopEndlessBrickType://"loopEndlessBrick":
                        Bricks.Add(new XmlForeverLoopEndBrick());
                        break;

                    case XmlConstants.XmlGlideToBrickType://"glideToBrick":
                        Bricks.Add(new XmlGlideToBrick());
                        break;

                    case XmlConstants.XmlGoNStepsBackBrickType://"goNStepsBackBrick":
                        Bricks.Add(new XmlGoNStepsBackBrick());
                        break;

                    case XmlConstants.XmlHideBrickType://"hideBrick":
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

                    case XmlConstants.XmlMoveNStepsBrickType://"moveNStepsBrick":
                        Bricks.Add(new XmlMoveNStepsBrick());
                        break;

                    case XmlConstants.XmlNextLookBrickType://"nextLookBrick":
                        Bricks.Add(new XmlNextLookBrick());
                        break;

                    case XmlConstants.XmlNoteBrickType://"noteBrick":
                        Bricks.Add(new XmlNoteBrick());
                        break;

                    case XmlConstants.XmlPlaceAtBrickType://"placeAtBrick":
                        Bricks.Add(new XmlPlaceAtBrick());
                        break;

                    case XmlConstants.XmlPlaySoundBrickType://"playSoundBrick":
                        Bricks.Add(new XmlPlaySoundBrick());
                        break;

                    case XmlConstants.XmlPointInDirectionBrickType://"pointInDirectionBrick":
                        Bricks.Add(new XmlPointInDirectionBrick());
                        break;

                    case XmlConstants.XmlPointToBrickType://"pointToBrick":
                        Bricks.Add(new XmlPointToBrick());
                        break;

                    case XmlConstants.XmlRepeatBrickType://"repeatBrick":
                        Bricks.Add(new XmlRepeatBrick());
                        break;

                    case XmlConstants.XmlRepeatLoopEndBrickType://"loopEndBrick":
                        Bricks.Add(new XmlRepeatLoopEndBrick());
                        break;

                    case XmlConstants.XmlSetBrightnessBrickType://"setBrightnessBrick":
                        Bricks.Add(new XmlSetBrightnessBrick());
                        break;

                    case XmlConstants.XmlSetLookBrickType://"setLookBrick":
                        Bricks.Add(new XmlSetLookBrick());
                        break;

                    case XmlConstants.XmlSetGhostEffectBrickType://"setGhostEffectBrick":
                        Bricks.Add(new XmlSetGhostEffectBrick());
                        break;

                    case XmlConstants.XmlSetSizeToBrickType://"setSizeToBrick":
                        Bricks.Add(new XmlSetSizeToBrick());
                        break;

                    case XmlConstants.XmlSetVariableBrickType://"setVariableBrick":
                        Bricks.Add(new XmlSetVariableBrick());
                        break;

                    case XmlConstants.XmlSetVolumeToBrickType://"setVolumeToBrick":
                        Bricks.Add(new XmlSetVolumeToBrick());
                        break;

                    case XmlConstants.XmlSetXBrickType://"setXBrick":
                        Bricks.Add(new XmlSetXBrick());
                        break;

                    case XmlConstants.XmlSetYBrickType://"setYBrick":
                        Bricks.Add(new XmlSetYBrick());
                        break;

                    case XmlConstants.XmlShowBrickType://"showBrick":
                        Bricks.Add(new XmlShowBrick());
                        break;

                    case XmlConstants.XmlSpeakBrickType://"speakBrick":
                        Bricks.Add(new XmlSpeakBrick());
                        break;

                    case XmlConstants.XmlStopAllSoundsBrickType://"stopAllSoundsBrick":
                        Bricks.Add(new XmlStopAllSoundsBrick());
                        break;

                    case XmlConstants.XmlTurnLeftBrickType://"turnLeftBrick":
                        Bricks.Add(new XmlTurnLeftBrick());
                        break;

                    case XmlConstants.XmlTurnRightBrickType://"turnRightBrick":
                        Bricks.Add(new XmlTurnRightBrick());
                        break;

                    case XmlConstants.XmlWaitBrickType://"waitBrick":
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
            //var xRoot = new XElement("brickList");
            var xRoot = new XElement(XmlConstants.BrickList);

            foreach (XmlBrick brick in Bricks)
            {
                xRoot.Add(brick.CreateXml());
            }

            return xRoot;
        }
    }
}