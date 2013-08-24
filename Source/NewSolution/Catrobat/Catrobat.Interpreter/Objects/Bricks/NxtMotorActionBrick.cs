using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Interpreter.Objects.Formulas;

namespace Catrobat.Interpreter.Objects.Bricks
{
    public class NxtMotorActionBrick : Brick
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

        protected Formula _speed;
        public Formula Speed
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


        public NxtMotorActionBrick() {}

        public NxtMotorActionBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
            _speed = new Formula(xRoot.Element("speed"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtMotorActionBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            var xVariable = new XElement("speed");
            xVariable.Add(_speed.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new NxtMotorActionBrick();
            newBrick._motor = _motor;
            newBrick._speed = _speed.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as NxtMotorActionBrick;

            if (otherBrick == null)
                return false;

            if (Motor != otherBrick.Motor)
                return false;

            return Speed.Equals(otherBrick.Speed);
        }
    }
}