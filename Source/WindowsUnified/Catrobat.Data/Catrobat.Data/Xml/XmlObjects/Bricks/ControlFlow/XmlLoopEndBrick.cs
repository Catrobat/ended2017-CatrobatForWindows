using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract class XmlLoopEndBrick : XmlBrick
    {
        protected internal XmlLoopBeginBrickReference LoopBeginBrickReference { get; set; }

        public XmlLoopBeginBrick LoopBeginBrick
        {
            get { return LoopBeginBrickReference.LoopBeginBrick; }
            set
            {
                if (LoopBeginBrickReference == null)
                {
                    LoopBeginBrickReference = new XmlLoopBeginBrickReference();
                }

                if (LoopBeginBrickReference.LoopBeginBrick == value)
                    return;

                LoopBeginBrickReference.LoopBeginBrick = value;

                if (value == null)
                    LoopBeginBrickReference = null;
            }
        }

        protected XmlLoopEndBrick() {}

        protected XmlLoopEndBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXml(XElement xRoot);

        internal abstract override XElement CreateXml();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                LoopBeginBrickReference = new XmlLoopBeginBrickReference(xRoot.Element("loopBeginBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(LoopBeginBrickReference.CreateXml());
        }

        internal override void LoadReference()
        {
            if (LoopBeginBrickReference != null)
                LoopBeginBrickReference.LoadReference();
        }
    }
}

