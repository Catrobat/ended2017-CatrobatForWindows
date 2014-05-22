using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class BroadcastBrick : Brick
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

        public BroadcastBrick() {}

        public BroadcastBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
            {
                _broadcastMessage = xRoot.Element("broadcastMessage").Value;
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("broadcastBrick");

            if (_broadcastMessage != null)
            {
                xRoot.Add(new XElement("broadcastMessage")
                {
                    Value = _broadcastMessage
                });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new BroadcastBrick();
            newBrick._broadcastMessage = _broadcastMessage;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as BroadcastBrick;

            if (otherBrick == null)
                return false;

            if (BroadcastMessage != otherBrick.BroadcastMessage)
                return false;

            return true;
        }
    }
}