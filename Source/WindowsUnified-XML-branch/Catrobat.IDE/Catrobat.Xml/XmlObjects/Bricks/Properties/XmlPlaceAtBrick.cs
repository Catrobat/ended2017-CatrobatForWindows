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
            XPosition = new XmlFormula(xRoot.Element(XmlConstants.XPosition));
            YPosition = new XmlFormula(xRoot.Element(XmlConstants.YPosition));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPlaceAtBrickType);

            var xElement1 = YPosition.CreateXml();
            xElement1.SetAttributeValue(XmlConstants.Category, XmlConstants.YPosition);
            xRoot.Add(xElement1);

            var xElement2 = XPosition.CreateXml();
            xElement2.SetAttributeValue(XmlConstants.Category, XmlConstants.XPosition);
            xRoot.Add(xElement2);

            

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