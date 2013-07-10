using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NxtPlayToneBrick : Brick
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

        protected int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequency == value)
                {
                    return;
                }

                _frequency = value;
                RaisePropertyChanged();
            }
        }


        public NxtPlayToneBrick() {}

        public NxtPlayToneBrick(Sprite parent) : base(parent) {}

        public NxtPlayToneBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInSeconds = int.Parse(xRoot.Element("durationInSeconds").Value);
            _frequency = int.Parse(xRoot.Element("frequency").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtPlayToneBrick");

            xRoot.Add(new XElement("durationInSeconds")
            {
                Value = _durationInSeconds.ToString()
            });

            xRoot.Add(new XElement("frequency")
            {
                Value = _frequency.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtPlayToneBrick(parent);
            newBrick._durationInSeconds = _durationInSeconds;
            newBrick._frequency = _frequency;

            return newBrick;
        }
    }
}