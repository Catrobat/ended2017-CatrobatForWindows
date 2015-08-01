using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlMoveNStepsBrick : XmlBrick
    {
        public XmlFormula Steps { get; set; }

        public XmlMoveNStepsBrick() {}

        public XmlMoveNStepsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
            //@michael funktioniert an sich aber meine vermutung von gestern, dass dann new XmlFormula nimmer pfeifen
            //könnt weils ned a formula element mit category sondern ws. noch an altes xelement des xposition heißt
            //und drunter an alten formulatree erwartet scheint sich als richtig herrauszustellen
            foreach (XElement xElement in elements)
            {
                if (xElement.Attribute(XmlConstants.Category).Value == XmlConstants.Steps)
                    Steps = new XmlFormula(xElement);

            }
            //Steps = new XmlFormula(xRoot.Element(XmlConstants.Steps));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlMoveNStepsBrickType);

            var xElement = Steps.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Steps);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}