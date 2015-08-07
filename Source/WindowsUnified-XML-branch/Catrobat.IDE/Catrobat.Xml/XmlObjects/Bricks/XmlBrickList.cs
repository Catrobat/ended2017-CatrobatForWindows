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
                switch(element.Attribute(XmlConstants.Type).Value.ToString())
                {
                    case XmlConstants.XmlBroadcastBrickType:
                        Bricks.Add(new XmlBroadcastBrick());
                        break;

                    case XmlConstants.XmlBroadcastWaitBrickType:
                        Bricks.Add(new XmlBroadcastWaitBrick());
                        break;

                    case XmlConstants.XmlChangeBrightnessBrickType:
                        Bricks.Add(new XmlChangeBrightnessBrick());
                        break;

                    case XmlConstants.XmlChangeGhostEffectBrickType:
                        Bricks.Add(new XmlChangeGhostEffectBrick());
                        break;

                    case XmlConstants.XmlChangeSizeByNBrickType:
                        Bricks.Add(new XmlChangeSizeByNBrick());
                        break;

                    case XmlConstants.XmlChangeVariableBrickType:
                        Bricks.Add(new XmlChangeVariableBrick());
                        break;

                    case XmlConstants.XmlChangeVolumeByBricksType:
                        Bricks.Add(new XmlChangeVolumeByBrick());
                        break;

                    case XmlConstants.XmlChangeXByBrickType:
                        Bricks.Add(new XmlChangeXByBrick());
                        break;

                    case XmlConstants.XmlChangeYByBrickType:
                        Bricks.Add(new XmlChangeYByBrick());
                        break;

                    case XmlConstants.XmlClearGraphicEffectBrickType:
                        Bricks.Add(new XmlClearGraphicEffectBrick());
                        break;

                    case XmlConstants.XmlComeToFrontBrickType:
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

                    case XmlConstants.XmlIfLogicBeginBrick:
                        Bricks.Add(new XmlIfLogicBeginBrick());
                        break;

                    case XmlConstants.XmlIfLogicElseBrick:
                        Bricks.Add(new XmlIfLogicElseBrick());
                        break;

                    case XmlConstants.XmlIfLogicEndBrick:
                        Bricks.Add(new XmlIfLogicEndBrick());
                        break;

                    case XmlConstants.XmlIfOnEdgeBounceBrickType:
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

                    case XmlConstants.XmlMoveNStepsBrickType:
                        Bricks.Add(new XmlMoveNStepsBrick());
                        break;

                    case XmlConstants.XmlNextLookBrickType:
                        Bricks.Add(new XmlNextLookBrick());
                        break;

                    case XmlConstants.XmlNoteBrickType:
                        Bricks.Add(new XmlNoteBrick());
                        break;

                    case XmlConstants.XmlPlaceAtBrickType:
                        Bricks.Add(new XmlPlaceAtBrick());
                        break;

                    case XmlConstants.XmlPlaySoundBrickType:
                        Bricks.Add(new XmlPlaySoundBrick());
                        break;

                    case XmlConstants.XmlPointInDirectionBrickType:
                        Bricks.Add(new XmlPointInDirectionBrick());
                        break;

                    case XmlConstants.XmlPointToBrickType:
                        Bricks.Add(new XmlPointToBrick());
                        break;

                    case XmlConstants.XmlRepeatBrickType:
                        Bricks.Add(new XmlRepeatBrick());
                        break;

                    case XmlConstants.XmlRepeatLoopEndBrickType:
                        Bricks.Add(new XmlRepeatLoopEndBrick());
                        break;

                    case XmlConstants.XmlSetBrightnessBrickType:
                        Bricks.Add(new XmlSetBrightnessBrick());
                        break;

                    case XmlConstants.XmlSetLookBrickType:
                        Bricks.Add(new XmlSetLookBrick());
                        break;

                    case XmlConstants.XmlSetGhostEffectBrickType:
                        Bricks.Add(new XmlSetGhostEffectBrick());
                        break;

                    case XmlConstants.XmlSetSizeToBrickType:
                        Bricks.Add(new XmlSetSizeToBrick());
                        break;

                    case XmlConstants.XmlSetVariableBrickType:
                        Bricks.Add(new XmlSetVariableBrick());
                        break;

                    case XmlConstants.XmlSetVolumeToBrickType:
                        Bricks.Add(new XmlSetVolumeToBrick());
                        break;

                    case XmlConstants.XmlSetXBrickType:
                        Bricks.Add(new XmlSetXBrick());
                        break;

                    case XmlConstants.XmlSetYBrickType:
                        Bricks.Add(new XmlSetYBrick());
                        break;

                    case XmlConstants.XmlShowBrickType:
                        Bricks.Add(new XmlShowBrick());
                        break;

                    case XmlConstants.XmlSpeakBrickType:
                        Bricks.Add(new XmlSpeakBrick());
                        break;

                    case XmlConstants.XmlStopAllSoundsBrickType:
                        Bricks.Add(new XmlStopAllSoundsBrick());
                        break;

                    case XmlConstants.XmlTurnLeftBrickType:
                        Bricks.Add(new XmlTurnLeftBrick());
                        break;

                    case XmlConstants.XmlTurnRightBrickType:
                        Bricks.Add(new XmlTurnRightBrick());
                        break;

                    case XmlConstants.XmlWaitBrickType:
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
            var xRoot = new XElement(XmlConstants.BrickList);

            foreach (XmlBrick brick in Bricks)
            {
                xRoot.Add(brick.CreateXml());
            }

            return xRoot;
        }
    }
}