using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetXBrick : XmlBrick
    {
        public XmlFormula XPosition { get; set; }

        public XmlSetXBrick() {}

        public XmlSetXBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            XPosition = new XmlFormula(xRoot.Element(XmlConstants.XPosition));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetXBrickType);

            var xVariable = new XElement(XmlConstants.XPosition);
            xVariable.Add(XPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
        }
    }
}