using System.Collections.ObjectModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;

namespace Catrobat.Core.Objects.Scripts
{
    public class BroadcastScript : Script
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


        public BroadcastScript() {}

        public BroadcastScript(Sprite parent) : base(parent) {}

        public BroadcastScript(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("receivedMessage") != null)
            {
                _receivedMessage = xRoot.Element("receivedMessage").Value;
            }
        }

        internal override XElement CreateXML()
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

        public override DataObject Copy(Sprite parent)
        {
            var newBroadcastScript = new BroadcastScript(parent);
            newBroadcastScript._receivedMessage = _receivedMessage;
            if (_bricks != null)
            {
                newBroadcastScript._bricks = _bricks.Copy(parent) as BrickList;
            }

            return newBroadcastScript;
        }
    }
}