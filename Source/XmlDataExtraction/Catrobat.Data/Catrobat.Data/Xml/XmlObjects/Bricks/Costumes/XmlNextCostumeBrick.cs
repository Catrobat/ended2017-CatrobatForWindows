using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Costumes
{
    public partial class XmlNextCostumeBrick : XmlBrick
    {
        public XmlNextCostumeBrick() {}

        public XmlNextCostumeBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot) {}

        public override XElement CreateXml()
        {
            var xRoot = new XElement("nextLookBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}