using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class GlideToBrick : Brick
    {
        protected int _durationInSeconds;
        public int DurationInSeconds
        {
            get { return _durationInSeconds; }
            set
            {
                if (_durationInSeconds == value)
                {
                    return;
                }

                _durationInSeconds = value;
                RaisePropertyChanged();
            }
        }

        protected int _xDestination;
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
                RaisePropertyChanged();
            }
        }

        protected int _yDestination;
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
                RaisePropertyChanged();
            }
        }


        public GlideToBrick() {}

        public GlideToBrick(Sprite parent) : base(parent) {}

        public GlideToBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInSeconds = int.Parse(xRoot.Element("durationInSeconds").Value);
            _xDestination = int.Parse(xRoot.Element("xDestination").Value);
            _yDestination = int.Parse(xRoot.Element("yDestination").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("glideToBrick");

            xRoot.Add(new XElement("durationInSeconds")
            {
                Value = _durationInSeconds.ToString()
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
            newBrick._durationInSeconds = _durationInSeconds;
            newBrick._xDestination = _xDestination;
            newBrick._yDestination = _yDestination;

            return newBrick;
        }
    }
}