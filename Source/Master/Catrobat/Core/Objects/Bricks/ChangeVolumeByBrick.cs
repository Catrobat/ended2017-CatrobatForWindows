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

        public ChangeVolumeByBrick(Sprite parent) : base(parent) {}

        public ChangeVolumeByBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeVolumeByBrick(parent);
            newBrick._volume = _volume.Copy(parent) as Formula;

            return newBrick;
        }
    }
}