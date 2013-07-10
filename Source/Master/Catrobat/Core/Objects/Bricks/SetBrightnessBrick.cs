using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetBrightnessBrick : Brick
    {
        protected double _brightness = 0.0f;
        public double Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                RaisePropertyChanged();
            }
        }


        public SetBrightnessBrick() {}

        public SetBrightnessBrick(Sprite parent) : base(parent) {}

        public SetBrightnessBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _brightness = double.Parse(xRoot.Element("brightness").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setBrightnessBrick");

            xRoot.Add(new XElement("brightness")
            {
                Value = _brightness.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetBrightnessBrick(parent);
            newBrick._brightness = _brightness;

            return newBrick;
        }
    }
}