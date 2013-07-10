using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeGhostEffectBrick : Brick
    {
        protected double _changeGhostEffect = 25.0f;
        public double ChangeGhostEffect
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
            _changeGhostEffect = double.Parse(xRoot.Element("changeGhostEffect").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeGhostEffectByNBrick");

            xRoot.Add(new XElement("changeGhostEffect")
            {
                Value = _changeGhostEffect.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeGhostEffectBrick(parent);
            newBrick._changeGhostEffect = _changeGhostEffect;

            return newBrick;
        }
    }
}