using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
//using System.Collections.Generic;
//using System.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPlaceAtBrick : XmlBrick
    {
        public XmlFormula XPosition { get; set; }

        public XmlFormula YPosition { get; set; }

        public XmlPlaceAtBrick() {}

        public XmlPlaceAtBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                YPosition = XmlFormulaTreeFactory.getFormula(xRoot, XmlConstants.YPosition);
                XPosition = XmlFormulaTreeFactory.getFormula(xRoot, XmlConstants.XPosition);
                //IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
                //foreach (XElement xElement in elements)
                //{
                //    if (xElement.Attribute(XmlConstants.Category).Value == XmlConstants.XPosition)
                //        XPosition = new XmlFormula(xElement);
                //    else if (xElement.Attribute(XmlConstants.Category).Value == XmlConstants.YPosition)
                //        YPosition = new XmlFormula(xElement);

                //}
                
            }
            

             //XPosition = new XmlFormula(e.Element());
            //YPosition = new XmlFormula(xRoot.Element(XmlConstants.YPosition));
            
            /*IEnumerable<XElement> XElemensWhichShouldOnlyBeOne =
                from el in xRoot.Elements(XmlConstants.Formula)
                where (string)el.Attribute(XmlConstants.Category) == XmlConstants.YPosition
                select el;
            foreach (XElement el in XElemensWhichShouldOnlyBeOne)
                XPosition = new XmlFormula(el); //i know its not cute - but it is safe!

            XElemensWhichShouldOnlyBeOne =
                from el in xRoot.Elements(XmlConstants.Formula)
                where (string)el.Attribute(XmlConstants.Category) == XmlConstants.YPosition
                select el;
            foreach (XElement el in XElemensWhichShouldOnlyBeOne)
                YPosition = new XmlFormula(el); //i know its not cute - but it is safe!*/
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPlaceAtBrickType);

            var xElementY = YPosition.CreateXml();
            xElementY.SetAttributeValue(XmlConstants.Category, XmlConstants.YPosition);

            var xElementX = XPosition.CreateXml();
            xElementX.SetAttributeValue(XmlConstants.Category, XmlConstants.XPosition);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElementY);
            xFormulalist.Add(xElementX);

            xRoot.Add(xFormulalist);

            

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}