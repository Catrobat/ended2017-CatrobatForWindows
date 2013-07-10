using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetVolumeToBrick : Brick
    {
        protected double _volume = 100.0f;

        public SetVolumeToBrick() {}

        public SetVolumeToBrick(Sprite parent) : base(parent) {}

        public SetVolumeToBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _volume = double.Parse(xRoot.Element("volume").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setVolumeToBrick");

            xRoot.Add(new XElement("volume")
            {
                Value = _volume.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetVolumeToBrick(parent);
            newBrick._volume = _volume;

            return newBrick;
        }
    }
}