using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    public partial class XmlChangeVariableBrick : XmlBrick
    {
        private XmlUserVariableReference _userVariableReference;
        internal XmlUserVariableReference UserVariableReference
        {
            get { return _userVariableReference; }
            set
            {
                if (_userVariableReference == value)
                    return;

                _userVariableReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => UserVariable);
            }
        }

        public XmlUserVariable UserVariable
        {
            get
            {
                if (_userVariableReference == null)
                    return null;

                return _userVariableReference.UserVariable;
            }
            set
            {
                if (_userVariableReference == null)
                    _userVariableReference = new XmlUserVariableReference();

                if (_userVariableReference.UserVariable == value)
                    return;

                _userVariableReference.UserVariable = value;

                if (value == null)
                    _userVariableReference = null;

                RaisePropertyChanged();
            }
        }

        private XmlFormula _variableFormula;
        public XmlFormula VariableFormula
        {
            get { return _variableFormula; }
            set
            {
                _variableFormula = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeVariableBrick() { }

        public XmlChangeVariableBrick(XElement xElement) : base(xElement) { }


        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("userVariable") != null)
                _userVariableReference = new XmlUserVariableReference(xRoot.Element("userVariable"));

            if (xRoot.Element("variableFormula") != null)
                VariableFormula = new XmlFormula(xRoot.Element("variableFormula"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeVariableBrick");

            if(_userVariableReference != null)
                xRoot.Add(_userVariableReference.CreateXml());

            var xFormula = new XElement("variableFormula");
            xFormula.Add(VariableFormula.CreateXml());
            xRoot.Add(xFormula);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_userVariableReference != null)
                _userVariableReference.LoadReference();
            if (VariableFormula != null)
                VariableFormula.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeVariableBrick();

            if(_userVariableReference != null)
                newBrick._userVariableReference = _userVariableReference.Copy() as XmlUserVariableReference;
            if(VariableFormula != null)
                newBrick.VariableFormula = VariableFormula.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeVariableBrick;

            if (otherBrick == null)
                return false;

            if (!UserVariableReference.Equals(otherBrick.UserVariableReference))
                return false;

            return VariableFormula.Equals(otherBrick.VariableFormula);
        }
    }
}