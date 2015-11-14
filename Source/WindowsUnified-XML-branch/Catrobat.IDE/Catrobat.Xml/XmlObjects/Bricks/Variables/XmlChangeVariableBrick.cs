using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlChangeVariableBrick : XmlBrick
    {
        internal XmlUserVariableReference UserVariableReference { get; set; }

        public XmlUserVariable UserVariable
        {
            get
            {
                if (UserVariableReference == null)
                    return null;

                return UserVariableReference.UserVariable;
            }
            set
            {
                if (UserVariableReference == null)
                    UserVariableReference = new XmlUserVariableReference();

                if (UserVariableReference.UserVariable == value)
                    return;

                UserVariableReference.UserVariable = value;

                if (value == null)
                    UserVariableReference = null;
            }
        }

        public XmlFormula VariableFormula { get; set; }

        public XmlChangeVariableBrick() { }

        public XmlChangeVariableBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                //TODO: needs references etc. -done?
                // -- änderungen zwecks änderungen an XmlUserVariableReference allgemein

                if (xRoot != null)
                {
                    VariableFormula = new XmlFormula(xRoot, XmlConstants.VariableChange);

                    if (xRoot.Element(XmlConstants.UserVariable) != null)
                        UserVariableReference = new XmlUserVariableReference(xRoot.Element(XmlConstants.UserVariable));
                }
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeVariableBrickType);

            XmlParserTempProjectHelper.currentBrickNum++;
            XmlParserTempProjectHelper.currentVariableNum = 0;

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            var xElement = VariableFormula.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.VariableChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (UserVariableReference != null)
                UserVariableReference.LoadReference();
            if (VariableFormula != null)
                VariableFormula.LoadReference();
        }
    }
}