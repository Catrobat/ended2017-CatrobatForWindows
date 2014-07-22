using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicBeginBrick : XmlBrick
    {
        public XmlFormula IfCondition { get; set; }

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

        public XmlIfLogicEndBrickReference IfLogicEndBrickReference { get; set; }

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

        public XmlIfLogicBeginBrick() {}

        public XmlIfLogicBeginBrick(XElement xElement) : base(xElement) { }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifCondition") != null)
            {
                IfCondition = new XmlFormula(xRoot.Element("ifCondition"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                IfLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                IfLogicEndBrickReference = new XmlIfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicBeginBrick");

            if (IfCondition != null)
            {
                var xVariable1 = new XElement("ifCondition");
                xVariable1.Add(IfCondition.CreateXml());
                xRoot.Add(xVariable1);
            }

                xRoot.Add(IfLogicElseBrickReference.CreateXml());

                xRoot.Add(IfLogicEndBrickReference.CreateXml());

            return xRoot;
        }

        public override void LoadReference()
        {
            if (IfLogicElseBrickReference != null)
                IfLogicElseBrickReference.LoadReference();
            if (IfLogicEndBrickReference != null)
                IfLogicEndBrickReference.LoadReference();
            if (IfCondition != null)
                IfCondition.LoadReference();

        }
    }
}