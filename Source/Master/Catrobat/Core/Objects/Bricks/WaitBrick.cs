using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class WaitBrick : Brick
    {
        protected int _timeToWaitInMilliSeconds;

        public WaitBrick() {}

        public WaitBrick(Sprite parent) : base(parent) {}

        public WaitBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int TimeToWaitInMilliSeconds
        {
            get { return _timeToWaitInMilliSeconds; }
            set
            {
                _timeToWaitInMilliSeconds = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _timeToWaitInMilliSeconds = int.Parse(xRoot.Element("timeToWaitInMilliSeconds").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("waitBrick");

            xRoot.Add(new XElement("timeToWaitInMilliSeconds")
            {
                Value = _timeToWaitInMilliSeconds.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new WaitBrick(parent);
            newBrick._timeToWaitInMilliSeconds = _timeToWaitInMilliSeconds;

            return newBrick;
        }
    }
}