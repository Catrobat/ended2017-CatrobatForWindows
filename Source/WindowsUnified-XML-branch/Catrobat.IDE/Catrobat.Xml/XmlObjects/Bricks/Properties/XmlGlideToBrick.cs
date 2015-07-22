using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
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
            //DurationInSeconds = new XmlFormula(xRoot.Element("durationInSeconds"));
            //XDestination = new XmlFormula(xRoot.Element("xDestination"));
            //YDestination = new XmlFormula(xRoot.Element("yDestination"));
            DurationInSeconds = new XmlFormula(xRoot.Element(XmlConstants.DurationInSeconds));
            XDestination = new XmlFormula(xRoot.Element(XmlConstants.XDestination));
            YDestination = new XmlFormula(xRoot.Element(XmlConstants.YDestination));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("glideToBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "glideToBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlGlideToBrickType);

            //var xVariable1 = new XElement("yDestination");
            var xVariable1 = new XElement(XmlConstants.YDestination);
            xVariable1.Add(YDestination.CreateXml());
            xRoot.Add(xVariable1);

            //var xVariable2 = new XElement("xDestination");
            var xVariable2 = new XElement(XmlConstants.XDestination);
            xVariable2.Add(XDestination.CreateXml());
            xRoot.Add(xVariable2);

            //var xVariable3 = new XElement("durationInSeconds");
            var xVariable3 = new XElement(XmlConstants.DurationInSeconds);
            xVariable3.Add(DurationInSeconds.CreateXml());
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