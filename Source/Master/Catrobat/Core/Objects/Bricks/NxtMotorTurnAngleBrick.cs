using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class NxtMotorTurnAngleBrick : Brick
    {
        protected int degrees;
        protected string motor;

        public NxtMotorTurnAngleBrick()
        {
        }

        public NxtMotorTurnAngleBrick(Sprite parent) : base(parent)
        {
        }

        public NxtMotorTurnAngleBrick(XElement xElement, Sprite parent) : base(xElement, parent)
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

        public int Degrees
        {
            get { return degrees; }
            set
            {
                if (degrees == value)
                    return;

                degrees = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Degrees"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            motor = xRoot.Element("motor").Value;
            degrees = Int32.Parse(xRoot.Element("degrees").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtMotorTurnAngleBrick");

            xRoot.Add(new XElement("degrees")
                {
                    Value = degrees.ToString()
                });

            xRoot.Add(new XElement("motor")
                {
                    Value = motor
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorTurnAngleBrick(parent);
            newBrick.degrees = degrees;
            newBrick.motor = motor;

            return newBrick;
        }
    }
}