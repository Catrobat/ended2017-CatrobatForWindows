using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

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
            //if (xRoot.Element("userVariable") != null)
            //    UserVariableReference = new XmlUserVariableReference(xRoot.Element("userVariable"));
            if (xRoot.Element(XmlConstants.UserVariable) != null)
                UserVariableReference = new XmlUserVariableReference(xRoot.Element(XmlConstants.UserVariable));

            //if (xRoot.Element("variableFormula") != null)
            //    VariableFormula = new XmlFormula(xRoot.Element("variableFormula"));
            if (xRoot.Element(XmlConstants.VariableChange) != null)
                VariableFormula = new XmlFormula(xRoot.Element(XmlConstants.VariableChange));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("changeVariableBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "changeVariableBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeVariableBrickType);

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            //var xFormula = new XElement("variableFormula");
            var xFormula = new XElement(XmlConstants.VariableChange);
            xFormula.Add(VariableFormula.CreateXml());
            xRoot.Add(xFormula);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (UserVariableReference != null)
                UserVariableReference.LoadReference();
            if (VariableFormula != null)
                VariableFormula.LoadReference();
        }
    }
}