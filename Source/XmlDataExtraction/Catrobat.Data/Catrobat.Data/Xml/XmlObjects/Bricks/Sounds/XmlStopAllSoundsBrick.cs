using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlStopAllSoundsBrick : XmlBrick
    {
        public XmlStopAllSoundsBrick() {}

        public XmlStopAllSoundsBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot) {}

        public override XElement CreateXml()
        {
            var xRoot = new XElement("stopAllSoundsBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}