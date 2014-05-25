using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlShowBrick : XmlBrick
    {
        public XmlShowBrick() {}

        public XmlShowBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("showBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}