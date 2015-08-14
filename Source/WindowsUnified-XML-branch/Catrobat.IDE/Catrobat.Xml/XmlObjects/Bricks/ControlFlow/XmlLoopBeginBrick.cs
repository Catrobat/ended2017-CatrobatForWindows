using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract partial class XmlLoopBeginBrick : XmlBrick
    {

        protected XmlLoopBeginBrick() {}

        protected XmlLoopBeginBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXml(XElement xRoot);

        internal abstract override XElement CreateXml();

    }
}