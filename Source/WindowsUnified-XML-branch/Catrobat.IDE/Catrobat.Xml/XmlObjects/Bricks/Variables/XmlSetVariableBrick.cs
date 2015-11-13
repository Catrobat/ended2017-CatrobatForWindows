using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlSetVariableBrick : XmlBrick
    {
        public XmlUserVariable UserVariable { get; set; }

        public XmlFormula VariableFormula { get; set; }

        public XmlSetVariableBrick() { }

        public XmlSetVariableBrick(XElement xElement) : base(xElement) { }



        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                VariableFormula = new XmlFormula(xRoot, XmlConstants.Variable);

                if (xRoot.Element(XmlConstants.UserVariable) != null)
                    UserVariable = new XmlUserVariable(xRoot.Element(XmlConstants.UserVariable));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVariableBrickType);

            var xElement = VariableFormula.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Variable);



            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            xRoot.Add(UserVariable.CreateXml());

            return xRoot;
        }

    }
}