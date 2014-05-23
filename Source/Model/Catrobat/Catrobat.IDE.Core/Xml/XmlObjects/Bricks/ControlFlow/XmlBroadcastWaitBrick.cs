using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlBroadcastWaitBrick : XmlBrick
    {
        protected string _broadcastMessage;
        public string BroadcastMessage
        {
            get { return _broadcastMessage; }
            set
            {
                if (_broadcastMessage == value)
                {
                    return;
                }

                _broadcastMessage = value;
                RaisePropertyChanged();
            }
        }

        public XmlBroadcastWaitBrick() {}

        public XmlBroadcastWaitBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
            {
                _broadcastMessage = xRoot.Element("broadcastMessage").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("broadcastWaitBrick");

            if (_broadcastMessage != null)
            {
                xRoot.Add(new XElement("broadcastMessage")
                {
                    Value = _broadcastMessage
                });
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlBroadcastWaitBrick();
            newBrick._broadcastMessage = _broadcastMessage;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlBroadcastWaitBrick;

            if (otherBrick == null)
                return false;

            if (BroadcastMessage != otherBrick.BroadcastMessage)
                return false;

            return true;
        }
    }
}