using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlMoveNStepsBrick : XmlBrick
    {
        public XmlFormula Steps { get; set; }

        public XmlMoveNStepsBrick() {}

        public XmlMoveNStepsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Steps = new XmlFormula(xRoot.Element("steps"));
            Steps = new XmlFormula(xRoot.Element(XmlConstants.Steps));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("moveNStepsBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "moveNStepsBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlMoveNStepsBrickType);

            //var xVariable = new XElement("steps");
            var xVariable = new XElement(XmlConstants.Steps);
            xVariable.Add(Steps.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}