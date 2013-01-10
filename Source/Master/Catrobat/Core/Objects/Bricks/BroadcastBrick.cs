using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class BroadcastBrick : Brick
    {
        protected string broadcastMessage;

        public BroadcastBrick()
        {
        }

        public BroadcastBrick(Sprite parent) : base(parent)
        {
        }

        public BroadcastBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public string BroadcastMessage
        {
            get { return broadcastMessage; }
            set
            {
                if (broadcastMessage == value)
                    return;

                broadcastMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BroadcastMessage"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
                broadcastMessage = xRoot.Element("broadcastMessage").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("broadcastBrick");

            if (broadcastMessage != null)
            {
                xRoot.Add(new XElement("broadcastMessage")
                    {
                        Value = broadcastMessage
                    });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new BroadcastBrick(parent);
            newBrick.broadcastMessage = broadcastMessage;

            return newBrick;
        }
    }
}