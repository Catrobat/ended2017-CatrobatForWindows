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
                YPosition = new XmlFormula(xRoot, XmlConstants.YPosition);
                XPosition = new XmlFormula(xRoot, XmlConstants.XPosition);           
            }
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

        public override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}