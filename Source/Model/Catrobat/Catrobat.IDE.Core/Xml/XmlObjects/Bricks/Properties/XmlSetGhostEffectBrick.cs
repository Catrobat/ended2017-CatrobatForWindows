using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetGhostEffectBrick : XmlBrick
    {
        protected XmlFormula _transparency;
        public XmlFormula Transparency
        {
            get { return _transparency; }
            set
            {
                _transparency = value;
                RaisePropertyChanged();
            }
        }


        public XmlSetGhostEffectBrick() {}

        public XmlSetGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _transparency = new XmlFormula(xRoot.Element("transparency"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setGhostEffectBrick");

            var xVariable = new XElement("transparency");
            xVariable.Add(_transparency.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_transparency != null)
                _transparency.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlSetGhostEffectBrick();
            newBrick._transparency = _transparency.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSetGhostEffectBrick;

            if (otherBrick == null)
                return false;

            return Transparency.Equals(otherBrick.Transparency);
        }
    }
}