using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class BroadcastWaitBrick : Brick
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

        public BroadcastWaitBrick() {}

        public BroadcastWaitBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
            {
                _broadcastMessage = xRoot.Element("broadcastMessage").Value;
            }
        }

        internal override XElement CreateXML()
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

        public override DataObject Copy()
        {
            var newBrick = new BroadcastWaitBrick();
            newBrick._broadcastMessage = _broadcastMessage;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as BroadcastWaitBrick;

            if (otherBrick == null)
                return false;

            if (BroadcastMessage != otherBrick.BroadcastMessage)
                return false;

            return true;
        }
    }
}