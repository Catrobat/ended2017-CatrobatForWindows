using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class PlaceAtBrick : Brick
    {
        protected int _xPosition = 0;

        protected int _yPosition = 0;

        public PlaceAtBrick() {}

        public PlaceAtBrick(Sprite parent) : base(parent) {}

        public PlaceAtBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XPosition"));
            }
        }

        public int YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YPosition"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _xPosition = int.Parse(xRoot.Element("xPosition").Value);
            _yPosition = int.Parse(xRoot.Element("yPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("placeAtBrick");

            xRoot.Add(new XElement("xPosition")
            {
                Value = _xPosition.ToString()
            });

            xRoot.Add(new XElement("yPosition")
            {
                Value = _yPosition.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PlaceAtBrick(parent);
            newBrick._xPosition = _xPosition;
            newBrick._yPosition = _yPosition;

            return newBrick;
        }
    }
}