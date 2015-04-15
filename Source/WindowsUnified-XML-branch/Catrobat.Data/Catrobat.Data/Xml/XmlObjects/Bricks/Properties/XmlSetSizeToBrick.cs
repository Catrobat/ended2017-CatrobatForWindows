using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetSizeToBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlSetSizeToBrick() {}

        public XmlSetSizeToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            Size = new XmlFormula(xRoot.Element("size"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setSizeToBrick");

            var xVariable = new XElement("size");
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