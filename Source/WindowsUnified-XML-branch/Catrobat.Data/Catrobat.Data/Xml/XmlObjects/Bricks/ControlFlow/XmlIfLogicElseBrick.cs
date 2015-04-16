using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicElseBrick : XmlBrick
    {
        internal XmlIfLogicBeginBrickReference IfLogicBeginBrickReference { get; set; }

        public XmlIfLogicBeginBrick IfLogicBeginBrick
        {
            get
            {
                if (IfLogicBeginBrickReference == null)
                    return null;

                return IfLogicBeginBrickReference.IfLogicBeginBrick;
            }
            set
            {
                if (IfLogicBeginBrickReference == null)
                    IfLogicBeginBrickReference = new XmlIfLogicBeginBrickReference();

                if (IfLogicBeginBrickReference.IfLogicBeginBrick == value)
                    return;

                IfLogicBeginBrickReference.IfLogicBeginBrick = value;

                if (value == null)
                    IfLogicBeginBrickReference = null;
            }
        }

        internal XmlIfLogicEndBrickReference IfLogicEndBrickReference { get; set; }

        public XmlIfLogicEndBrick IfLogicEndBrick
        {
            get
            {
                if (IfLogicEndBrickReference == null)
                    return null;

                return IfLogicEndBrickReference.IfLogicEndBrick;
            }
            set
            {
                if (IfLogicEndBrickReference == null)
                    IfLogicEndBrickReference = new XmlIfLogicEndBrickReference();

                if (IfLogicEndBrickReference.IfLogicEndBrick == value)
                    return;

                IfLogicEndBrickReference.IfLogicEndBrick = value;

                if (value == null)
                    IfLogicEndBrickReference = null;
            }
        }

        public XmlIfLogicElseBrick() {}

        public XmlIfLogicElseBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                IfLogicBeginBrickReference = new XmlIfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                IfLogicEndBrickReference = new XmlIfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicElseBrick");

                xRoot.Add(IfLogicBeginBrickReference.CreateXml());

                xRoot.Add(IfLogicEndBrickReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (IfLogicBeginBrickReference != null)
                IfLogicBeginBrickReference.LoadReference();
            if (IfLogicEndBrickReference != null)
                IfLogicEndBrickReference.LoadReference();
        }
    }
}