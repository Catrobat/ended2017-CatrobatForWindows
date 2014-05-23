using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPlaceAtBrick : XmlBrick
    {
        protected XmlFormula _xPosition;
        public XmlFormula XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }

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


        public XmlPlaceAtBrick() {}

        public XmlPlaceAtBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _xPosition = new XmlFormula(xRoot.Element("xPosition"));
            _yPosition = new XmlFormula(xRoot.Element("yPosition"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("placeAtBrick");

            var xVariable1 = new XElement("xPosition");
            xVariable1.Add(_xPosition.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("yPosition");
            xVariable2.Add(_yPosition.CreateXml());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_xPosition != null)
                _xPosition.LoadReference();
            if (_yPosition != null)
                _yPosition.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlPlaceAtBrick();
            newBrick._xPosition = _xPosition.Copy() as XmlFormula;
            newBrick._yPosition = _yPosition.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlPlaceAtBrick;

            if (otherBrick == null)
                return false;

            if (!XPosition.Equals(otherBrick.XPosition))
                return false;

            return YPosition.Equals(otherBrick.YPosition);
        }
    }
}