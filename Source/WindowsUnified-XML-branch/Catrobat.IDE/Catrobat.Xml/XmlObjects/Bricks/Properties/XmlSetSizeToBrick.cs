using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetSizeToBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlSetSizeToBrick() {}

        public XmlSetSizeToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Size = new XmlFormula(xRoot.Element("size"));
            Size = new XmlFormula(xRoot.Element(XmlConstants.Size));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setSizeToBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setSizeToBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetSizeToBrickType);

            //var xVariable = new XElement("size");
            var xVariable = new XElement(XmlConstants.Size);
            xVariable.Add(Size.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Size != null)
                Size.LoadReference();
        }
    }
}