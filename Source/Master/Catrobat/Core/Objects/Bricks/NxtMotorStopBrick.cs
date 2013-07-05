using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtMotorStopBrick : Brick
    {
        protected string _motor;

        public NxtMotorStopBrick() {}

        public NxtMotorStopBrick(Sprite parent) : base(parent) {}

        public NxtMotorStopBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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
                OnPropertyChanged(new PropertyChangedEventArgs("Motor"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtMotorStopBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtMotorStopBrick(parent);
            newBrick._motor = _motor;

            return newBrick;
        }
    }
}