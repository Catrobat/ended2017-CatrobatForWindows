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
            //if (xRoot.Element("userVariable") != null)
            //    UserVariableReference = new XmlUserVariableReference(xRoot.Element("userVariable"));
            if (xRoot.Element(XmlConstants.UserVariable) != null)
                UserVariableReference = new XmlUserVariableReference(xRoot.Element(XmlConstants.UserVariable));

            //if (xRoot.Element("variableFormula") != null)
            
            //    VariableFormula = new XmlFormula(xRoot.Element("variableFormula"));
            // sollte wenn fertig wieder in ein new XmlFormula umgebaut werden!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (xRoot.Element("formulaList") != null)
            {
                var formulaList = xRoot.Element("formulaList");
                if (formulaList.Element("formula") != null)
                {
                    //toDo: Wird das zu einer Liste, oder anders lösen?
                    VariableFormula = new XmlFormula(xRoot.Element("formula"));
                }
            }
            // bis hier her!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setVariableBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setVariableBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVariableBrickType);

            if(UserVariableReference != null)
                xRoot.Add(UserVariableReference.CreateXml());

            //var xVariable2 = new XElement("variableFormula");
            //xVariable2.Add(VariableFormula.CreateXml());
            //xRoot.Add(xVariable2);
            //Eigentliche funktion wurde auskommentiert
            //xRoot.Add(VariableFormula.CreateXml("variableForumla"));
            //xRoot.Add(VariableFormula.CreateXml(XmlConstants.Variable));
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