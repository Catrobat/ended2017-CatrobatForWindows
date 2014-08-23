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
            Steps = new XmlFormula(xRoot.Element("steps"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("moveNStepsBrick");

            var xVariable = new XElement("steps");
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