using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetYBrick : XmlBrick
    {
        public XmlFormula YPosition { get; set; }

        public XmlSetYBrick() {}

        public XmlSetYBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //YPosition = new XmlFormula(xRoot.Element("yPosition"));
            YPosition = new XmlFormula(xRoot.Element(XmlConstants.YPosition));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setYBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setYBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetYBrickType);

            //var xVariable = new XElement("yPosition");
            var xVariable = new XElement(XmlConstants.YPosition);
            xVariable.Add(YPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}