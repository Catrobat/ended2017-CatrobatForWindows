using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class WaitBrick : Brick
    {
        protected int timeToWaitInMilliSeconds;

        public WaitBrick()
        {
        }

        public WaitBrick(Sprite parent) : base(parent)
        {
        }

        public WaitBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int TimeToWaitInMilliSeconds
        {
            get { return timeToWaitInMilliSeconds; }
            set
            {
                timeToWaitInMilliSeconds = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TimeToWaitInMilliSeconds"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            timeToWaitInMilliSeconds = int.Parse(xRoot.Element("timeToWaitInMilliSeconds").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("waitBrick");

            xRoot.Add(new XElement("timeToWaitInMilliSeconds")
                {
                    Value = timeToWaitInMilliSeconds.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new WaitBrick(parent);
            newBrick.timeToWaitInMilliSeconds = timeToWaitInMilliSeconds;

            return newBrick;
        }
    }
}