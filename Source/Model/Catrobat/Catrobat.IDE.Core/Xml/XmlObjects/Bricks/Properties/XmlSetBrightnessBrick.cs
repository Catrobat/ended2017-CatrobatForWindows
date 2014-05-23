using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetBrightnessBrick : XmlBrick
    {
        protected XmlFormula _brightness;
        public XmlFormula Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                RaisePropertyChanged();
            }
        }


        public XmlSetBrightnessBrick() {}

        public XmlSetBrightnessBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _brightness = new XmlFormula(xRoot.Element("brightness"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setBrightnessBrick");

            var xVariable = new XElement("brightness");
            xVariable.Add(_brightness.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_brightness != null)
                _brightness.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlSetBrightnessBrick();
            newBrick._brightness = _brightness.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSetBrightnessBrick;

            if (otherBrick == null)
                return false;

            return Brightness.Equals(otherBrick.Brightness);
        }
    }
}