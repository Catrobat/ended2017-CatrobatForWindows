using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class WaitBrick : Brick
    {
        protected int _timeToWaitInSeconds;
        public int TimeToWaitInSeconds
        {
            get { return _timeToWaitInSeconds; }
            set
            {
                _timeToWaitInSeconds = value;
                RaisePropertyChanged();
            }
        }


        public WaitBrick() {}

        public WaitBrick(Sprite parent) : base(parent) {}

        public WaitBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        

        internal override void LoadFromXML(XElement xRoot)
        {
            _timeToWaitInSeconds = int.Parse(xRoot.Element("timeToWaitInSeconds").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("waitBrick");

            xRoot.Add(new XElement("timeToWaitInSeconds")
            {
                Value = _timeToWaitInSeconds.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new WaitBrick(parent);
            newBrick._timeToWaitInSeconds = _timeToWaitInSeconds;

            return newBrick;
        }
    }
}