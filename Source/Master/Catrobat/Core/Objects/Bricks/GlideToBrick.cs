using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class GlideToBrick : Brick
    {
        protected int _durationInMilliSeconds;

        protected int _xDestination;

        protected int _yDestination;

        public GlideToBrick() {}

        public GlideToBrick(Sprite parent) : base(parent) {}

        public GlideToBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int DurationInMilliSeconds
        {
            get { return _durationInMilliSeconds; }
            set
            {
                if (_durationInMilliSeconds == value)
                {
                    return;
                }

                _durationInMilliSeconds = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DurationInMilliSeconds"));
            }
        }

        public int XDestination
        {
            get { return _xDestination; }
            set
            {
                if (_xDestination == value)
                {
                    return;
                }

                _xDestination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XDestination"));
            }
        }

        public int YDestination
        {
            get { return _yDestination; }
            set
            {
                if (_yDestination == value)
                {
                    return;
                }

                _yDestination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YDestination"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInMilliSeconds = int.Parse(xRoot.Element("durationInMilliSeconds").Value);
            _xDestination = int.Parse(xRoot.Element("xDestination").Value);
            _yDestination = int.Parse(xRoot.Element("yDestination").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("glideToBrick");

            xRoot.Add(new XElement("durationInMilliSeconds")
            {
                Value = _durationInMilliSeconds.ToString()
            });

            xRoot.Add(new XElement("xDestination")
            {
                Value = _xDestination.ToString()
            });

            xRoot.Add(new XElement("yDestination")
            {
                Value = _yDestination.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new GlideToBrick(parent);
            newBrick._durationInMilliSeconds = _durationInMilliSeconds;
            newBrick._xDestination = _xDestination;
            newBrick._yDestination = _yDestination;

            return newBrick;
        }
    }
}