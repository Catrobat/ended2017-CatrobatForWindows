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
            var xRoot = new XElement("foreverBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlForeverBrick();

            if(_loopEndBrickReference != null)
                newBrick.LoopEndBrickReference = _loopEndBrickReference.Copy() as XmlLoopEndBrickReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlForeverBrick;

            if (otherBrick == null)
                return false;

            return LoopEndBrickReference.Equals(otherBrick.LoopEndBrickReference);
        }
    }
}