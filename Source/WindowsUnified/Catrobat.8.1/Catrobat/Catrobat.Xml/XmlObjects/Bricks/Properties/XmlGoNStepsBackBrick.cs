using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGoNStepsBackBrick : XmlBrick
    {
        public XmlFormula Steps { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlGoNStepsBackBrick b = obj as XmlGoNStepsBackBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Steps.Equals(b.Steps);
        }

        public bool Equals(XmlGoNStepsBackBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Steps.Equals(b.Steps);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Steps.GetHashCode();
        }

        public XmlGoNStepsBackBrick()
        {
        }

        public XmlGoNStepsBackBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Steps = new XmlFormula(xRoot, XmlConstants.Steps);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlGoNStepsBackBrickType);
            
            var xElement = Steps.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Steps);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}
