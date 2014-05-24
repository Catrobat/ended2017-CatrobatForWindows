using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
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

        public override XmlObject Copy()
        {
            var newBrick = new XmlForeverLoopEndBrick();

            if (_loopBeginBrickReference != null)
                newBrick.LoopBeginBrickReference = _loopBeginBrickReference.Copy() as XmlLoopBeginBrickReference;

            return newBrick;
        }
    }
}