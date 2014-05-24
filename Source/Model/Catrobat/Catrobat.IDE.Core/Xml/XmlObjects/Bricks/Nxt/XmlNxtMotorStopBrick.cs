using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    public class XmlNxtMotorStopBrick : XmlBrick
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


        public XmlNxtMotorStopBrick() {}

        public XmlNxtMotorStopBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _motor = xRoot.Element("motor").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtMotorStopBrick");

            xRoot.Add(new XElement("motor")
            {
                Value = _motor
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNxtMotorStopBrick();
            newBrick._motor = _motor;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNxtMotorStopBrick;

            if (otherBrick == null)
                return false;

            if (Motor != otherBrick.Motor)
                return false;

            return true;
        }
    }
}