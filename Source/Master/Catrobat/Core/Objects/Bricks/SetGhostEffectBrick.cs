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

        public override DataObject Copy()
        {
            var newBrick = new SetGhostEffectBrick();
            newBrick._transparency = _transparency.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}