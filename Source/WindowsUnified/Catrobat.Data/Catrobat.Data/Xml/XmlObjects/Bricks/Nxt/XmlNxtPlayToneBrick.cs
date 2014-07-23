using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Nxt
{
    public partial class XmlNxtPlayToneBrick : XmlBrick
    {
        public XmlFormula DurationInSeconds { get; set; }

        public XmlFormula Frequency { get; set; }

        public XmlNxtPlayToneBrick() {}

        public XmlNxtPlayToneBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            DurationInSeconds = new XmlFormula(xRoot.Element("durationInSeconds"));
            Frequency = new XmlFormula(xRoot.Element("frequency"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtPlayToneBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(DurationInSeconds.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("frequency");
            xVariable2.Add(Frequency.CreateXml());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (DurationInSeconds != null)
                DurationInSeconds.LoadReference();
            if (Frequency != null)
                Frequency.LoadReference();
        }
    }
}