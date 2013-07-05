using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtPlayToneBrick : Brick
    {
        protected int _durationInMs;

        protected int _hertz;

        public NxtPlayToneBrick() {}

        public NxtPlayToneBrick(Sprite parent) : base(parent) {}

        public NxtPlayToneBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int DurationInMs
        {
            get { return _durationInMs; }
            set
            {
                if (_durationInMs == value)
                {
                    return;
                }

                _durationInMs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DurationInMs"));
            }
        }

        public int Hertz
        {
            get { return _hertz; }
            set
            {
                if (_hertz == value)
                {
                    return;
                }

                _hertz = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Hertz"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInMs = int.Parse(xRoot.Element("durationInMs").Value);
            _hertz = int.Parse(xRoot.Element("hertz").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtPlayToneBrick");

            xRoot.Add(new XElement("durationInMs")
            {
                Value = _durationInMs.ToString()
            });

            xRoot.Add(new XElement("hertz")
            {
                Value = _hertz.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtPlayToneBrick(parent);
            newBrick._durationInMs = _durationInMs;
            newBrick._hertz = _hertz;

            return newBrick;
        }
    }
}