using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    public class XmlNxtPlayToneBrick : XmlBrick
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

        protected XmlFormula _frequency;
        public XmlFormula Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequency == value)
                {
                    return;
                }

                _frequency = value;
                RaisePropertyChanged();
            }
        }


        public XmlNxtPlayToneBrick() {}

        public XmlNxtPlayToneBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _durationInSeconds = new XmlFormula(xRoot.Element("durationInSeconds"));
            _frequency = new XmlFormula(xRoot.Element("frequency"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtPlayToneBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(_durationInSeconds.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("frequency");
            xVariable2.Add(_frequency.CreateXml());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_durationInSeconds != null)
                _durationInSeconds.LoadReference();
            if (_frequency != null)
                _frequency.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNxtPlayToneBrick();
            newBrick._durationInSeconds = _durationInSeconds.Copy() as XmlFormula;
            newBrick._frequency = _frequency.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNxtPlayToneBrick;

            if (otherBrick == null)
                return false;

            if (!DurationInSeconds.Equals(otherBrick.DurationInSeconds))
                return false;

            return Frequency.Equals(otherBrick.Frequency);
        }
    }
}