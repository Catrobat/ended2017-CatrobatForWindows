using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
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

        public NxtMotorActionBrick(Sprite parent) : base(parent) {}

        public NxtMotorActionBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorActionBrick(parent);
            newBrick._motor = _motor;
            newBrick._speed = _speed.Copy(parent) as Formula;

            return newBrick;
        }
    }
}