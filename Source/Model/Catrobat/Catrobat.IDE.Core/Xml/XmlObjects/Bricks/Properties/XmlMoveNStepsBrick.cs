using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlMoveNStepsBrick : XmlBrick
    {
        protected XmlFormula _steps;
        public XmlFormula Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged();
            }
        }


        public XmlMoveNStepsBrick() {}

        public XmlMoveNStepsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _steps = new XmlFormula(xRoot.Element("steps"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("moveNStepsBrick");

            var xVariable = new XElement("steps");
            xVariable.Add(_steps.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_steps != null)
                _steps.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlMoveNStepsBrick();
            newBrick._steps = _steps.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlMoveNStepsBrick;

            if (otherBrick == null)
                return false;

            return Steps.Equals(otherBrick.Steps);
        }
    }
}