using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtMotorActionBrick : Brick
    {
        protected string motor;

        protected int speed;

        public NxtMotorActionBrick()
        {
        }

        public NxtMotorActionBrick(Sprite parent) : base(parent)
        {
        }

        public NxtMotorActionBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public string Motor
        {
            get { return motor; }
            set
            {
                if (motor == value)
                    return;

                motor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Motor"));
            }
        }

        public int Speed
        {
            get { return speed; }
            set
            {
                if (speed == value)
                    return;

                speed = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Speed"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            motor = xRoot.Element("motor").Value;
            speed = int.Parse(xRoot.Element("speed").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtMotorActionBrick");

            xRoot.Add(new XElement("motor")
                {
                    Value = motor
                });

            xRoot.Add(new XElement("speed")
                {
                    Value = speed.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorActionBrick(parent);
            newBrick.motor = motor;
            newBrick.speed = speed;

            return newBrick;
        }
    }
}