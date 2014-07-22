using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGlideToBrick : XmlBrick
    {
        public XmlFormula DurationInSeconds { get; set; }

        public XmlFormula XDestination { get; set; }

        public XmlFormula YDestination { get; set; }

        public XmlGlideToBrick() {}

        public XmlGlideToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            DurationInSeconds = new XmlFormula(xRoot.Element("durationInSeconds"));
            XDestination = new XmlFormula(xRoot.Element("xDestination"));
            YDestination = new XmlFormula(xRoot.Element("yDestination"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("glideToBrick");

            var xVariable1 = new XElement("durationInSeconds");
            xVariable1.Add(DurationInSeconds.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("xDestination");
            xVariable2.Add(XDestination.CreateXml());
            xRoot.Add(xVariable2);

            var xVariable3 = new XElement("yDestination");
            xVariable3.Add(YDestination.CreateXml());
            xRoot.Add(xVariable3);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (DurationInSeconds != null)
                DurationInSeconds.LoadReference();
            if (XDestination != null)
                XDestination.LoadReference();
            if (YDestination != null)
                YDestination.LoadReference();
        }
    }
}