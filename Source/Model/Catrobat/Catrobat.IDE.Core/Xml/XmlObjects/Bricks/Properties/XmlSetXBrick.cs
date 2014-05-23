using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetXBrick : XmlBrick
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


        public XmlSetXBrick() {}

        public XmlSetXBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _xPosition = new XmlFormula(xRoot.Element("xPosition"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setXBrick");

            var xVariable = new XElement("xPosition");
            xVariable.Add(_xPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_xPosition != null)
                _xPosition.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlSetXBrick();
            newBrick._xPosition = _xPosition.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSetXBrick;

            if (otherBrick == null)
                return false;

            return XPosition.Equals(otherBrick.XPosition);
        }
    }
}