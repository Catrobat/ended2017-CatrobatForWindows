using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeSizeByNBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlChangeSizeByNBrick() {}

        public XmlChangeSizeByNBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {

            if (xRoot != null)
            {
                IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
                foreach (XElement xElement in elements)
                {
                    if (xElement.Attribute(XmlConstants.Category).Value == XmlConstants.SizeChange)
                        Size = new XmlFormula(xElement);

                }
                //Size = new XmlFormula(xRoot.Element(XmlConstants.SizeChange));
            }
            
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeSizeByNBrickType);                

            var xElement = Size.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.SizeChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Size != null)
                Size.LoadReference();
        }
    }
}