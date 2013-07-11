using System;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtMotorTurnAngleBrick : Brick
    {
        protected Formula _degrees;
        public Formula Degrees
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


        public NxtMotorTurnAngleBrick() {}

        public NxtMotorTurnAngleBrick(Sprite parent) : base(parent) {}

        public NxtMotorTurnAngleBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = new Formula(xRoot.Element("degrees"));
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtMotorTurnAngleBrick");

             xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

             var xVariable = new XElement("degrees");
             xVariable.Add(_degrees.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorTurnAngleBrick(parent);
            newBrick._degrees = _degrees.Copy(parent) as Formula;
            newBrick._motor = _motor;

            return newBrick;
        }
    }
}