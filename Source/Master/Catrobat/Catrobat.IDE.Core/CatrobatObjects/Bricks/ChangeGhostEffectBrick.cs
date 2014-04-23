using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class ChangeGhostEffectBrick : Brick
    {
        protected Formula _changeGhostEffect;
        public Formula ChangeGhostEffect
        {
            get { return _changeGhostEffect; }
            set
            {
                _changeGhostEffect = value;
                RaisePropertyChanged();
            }
        }


        public ChangeGhostEffectBrick() {}

        public ChangeGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _changeGhostEffect = new Formula(xRoot.Element("changeGhostEffect"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeGhostEffectByNBrick");

            var xVariable = new XElement("changeGhostEffect");
            xVariable.Add(_changeGhostEffect.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_changeGhostEffect != null)
                _changeGhostEffect.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeGhostEffectBrick();
            newBrick._changeGhostEffect = _changeGhostEffect.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeGhostEffectBrick;

            if (otherBrick == null)
                return false;

            return ChangeGhostEffect.Equals(otherBrick.ChangeGhostEffect);
        }
    }
}