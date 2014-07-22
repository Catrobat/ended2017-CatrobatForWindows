using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
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
            var xRoot = new XElement("loopEndBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}