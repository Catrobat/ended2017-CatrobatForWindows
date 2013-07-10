using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtMotorTurnAngleBrick : Brick
    {
        protected int _degrees;
        protected string _motor;

        public NxtMotorTurnAngleBrick() {}

        public NxtMotorTurnAngleBrick(Sprite parent) : base(parent) {}

        public NxtMotorTurnAngleBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public int Degrees
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

        internal override void LoadFromXML(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
            _degrees = Int32.Parse(xRoot.Element("degrees").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtMotorTurnAngleBrick");

            xRoot.Add(new XElement("degrees")
            {
                Value = _degrees.ToString()
            });

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorTurnAngleBrick(parent);
            newBrick._degrees = _degrees;
            newBrick._motor = _motor;

            return newBrick;
        }
    }
}