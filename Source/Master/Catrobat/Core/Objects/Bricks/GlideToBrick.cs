using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class GlideToBrick : Brick
    {
        protected int durationInMilliSeconds;

        protected int xDestination;

        protected int yDestination;

        public GlideToBrick()
        {
        }

        public GlideToBrick(Sprite parent) : base(parent)
        {
        }

        public GlideToBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int DurationInMilliSeconds
        {
            get { return durationInMilliSeconds; }
            set
            {
                if (durationInMilliSeconds == value)
                    return;

                durationInMilliSeconds = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DurationInMilliSeconds"));
            }
        }

        public int XDestination
        {
            get { return xDestination; }
            set
            {
                if (xDestination == value)
                    return;

                xDestination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XDestination"));
            }
        }

        public int YDestination
        {
            get { return yDestination; }
            set
            {
                if (yDestination == value)
                    return;

                yDestination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YDestination"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            durationInMilliSeconds = int.Parse(xRoot.Element("durationInMilliSeconds").Value);
            xDestination = int.Parse(xRoot.Element("xDestination").Value);
            yDestination = int.Parse(xRoot.Element("yDestination").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("glideToBrick");

            xRoot.Add(new XElement("durationInMilliSeconds")
                {
                    Value = durationInMilliSeconds.ToString()
                });

            xRoot.Add(new XElement("xDestination")
                {
                    Value = xDestination.ToString()
                });

            xRoot.Add(new XElement("yDestination")
                {
                    Value = yDestination.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new GlideToBrick(parent);
            newBrick.durationInMilliSeconds = durationInMilliSeconds;
            newBrick.xDestination = xDestination;
            newBrick.yDestination = yDestination;

            return newBrick;
        }
    }
}