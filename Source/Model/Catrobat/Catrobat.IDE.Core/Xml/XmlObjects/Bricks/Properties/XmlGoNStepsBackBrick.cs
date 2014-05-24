using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGoNStepsBackBrick : XmlBrick
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

        public XmlGoNStepsBackBrick() {}

        public XmlGoNStepsBackBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _steps = new XmlFormula(xRoot.Element("steps"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("goNStepsBackBrick");

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
            var newBrick = new XmlGoNStepsBackBrick();
            newBrick._steps = _steps.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlGoNStepsBackBrick;

            if (otherBrick == null)
                return false;

            return Steps.Equals(otherBrick.Steps);
        }
    }
}