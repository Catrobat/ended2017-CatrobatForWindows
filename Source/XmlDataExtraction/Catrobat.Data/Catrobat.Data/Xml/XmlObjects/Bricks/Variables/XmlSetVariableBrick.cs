using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;
using Catrobat.Data.Xml.XmlObjects.Variables;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlSetVariableBrick : XmlBrick
    {
        public XmlUserVariableReference UserVariableReference { get; set; }

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



        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("userVariable") != null)
                UserVariableReference = new XmlUserVariableReference(xRoot.Element("userVariable"));

            if (xRoot.Element("variableFormula") != null)
                VariableFormula = new XmlFormula(xRoot.Element("variableFormula"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("setVariableBrick");

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            var xVariable2 = new XElement("variableFormula");
            xVariable2.Add(VariableFormula.CreateXml());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        public override void LoadReference()
        {
            if(UserVariableReference != null)
                UserVariableReference.LoadReference();
            if(VariableFormula != null)
                VariableFormula.LoadReference();
        }
    }
}