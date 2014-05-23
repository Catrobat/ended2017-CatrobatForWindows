using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetYBrick : XmlBrick
    {
        protected XmlFormula _yPosition;
        public XmlFormula YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                RaisePropertyChanged();
            }
        }


        public XmlSetYBrick() {}

        public XmlSetYBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _yPosition = new XmlFormula(xRoot.Element("yPosition"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setYBrick");

            var xVariable = new XElement("yPosition");
            xVariable.Add(_yPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_yPosition != null)
                _yPosition.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlSetYBrick();
            newBrick._yPosition = _yPosition.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSetYBrick;

            if (otherBrick == null)
                return false;

            return YPosition.Equals(otherBrick.YPosition);
        }
    }
}