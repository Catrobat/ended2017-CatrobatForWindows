using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;
using Catrobat.Data.Xml.XmlObjects.Variables;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Variables
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
            if (xRoot.Element("userVariable") != null)
                UserVariableReference = new XmlUserVariableReference(xRoot.Element("userVariable"));

            if (xRoot.Element("variableFormula") != null)
                VariableFormula = new XmlFormula(xRoot.Element("variableFormula"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeVariableBrick");

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            var xFormula = new XElement("variableFormula");
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