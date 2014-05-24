using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGlideToBrick : XmlBrick
    {
        protected XmlFormula _durationInSeconds;
        public XmlFormula DurationInSeconds
        {
            get { return _durationInSeconds; }
            set
            {
                if (_durationInSeconds == value)
                {
                    return;
                }

                _durationInSeconds = value;
                RaisePropertyChanged();
            }
        }

        protected XmlFormula _xDestination;
        public XmlFormula XDestination
        {
            get { return _xDestination; }
            set
            {
                if (_xDestination == value)
                {
                    return;
                }

                _xDestination = value;
                RaisePropertyChanged();
            }
        }

        protected XmlFormula _yDestination;
        public XmlFormula YDestination
        {
            get { return _yDestination; }
            set
            {
                if (_yDestination == value)
                {
                    return;
                }

                _yDestination = value;
                RaisePropertyChanged();
            }
        }


        public XmlGlideToBrick() {}

        public XmlGlideToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _durationInSeconds = new XmlFormula(xRoot.Element("durationInSeconds"));
            _xDestination = new XmlFormula(xRoot.Element("xDestination"));
            _yDestination = new XmlFormula(xRoot.Element("yDestination"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("glideToBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(_durationInSeconds.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("xDestination");
            xVariable2.Add(_xDestination.CreateXml());
            xRoot.Add(xVariable2);

            var xVariable3 = new XElement("yDestination");
            xVariable3.Add(_yDestination.CreateXml());
            xRoot.Add(xVariable3);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_durationInSeconds != null)
                _durationInSeconds.LoadReference();
            if (_xDestination != null)
                _xDestination.LoadReference();
            if (_yDestination != null)
                _yDestination.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlGlideToBrick();
            newBrick._durationInSeconds = _durationInSeconds.Copy() as XmlFormula;
            newBrick._xDestination = _xDestination.Copy() as XmlFormula;
            newBrick._yDestination = _yDestination.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlGlideToBrick;

            if (otherBrick == null)
                return false;

            if (!DurationInSeconds.Equals(otherBrick.DurationInSeconds))
                return false;

            if (!XDestination.Equals(otherBrick.XDestination))
                return false;

            return YDestination.Equals(otherBrick.YDestination);
        }
    }
}