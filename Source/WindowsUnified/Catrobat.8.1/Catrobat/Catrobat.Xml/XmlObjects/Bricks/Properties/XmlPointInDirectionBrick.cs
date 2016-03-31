using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPointInDirectionBrick : XmlBrick
    {
        public XmlFormula Degrees { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlPointInDirectionBrick b = obj as XmlPointInDirectionBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Degrees.Equals(b.Degrees);
        }

        public bool Equals(XmlPointInDirectionBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Degrees.Equals(b.Degrees);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Degrees.GetHashCode();
        }

        public XmlPointInDirectionBrick() {}

        public XmlPointInDirectionBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Degrees = new XmlFormula(xRoot, XmlConstants.Degrees);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPointInDirectionBrickType);

            var xElement = Degrees.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Degrees);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Degrees != null)
                Degrees.LoadReference();
        }
    }
}
