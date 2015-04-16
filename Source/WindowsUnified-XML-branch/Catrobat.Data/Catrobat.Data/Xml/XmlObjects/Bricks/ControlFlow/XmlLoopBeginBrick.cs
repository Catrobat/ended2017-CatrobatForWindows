using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract partial class XmlLoopBeginBrick : XmlBrick
    {
        public XmlLoopEndBrickReference LoopEndBrickReference { get; set; }

        public XmlLoopEndBrick LoopEndBrick
        {
            get { return LoopEndBrickReference.LoopEndBrick; }
            set
            {
                if (LoopEndBrickReference == null)
                {
                    LoopEndBrickReference = new XmlLoopEndBrickReference();
                    //if (value is XmlRepeatLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndBrick";
                    //else if (value is XmlForeverLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndlessBrick";
                }

                if (LoopEndBrickReference == null)
                    LoopEndBrickReference = new XmlLoopEndBrickReference();

                if (LoopEndBrickReference.LoopEndBrick == value)
                    return;

                LoopEndBrickReference.LoopEndBrick = value;

                if (value == null)
                    LoopEndBrickReference = null;
            }
        }

        protected XmlLoopBeginBrick() {}

        protected XmlLoopBeginBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXml(XElement xRoot);

        internal abstract override XElement CreateXml();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopEndBrick") != null)
            {
                LoopEndBrickReference = new XmlLoopEndBrickReference(xRoot.Element("loopEndBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(LoopEndBrickReference.CreateXml());
        }

        internal override void LoadReference()
        {
            if (LoopEndBrickReference != null)
                LoopEndBrickReference.LoadReference();
        }
    }
}