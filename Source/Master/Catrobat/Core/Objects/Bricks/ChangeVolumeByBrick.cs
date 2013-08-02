using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeVolumeByBrick : Brick
    {
        protected Formula _volume;
        public Formula Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                RaisePropertyChanged();
            }
        }


        public ChangeVolumeByBrick() {}

        public ChangeVolumeByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _volume = new Formula(xRoot.Element("volume"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeVolumeByNBrick");

            var xVariable = new XElement("volume");
            xVariable.Add(_volume.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeVolumeByBrick();
            newBrick._volume = _volume.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeVolumeByBrick;

            if (otherBrick == null)
                return false;

            return Volume.Equals(otherBrick.Volume);
        }
    }
}