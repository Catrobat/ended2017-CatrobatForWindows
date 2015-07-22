using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPlaceAtBrick : XmlBrick
    {
        public XmlFormula XPosition { get; set; }

        public XmlFormula YPosition { get; set; }

        public XmlPlaceAtBrick() {}

        public XmlPlaceAtBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //XPosition = new XmlFormula(xRoot.Element("xPosition"));
            //YPosition = new XmlFormula(xRoot.Element("yPosition"));
            XPosition = new XmlFormula(xRoot.Element(XmlConstants.XPosition));
            YPosition = new XmlFormula(xRoot.Element(XmlConstants.YPosition));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("placeAtBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "placeAtBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPlaceAtBrickType);

            //var xVariable1 = new XElement("yPosition");
            var xVariable1 = new XElement(XmlConstants.YPosition);
            xVariable1.Add(YPosition.CreateXml());
            xRoot.Add(xVariable1);

            //var xVariable1 = new XElement("xPosition");
            var xVariable2 = new XElement(XmlConstants.XPosition);
            xVariable2.Add(XPosition.CreateXml());
            xRoot.Add(xVariable2);

            

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}