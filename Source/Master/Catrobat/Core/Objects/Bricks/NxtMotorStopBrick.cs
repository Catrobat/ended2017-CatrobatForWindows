using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class NxtMotorStopBrick : Brick
    {
        protected string motor;

        public NxtMotorStopBrick()
        {
        }

        public NxtMotorStopBrick(Sprite parent) : base(parent)
        {
        }

        public NxtMotorStopBrick(XElement xElement, Sprite parent) : base(xElement, parent)
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

        internal override void LoadFromXML(XElement xRoot)
        {
            motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtMotorStopBrick");

            xRoot.Add(new XElement("motor")
                {
                    Value = motor
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorStopBrick(parent);
            newBrick.motor = motor;

            return newBrick;
        }
    }
}