using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    public class XmlNxtMotorActionBrick : XmlBrick
    {
        protected string _motor;
        public string Motor
        {
            get { return _motor; }
            set
            {
                if (_motor == value)
                {
                    return;
                }

                _motor = value;
                RaisePropertyChanged();
            }
        }

        protected XmlFormula _speed;
        public XmlFormula Speed
        {
            get { return _speed; }
            set
            {
                if (_speed == value)
                {
                    return;
                }

                _speed = value;
                RaisePropertyChanged();
            }
        }


        public XmlNxtMotorActionBrick() {}

        public XmlNxtMotorActionBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
            _speed = new XmlFormula(xRoot.Element("speed"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtMotorActionBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            var xVariable = new XElement("speed");
            xVariable.Add(_speed.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_speed != null)
                _speed.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNxtMotorActionBrick();
            newBrick._motor = _motor;
            newBrick._speed = _speed.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNxtMotorActionBrick;

            if (otherBrick == null)
                return false;

            if (Motor != otherBrick.Motor)
                return false;

            return Speed.Equals(otherBrick.Speed);
        }
    }
}