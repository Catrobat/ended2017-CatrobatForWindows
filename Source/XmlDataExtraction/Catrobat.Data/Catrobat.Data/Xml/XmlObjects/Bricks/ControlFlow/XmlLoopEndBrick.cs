using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract class XmlLoopEndBrick : XmlBrick
    {
        public XmlLoopBeginBrickReference LoopBeginBrickReference { get; set; }

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

        public abstract override void LoadFromXml(XElement xRoot);

        public abstract override XElement CreateXml();

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

        public override void LoadReference()
        {
            if (LoopBeginBrickReference != null)
                LoopBeginBrickReference.LoadReference();
        }
    }
}

