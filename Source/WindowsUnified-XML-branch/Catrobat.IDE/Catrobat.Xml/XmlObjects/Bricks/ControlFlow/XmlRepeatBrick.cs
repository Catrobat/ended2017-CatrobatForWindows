using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlRepeatBrick : XmlLoopBeginBrick
    {
        public XmlFormula TimesToRepeat { get; set; }

        public XmlRepeatBrick() {}

        public XmlRepeatBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //TODO: LoadFromCommonXML needs references etc.
            TimesToRepeat = new XmlFormula(xRoot.Element(XmlConstants.TimesToRepeat));
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Brick, XmlConstants.XmlRepeatBrickType);
            base.CreateCommonXML(xRoot);
            
            var xElement = TimesToRepeat.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.TimesToRepeat);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);
            
            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            base.LoadReference();

            if (TimesToRepeat != null)
                TimesToRepeat.LoadReference();
        }
    }
}