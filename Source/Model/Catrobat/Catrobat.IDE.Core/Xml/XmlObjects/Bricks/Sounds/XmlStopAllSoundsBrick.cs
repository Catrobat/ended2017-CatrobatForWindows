using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public class XmlStopAllSoundsBrick : XmlBrick
    {
        public XmlStopAllSoundsBrick() {}

        public XmlStopAllSoundsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("stopAllSoundsBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlStopAllSoundsBrick();

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlStopAllSoundsBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}