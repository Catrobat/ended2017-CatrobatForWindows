using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicBeginBrick : XmlBrick
    {
        public XmlFormula IfCondition { get; set; }

        internal XmlIfLogicElseBrickReference IfLogicElseBrickReference { get; set; }

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

        public XmlIfLogicBeginBrick() {}

        public XmlIfLogicBeginBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                IfCondition = new XmlFormula(xRoot, XmlConstants.XmlIFCONDITION);
            }
            /*if (xRoot.Element("ifElseBrick") != null)
            {
                IfLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                IfLogicEndBrickReference = new XmlIfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }*/
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlIfLogicBeginBrick);

            //TODO: Anstelle von einem <ifCondition>-Tag wird in der 093 ein formula element mit category="IF_CONDITION" verwendet
            if (IfCondition != null)
            {
                /*var xVariable1 = new XElement(XmlConstants.XmlIfLogicBeginBrick);
                xVariable1.Add(IfCondition.CreateXml());
                xRoot.Add(xVariable1);*/

                var xElement = IfCondition.CreateXml();
                xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.XmlIFCONDITION);

                var xFormulalist = new XElement(XmlConstants.FormulaList);
                xFormulalist.Add(xElement);

                xRoot.Add(xFormulalist);
            }

                //xRoot.Add(IfLogicElseBrickReference.CreateXml());

                //xRoot.Add(IfLogicEndBrickReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
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