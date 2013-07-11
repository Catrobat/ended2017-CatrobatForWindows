using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeBrightnessBrick : Brick
    {
        protected Formula _changeBrightness;
        public Formula ChangeBrightness
        {
            get { return _changeBrightness; }
            set
            {
                _changeBrightness = value;
                RaisePropertyChanged();
            }
        }


        public ChangeBrightnessBrick() { }

        public ChangeBrightnessBrick(Sprite parent) : base(parent) { }

        public ChangeBrightnessBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            _changeBrightness = new Formula(xRoot.Element("changeBrightness"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeBrightnessByNBrick");

            var xVariable = new XElement("changeBrightness");
            xVariable.Add(_changeBrightness.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeBrightnessBrick(parent);
            newBrick._changeBrightness = _changeBrightness.Copy(parent) as Formula;

            return newBrick;
        }
    }
}