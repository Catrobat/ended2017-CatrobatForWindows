using System;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.CatrobatObjects.Formulas;

namespace Catrobat.Core.CatrobatObjects.Bricks
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

        public NxtMotorTurnAngleBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = new Formula(xRoot.Element("degrees"));
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtMotorTurnAngleBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXML());
            xRoot.Add(xVariable);

             xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new NxtMotorTurnAngleBrick();
            newBrick._degrees = _degrees.Copy() as Formula;
            newBrick._motor = _motor;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as NxtMotorTurnAngleBrick;

            if (otherBrick == null)
                return false;

            if (Motor != otherBrick.Motor)
                return false;

            return Degrees.Equals(otherBrick.Degrees);
        }
    }
}