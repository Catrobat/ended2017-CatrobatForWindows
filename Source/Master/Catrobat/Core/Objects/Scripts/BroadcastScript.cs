using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class BroadcastScript : Script
    {
        private string receivedMessage;

        public BroadcastScript()
        {
        }

        public BroadcastScript(Sprite parent) : base(parent)
        {
        }

        public BroadcastScript(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public string ReceivedMessage
        {
            get { return receivedMessage; }
            set
            {
                if (receivedMessage == value)
                    return;

                // TODO: update available UIReceivedMessages from Project
                receivedMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ReceivedMessage"));
            }
        }

        public ObservableCollection<string> UIReceivedMessages
        {
            get
            {
                // TODO: return available UIReceivedMessages from Project
                return new ObservableCollection<string>();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("receivedMessage") != null)
                receivedMessage = xRoot.Element("receivedMessage").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("broadcastScript");

            CreateCommonXML(xRoot);

            if (receivedMessage != null)
            {
                xRoot.Add(new XElement("receivedMessage")
                    {
                        Value = receivedMessage
                    });
            }

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBroadcastScript = new BroadcastScript(parent);
            newBroadcastScript.receivedMessage = receivedMessage;
            if (bricks != null)
                newBroadcastScript.bricks = bricks.Copy(parent) as BrickList;

            return newBroadcastScript;
        }
    }
}