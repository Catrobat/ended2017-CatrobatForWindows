using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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

        public ChangeBrightnessBrick(XElement xElement) : base(xElement) { }

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

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_changeBrightness != null)
                _changeBrightness.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeBrightnessBrick();
            newBrick._changeBrightness = _changeBrightness.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeBrightnessBrick;

            if (otherBrick == null)
                return false;

            return ChangeBrightness.Equals(otherBrick.ChangeBrightness);
        }
    }
}
