using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.CatrobatObjects.Formulas;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class NxtPlayToneBrick : Brick
    {
        protected Formula _durationInSeconds;
        public Formula DurationInSeconds
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

        protected Formula _frequency;
        public Formula Frequency
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

        public NxtPlayToneBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInSeconds = new Formula(xRoot.Element("durationInSeconds"));
            _frequency = new Formula(xRoot.Element("frequency"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("legoNxtPlayToneBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(_durationInSeconds.CreateXML());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("frequency");
            xVariable2.Add(_frequency.CreateXML());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new NxtPlayToneBrick();
            newBrick._durationInSeconds = _durationInSeconds.Copy() as Formula;
            newBrick._frequency = _frequency.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as NxtPlayToneBrick;

            if (otherBrick == null)
                return false;

            if (!DurationInSeconds.Equals(otherBrick.DurationInSeconds))
                return false;

            return Frequency.Equals(otherBrick.Frequency);
        }
    }
}