using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlChangeVariableBrick : XmlBrick
    {
        public XmlUserVariable UserVariable { get; set; }

        public XmlFormula VariableFormula { get; set; }

        public XmlChangeVariableBrick() { }

        public XmlChangeVariableBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {

            if (xRoot != null)
            {
                VariableFormula = new XmlFormula(xRoot, XmlConstants.VariableChange);

                if (xRoot.Element(XmlConstants.UserVariable) != null)
                    UserVariable = new XmlUserVariable(xRoot.Element(XmlConstants.UserVariable));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeVariableBrickType);

            var xElement = VariableFormula.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.VariableChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            xRoot.Add(UserVariable.CreateXml());

            return xRoot;
        }
    }
}