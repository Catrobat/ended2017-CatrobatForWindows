using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeGhostEffectBrick : XmlBrick
    {
        protected XmlFormula _changeGhostEffect;
        public XmlFormula ChangeGhostEffect
        {
            get { return _changeGhostEffect; }
            set
            {
                _changeGhostEffect = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeGhostEffectBrick() {}

        public XmlChangeGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _changeGhostEffect = new XmlFormula(xRoot.Element("changeGhostEffect"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeGhostEffectByNBrick");

            var xVariable = new XElement("changeGhostEffect");
            xVariable.Add(_changeGhostEffect.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_changeGhostEffect != null)
                _changeGhostEffect.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeGhostEffectBrick();
            newBrick._changeGhostEffect = _changeGhostEffect.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeGhostEffectBrick;

            if (otherBrick == null)
                return false;

            return ChangeGhostEffect.Equals(otherBrick.ChangeGhostEffect);
        }
    }
}