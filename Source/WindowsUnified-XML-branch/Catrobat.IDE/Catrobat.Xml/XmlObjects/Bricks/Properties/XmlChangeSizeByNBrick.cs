using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeSizeByNBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlChangeSizeByNBrick() {}

        public XmlChangeSizeByNBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Size = new XmlFormula(xRoot.Element("size"));
            Size = new XmlFormula(xRoot.Element(XmlConstants.SizeChange));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("changeSizeByNBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "changeSizeByNBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeSizeByNBrickType);

            //var xVariable = new XElement("size");
            var xVariable = new XElement(XmlConstants.SizeChange);
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