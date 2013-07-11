using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
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

        public ChangeGhostEffectBrick(Sprite parent) : base(parent) {}

        public ChangeGhostEffectBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeGhostEffectBrick(parent);
            newBrick._changeGhostEffect = _changeGhostEffect.Copy(parent) as Formula;

            return newBrick;
        }
    }
}