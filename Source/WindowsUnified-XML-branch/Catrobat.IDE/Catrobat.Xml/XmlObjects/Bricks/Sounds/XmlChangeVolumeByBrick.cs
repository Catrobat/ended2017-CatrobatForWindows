using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlChangeVolumeByBrick : XmlBrick
    {
        public XmlFormula Volume { get; set; }

        public XmlChangeVolumeByBrick() {}

        public XmlChangeVolumeByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Volume = new XmlFormula(xRoot.Element("volume"));
            Volume = new XmlFormula(xRoot.Element(XmlConstants.Volume));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("changeVolumeByNBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "changeVolumeByNBrick ");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeVolumeByBricksType);

            //var xVariable = new XElement("volume");
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