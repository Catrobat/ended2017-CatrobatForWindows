using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicEndBrick : XmlBrick
    {
        public XmlIfLogicBeginBrickReference IfLogicBeginBrickReference { get; set; }

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

        public XmlIfLogicElseBrickReference IfLogicElseBrickReference { get; set; }

        public XmlIfLogicElseBrick IfLogicElseBrick
        {
            get
            {
                if (IfLogicElseBrickReference == null)
                    return null;

                return IfLogicElseBrickReference.IfLogicElseBrick;
            }
            set
            {
                if (IfLogicElseBrickReference == null)
                    IfLogicElseBrickReference = new XmlIfLogicElseBrickReference();

                if (IfLogicElseBrickReference.IfLogicElseBrick == value)
                    return;

                IfLogicElseBrickReference.IfLogicElseBrick = value;

                if (value == null)
                    IfLogicElseBrickReference = null;
            }
        }

        public XmlIfLogicEndBrick() {}

        public XmlIfLogicEndBrick(XElement xElement) : base(xElement) { }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                IfLogicBeginBrickReference = new XmlIfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                IfLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicEndBrick");

                xRoot.Add(IfLogicBeginBrickReference.CreateXml());

                xRoot.Add(IfLogicElseBrickReference.CreateXml());

            return xRoot;
        }

        public override void LoadReference()
        {
            if (IfLogicBeginBrickReference != null)
                IfLogicBeginBrickReference.LoadReference();
            if(IfLogicElseBrickReference != null)
                IfLogicElseBrickReference.LoadReference();
        }
    }
}