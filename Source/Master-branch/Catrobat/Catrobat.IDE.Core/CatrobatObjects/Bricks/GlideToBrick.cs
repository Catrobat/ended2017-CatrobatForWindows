using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class GlideToBrick : Brick
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

        protected Formula _xDestination;
        public Formula XDestination
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

        protected Formula _yDestination;
        public Formula YDestination
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

        public GlideToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _durationInSeconds = new Formula(xRoot.Element("durationInSeconds"));
            _xDestination = new Formula(xRoot.Element("xDestination"));
            _yDestination = new Formula(xRoot.Element("yDestination"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("glideToBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(_durationInSeconds.CreateXML());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("xDestination");
            xVariable2.Add(_xDestination.CreateXML());
            xRoot.Add(xVariable2);

            var xVariable3 = new XElement("yDestination");
            xVariable3.Add(_yDestination.CreateXML());
            xRoot.Add(xVariable3);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new GlideToBrick();
            newBrick._durationInSeconds = _durationInSeconds.Copy() as Formula;
            newBrick._xDestination = _xDestination.Copy() as Formula;
            newBrick._yDestination = _yDestination.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as GlideToBrick;

            if (otherBrick == null)
                return false;

            if (!DurationInSeconds.Equals(otherBrick.DurationInSeconds))
                return false;

            if (!XDestination.Equals(otherBrick.XDestination))
                return false;

            return YDestination.Equals(otherBrick.YDestination);
        }
    }
}