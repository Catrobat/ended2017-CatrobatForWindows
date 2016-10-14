using System.Xml.Linq;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlStopAllSoundsBrick : XmlBrick, IStopSoundsBrick
    {
        public XmlStopAllSoundsBrick() { }

        public XmlStopAllSoundsBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot) { }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlStopAllSoundsBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
