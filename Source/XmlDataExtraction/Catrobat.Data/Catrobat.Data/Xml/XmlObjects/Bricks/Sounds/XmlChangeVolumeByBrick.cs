using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlChangeVolumeByBrick : XmlBrick
    {
        public XmlFormula Volume { get; set; }

        public XmlChangeVolumeByBrick() {}

        public XmlChangeVolumeByBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            Volume = new XmlFormula(xRoot.Element("volume"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("changeVolumeByNBrick");

            var xVariable = new XElement("volume");
            xVariable.Add(Volume.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Volume != null)
                Volume.LoadReference();
        }
    }
}