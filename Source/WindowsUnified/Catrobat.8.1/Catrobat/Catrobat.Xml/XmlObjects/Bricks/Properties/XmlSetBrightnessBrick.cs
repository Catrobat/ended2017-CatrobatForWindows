using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetBrightnessBrick : XmlBrick
    {
        public XmlFormula Brightness { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlSetBrightnessBrick b = obj as XmlSetBrightnessBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Brightness.Equals(b.Brightness);
        }

        public bool Equals(XmlSetBrightnessBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Brightness.Equals(b.Brightness);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Brightness.GetHashCode();
        }

        public XmlSetBrightnessBrick()
        {
        }

        public XmlSetBrightnessBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Brightness = new XmlFormula(xRoot, XmlConstants.Brightness);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetBrightnessBrickType);

            var xElement = Brightness.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Brightness);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Brightness != null)
                Brightness.LoadReference();
        }
    }
}
