using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class PlaceAtBrick : Brick
    {
        protected int xPosition = 0;

        protected int yPosition = 0;

        public PlaceAtBrick()
        {
        }

        public PlaceAtBrick(Sprite parent) : base(parent)
        {
        }

        public PlaceAtBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int XPosition
        {
            get { return xPosition; }
            set
            {
                xPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XPosition"));
            }
        }

        public int YPosition
        {
            get { return yPosition; }
            set
            {
                yPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YPosition"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            xPosition = int.Parse(xRoot.Element("xPosition").Value);
            yPosition = int.Parse(xRoot.Element("yPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("placeAtBrick");

            xRoot.Add(new XElement("xPosition")
                {
                    Value = xPosition.ToString()
                });

            xRoot.Add(new XElement("yPosition")
                {
                    Value = yPosition.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PlaceAtBrick(parent);
            newBrick.xPosition = xPosition;
            newBrick.yPosition = yPosition;

            return newBrick;
        }
    }
}