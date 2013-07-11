using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetBrightnessBrick : Brick
    {
        protected Formula _brightness;
        public Formula Brightness
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
            _brightness = new Formula(xRoot.Element("brightness"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setBrightnessBrick");

            var xVariable = new XElement("brightness");
            xVariable.Add(_brightness.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetBrightnessBrick(parent);
            newBrick._brightness = _brightness.Copy(parent) as Formula;

            return newBrick;
        }
    }
}