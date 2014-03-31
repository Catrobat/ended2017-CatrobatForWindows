using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class ChangeVariableBrick : Brick
    {
        private UserVariableReference _userVariableReference;
        internal UserVariableReference UserVariableReference
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

        public UserVariable UserVariable
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
                    _userVariableReference = new UserVariableReference();

                if (_userVariableReference.UserVariable == value)
                    return;

                _userVariableReference.UserVariable = value;

                if (value == null)
                    _userVariableReference = null;

                RaisePropertyChanged();
            }
        }

        protected Formula _variableFormula;
        public Formula VariableFormula
        {
            get { return _variableFormula; }
            set
            {
                _variableFormula = value;
                RaisePropertyChanged();
            }
        }


        public ChangeVariableBrick() { }

        public ChangeVariableBrick(XElement xElement) : base(xElement) { }


        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("userVariable") != null)
                _userVariableReference = new UserVariableReference(xRoot.Element("userVariable"));

            if (xRoot.Element("variableFormula") != null)
                _variableFormula = new Formula(xRoot.Element("variableFormula"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeVariableBrick");

            if(_userVariableReference != null)
                xRoot.Add(_userVariableReference.CreateXML());

            var xFormula = new XElement("variableFormula");
            xFormula.Add(_variableFormula.CreateXML());
            xRoot.Add(xFormula);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_userVariableReference != null)
                _userVariableReference.LoadReference();
            if (_variableFormula != null)
                _variableFormula.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeVariableBrick();

            if(_userVariableReference != null)
                newBrick._userVariableReference = _userVariableReference.Copy() as UserVariableReference;
            if(_variableFormula != null)
                newBrick._variableFormula = _variableFormula.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeVariableBrick;

            if (otherBrick == null)
                return false;

            if (!UserVariableReference.Equals(otherBrick.UserVariableReference))
                return false;

            return VariableFormula.Equals(otherBrick.VariableFormula);
        }
    }
}