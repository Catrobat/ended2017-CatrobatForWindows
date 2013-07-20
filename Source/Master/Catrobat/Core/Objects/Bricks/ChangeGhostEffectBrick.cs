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

        public override DataObject Copy()
        {
            var newBrick = new ChangeGhostEffectBrick();
            newBrick._changeGhostEffect = _changeGhostEffect.Copy() as Formula;

            return newBrick;
        }
    }
}