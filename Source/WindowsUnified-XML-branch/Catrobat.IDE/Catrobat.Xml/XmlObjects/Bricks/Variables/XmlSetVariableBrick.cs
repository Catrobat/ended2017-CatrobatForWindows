using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlSetVariableBrick : XmlBrick
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

        public XmlSetVariableBrick() { }

        public XmlSetVariableBrick(XElement xElement) : base(xElement) { }



        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                VariableFormula = new XmlFormula(xRoot, XmlConstants.Variable);

                if (xRoot.Element(XmlConstants.UserVariable) != null)
                    UserVariableReference = new XmlUserVariableReference(xRoot.Element(XmlConstants.UserVariable));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVariableBrickType);

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            var xElement = VariableFormula.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Variable);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            /*TODO: add     <inUserBrick>false</inUserBrick>
                            <userVariable>random to</userVariable>
             * and delete/change reference
             * here or mabyesomewhere else but for this brick
            */
            return xRoot;
        }

        internal override void LoadReference()
        {
            if(UserVariableReference != null)
                UserVariableReference.LoadReference();
            if(VariableFormula != null)
                VariableFormula.LoadReference();
        }
    }
}