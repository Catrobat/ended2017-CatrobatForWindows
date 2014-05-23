using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    public class XmlNxtMotorTurnAngleBrick : XmlBrick
    {
        protected XmlFormula _degrees;
        public XmlFormula Degrees
        {
            get { return _degrees; }
            set
            {
                if (_degrees == value)
                {
                    return;
                }

                _degrees = value;
                RaisePropertyChanged();
            }
        }

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


        public XmlNxtMotorTurnAngleBrick() {}

        public XmlNxtMotorTurnAngleBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _degrees = new XmlFormula(xRoot.Element("degrees"));
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtMotorTurnAngleBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXml());
            xRoot.Add(xVariable);

             xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_degrees != null)
                _degrees.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNxtMotorTurnAngleBrick();
            newBrick._degrees = _degrees.Copy() as XmlFormula;
            newBrick._motor = _motor;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNxtMotorTurnAngleBrick;

            if (otherBrick == null)
                return false;

            if (Motor != otherBrick.Motor)
                return false;

            return Degrees.Equals(otherBrick.Degrees);
        }
    }
}