using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SetVolumeToBrick : Brick
    {
        protected double volume = 100.0f;

        public SetVolumeToBrick()
        {
        }

        public SetVolumeToBrick(Sprite parent) : base(parent)
        {
        }

        public SetVolumeToBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Volume"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            volume = double.Parse(xRoot.Element("volume").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setVolumeToBrick");

            xRoot.Add(new XElement("volume")
                {
                    Value = volume.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetVolumeToBrick(parent);
            newBrick.volume = volume;

            return newBrick;
        }
    }
}