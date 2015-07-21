using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlForeverBrick : XmlLoopBeginBrick
    {
        public XmlForeverBrick() {}

        public XmlForeverBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("oreverBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            //var xRoot = new XElement("brick");
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlForeverBrickType);
            //xRoot.SetAttributeValue("type", "ForeverBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}