using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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

        public SetBrightnessBrick(XElement xElement) : base(xElement) {}

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

        public override DataObject Copy()
        {
            var newBrick = new SetBrightnessBrick();
            newBrick._brightness = _brightness.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetBrightnessBrick;

            if (otherBrick == null)
                return false;

            return Brightness.Equals(otherBrick.Brightness);
        }
    }
}