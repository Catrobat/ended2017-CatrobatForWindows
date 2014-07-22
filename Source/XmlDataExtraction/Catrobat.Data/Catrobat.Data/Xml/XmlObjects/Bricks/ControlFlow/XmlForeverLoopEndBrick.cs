using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlForeverLoopEndBrick : XmlLoopEndBrick
    {
        public XmlForeverLoopEndBrick() {}

        public XmlForeverLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("loopEndlessBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}