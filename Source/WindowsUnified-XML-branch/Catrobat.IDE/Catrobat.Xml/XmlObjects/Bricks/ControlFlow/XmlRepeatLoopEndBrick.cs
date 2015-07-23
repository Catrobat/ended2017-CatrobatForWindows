using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlRepeatLoopEndBrick : XmlLoopEndBrick
    {
        public XmlRepeatLoopEndBrick() {}

        public XmlRepeatLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Brick, XmlConstants.XmlRepeatLoopEndBrickType);
            base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}