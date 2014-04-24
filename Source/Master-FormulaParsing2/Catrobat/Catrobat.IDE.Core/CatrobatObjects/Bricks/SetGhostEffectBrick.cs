using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class SetGhostEffectBrick : Brick
    {
        protected Formula _transparency;
        public Formula Transparency
        {
            get { return _transparency; }
            set
            {
                _transparency = value;
                RaisePropertyChanged();
            }
        }


        public SetGhostEffectBrick() {}

        public SetGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _transparency = new Formula(xRoot.Element("transparency"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setGhostEffectBrick");

            var xVariable = new XElement("transparency");
            xVariable.Add(_transparency.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_transparency != null)
                _transparency.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new SetGhostEffectBrick();
            newBrick._transparency = _transparency.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetGhostEffectBrick;

            if (otherBrick == null)
                return false;

            return Transparency.Equals(otherBrick.Transparency);
        }
    }
}