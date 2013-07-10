using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetVariableBrick : Brick
    {
        protected double _userVariable; //TODO: correct type to userVariable
        public double UserVariable
        {
            get { return _userVariable; }
            set
            {
                _userVariable = value;
                RaisePropertyChanged();
            }
        }

        protected double _variableFormula;//TODO: correct type to formula
        public double VariableFormula
        {
            get { return _variableFormula; }
            set
            {
                _variableFormula = value;
                RaisePropertyChanged();
            }
        }

        public SetVariableBrick() {}

        public SetVariableBrick(Sprite parent) : base(parent) {}

        public SetVariableBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }

        

        internal override void LoadFromXML(XElement xRoot)
        {
            _userVariable = double.Parse(xRoot.Element("userVariable").Value, CultureInfo.InvariantCulture);
            _variableFormula = double.Parse(xRoot.Element("variableFormula").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setVariableBrick");

            xRoot.Add(new XElement("userVariable")
            {
                Value = _userVariable.ToString()
            });

            xRoot.Add(new XElement("variableFormula")
            {
                Value = _variableFormula.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetVariableBrick(parent);
            newBrick._userVariable = _userVariable;
            newBrick._variableFormula = _variableFormula;

            return newBrick;
        }
    }
}