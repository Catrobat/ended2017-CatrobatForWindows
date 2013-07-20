using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtMotorStopBrick : Brick
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


        public NxtMotorStopBrick() {}

        public NxtMotorStopBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtMotorStopBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new NxtMotorStopBrick();
            newBrick._motor = _motor;

            return newBrick;
        }
    }
}