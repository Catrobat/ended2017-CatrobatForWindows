using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetBrightnessBrick : Brick
    {
        protected double brightness = 0.0f;

        public SetBrightnessBrick()
        {
        }

        public SetBrightnessBrick(Sprite parent) : base(parent)
        {
        }

        public SetBrightnessBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Brightness
        {
            get { return brightness; }
            set
            {
                brightness = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Brightness"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            brightness = double.Parse(xRoot.Element("brightness").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setBrightnessBrick");

            xRoot.Add(new XElement("brightness")
                {
                    Value = brightness.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetBrightnessBrick(parent);
            newBrick.brightness = brightness;

            return newBrick;
        }
    }
}