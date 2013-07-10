using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeBrightnessBrick : Brick
    {
        protected double _changeBrightness = 25.0f;

        public ChangeBrightnessBrick() {}

        public ChangeBrightnessBrick(Sprite parent) : base(parent) {}

        public ChangeBrightnessBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public double ChangeBrightness
        {
            get { return _changeBrightness; }
            set
            {
                _changeBrightness = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _changeBrightness = double.Parse(xRoot.Element("changeBrightness").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeBrightnessBrick");

            xRoot.Add(new XElement("changeBrightness")
            {
                Value = _changeBrightness.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeBrightnessBrick(parent);
            newBrick._changeBrightness = _changeBrightness;

            return newBrick;
        }
    }
}