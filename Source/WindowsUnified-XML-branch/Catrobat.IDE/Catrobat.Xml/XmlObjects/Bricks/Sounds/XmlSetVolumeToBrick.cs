using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSetVolumeToBrick : XmlBrick
    {
        public XmlFormula Volume { get; set; }

        public XmlSetVolumeToBrick() {}

        public XmlSetVolumeToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            Volume = new XmlFormula(xRoot.Element(XmlConstants.Volume));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVolumeToBrickType);

            var xVariable = new XElement(XmlConstants.Volume);
            xVariable.Add(Volume.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Volume != null)
                Volume.LoadReference();
        }
    }
}