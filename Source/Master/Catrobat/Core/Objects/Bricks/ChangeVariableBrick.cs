using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeVariableBrick : Brick
    {
        protected UserVariableReference _userVariableReference;
        public UserVariableReference UserVariableReference
        {
            get { return _userVariableReference; }
            set
            {
                _userVariableReference = value;
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


        public ChangeVariableBrick() {}

        public ChangeVariableBrick(Sprite parent) : base(parent) {}

        public ChangeVariableBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }


        internal override void LoadFromXML(XElement xRoot)
        {
            _userVariableReference = new UserVariableReference(xRoot.Element("userVariable"), _sprite);
            _variableFormula = new Formula(xRoot.Element("variableFormula"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeVariableBrick");

            var xVariable1 = new XElement("userVariable");
            xVariable1.Add(_userVariableReference.CreateXML());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("variableFormula");
            xVariable2.Add(_variableFormula.CreateXML());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeVariableBrick(parent);
            newBrick._userVariableReference = _userVariableReference.Copy(parent) as UserVariableReference;
            newBrick._variableFormula = _variableFormula.Copy(parent) as Formula;

            return newBrick;
        }
    }
}