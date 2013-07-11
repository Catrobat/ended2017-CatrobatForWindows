using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
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

        public SetGhostEffectBrick(Sprite parent) : base(parent) {}

        public SetGhostEffectBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetGhostEffectBrick(parent);
            newBrick._transparency = _transparency.Copy(parent) as Formula;

            return newBrick;
        }
    }
}