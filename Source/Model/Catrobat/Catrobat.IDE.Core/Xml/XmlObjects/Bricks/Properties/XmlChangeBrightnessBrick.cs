using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeBrightnessBrick : XmlBrick
    {
        protected XmlFormula _changeBrightness;
        public XmlFormula ChangeBrightness
        {
            get { return _changeBrightness; }
            set
            {
                _changeBrightness = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeBrightnessBrick() { }

        public XmlChangeBrightnessBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            _changeBrightness = new XmlFormula(xRoot.Element("changeBrightness"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeBrightnessByNBrick");

            var xVariable = new XElement("changeBrightness");
            xVariable.Add(_changeBrightness.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_changeBrightness != null)
                _changeBrightness.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeBrightnessBrick();
            newBrick._changeBrightness = _changeBrightness.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeBrightnessBrick;

            if (otherBrick == null)
                return false;

            return ChangeBrightness.Equals(otherBrick.ChangeBrightness);
        }
    }
}
