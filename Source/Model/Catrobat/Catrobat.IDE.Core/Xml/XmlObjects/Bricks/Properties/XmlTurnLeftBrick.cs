using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlTurnLeftBrick : XmlBrick
    {
        protected XmlFormula _degrees;
        public XmlFormula Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                RaisePropertyChanged();
            }
        }


        public XmlTurnLeftBrick() {}

        public XmlTurnLeftBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _degrees = new XmlFormula(xRoot.Element("degrees"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("turnLeftBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_degrees != null)
                _degrees.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlTurnLeftBrick();
            newBrick._degrees = _degrees.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlTurnLeftBrick;

            if (otherBrick == null)
                return false;

            return Degrees.Equals(otherBrick.Degrees);
        }
    }
}