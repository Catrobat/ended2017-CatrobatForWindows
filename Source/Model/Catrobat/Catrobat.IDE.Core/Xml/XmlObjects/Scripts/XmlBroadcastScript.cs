using System.Collections.ObjectModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlBroadcastScript : XmlScript
    {
        public ObservableCollection<string> UIReceivedMessages
        {
            get
            {
                // TODO: return available UIReceivedMessages from Project
                return new ObservableCollection<string>();
            }
        }

        private string _receivedMessage;
        public string ReceivedMessage
        {
            get { return _receivedMessage; }
            set
            {
                if (_receivedMessage == value)
                {
                    return;
                }

                // TODO: update available UIReceivedMessages from Project
                _receivedMessage = value;
                RaisePropertyChanged();
            }
        }


        public XmlBroadcastScript() {}

        public XmlBroadcastScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("receivedMessage") != null)
            {
                _receivedMessage = xRoot.Element("receivedMessage").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("broadcastScript");

            CreateCommonXML(xRoot);

            if (_receivedMessage != null)
            {
                xRoot.Add(new XElement("receivedMessage")
                {
                    Value = _receivedMessage
                });
            }

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBroadcastScript = new XmlBroadcastScript();
            newBroadcastScript._receivedMessage = _receivedMessage;
            if (Bricks != null)
            {
                newBroadcastScript.Bricks = Bricks.Copy() as XmlBrickList;
            }

            return newBroadcastScript;
        }

        public override bool Equals(XmlObject other)
        {
            var otherScript = other as XmlBroadcastScript;

            if (otherScript == null)
                return false;

            if (ReceivedMessage != otherScript.ReceivedMessage)
                return false;

            return Bricks.Equals(((XmlScript) otherScript).Bricks);
        }
    }
}