using System.ComponentModel;
using System.Xml.Linq;

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

        protected int _speed;
        public int Speed
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
            _speed = int.Parse(xRoot.Element("speed").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtMotorActionBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            xRoot.Add(new XElement("speed")
            {
                Value = _speed.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorActionBrick(parent);
            newBrick._motor = _motor;
            newBrick._speed = _speed;

            return newBrick;
        }
    }
}