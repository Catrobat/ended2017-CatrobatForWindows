using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
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

        public BroadcastWaitBrick(Sprite parent) : base(parent) {}

        public BroadcastWaitBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new BroadcastWaitBrick(parent);
            newBrick._broadcastMessage = _broadcastMessage;

            return newBrick;
        }
    }
}