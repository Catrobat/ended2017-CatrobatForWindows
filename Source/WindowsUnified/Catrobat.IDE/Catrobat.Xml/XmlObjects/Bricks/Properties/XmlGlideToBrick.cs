using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGlideToBrick : XmlBrick
    {
        public XmlFormula DurationInSeconds { get; set; }

        public XmlFormula XDestination { get; set; }

        public XmlFormula YDestination { get; set; }

        public XmlGlideToBrick() {}

        public XmlGlideToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                DurationInSeconds = new XmlFormula(xRoot, XmlConstants.DurationInSeconds);
                XDestination = new XmlFormula(xRoot, XmlConstants.XDestination);
                YDestination = new XmlFormula(xRoot, XmlConstants.YDestination);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlGlideToBrickType);

            var xElementY = YDestination.CreateXml();
            xElementY.SetAttributeValue(XmlConstants.Category, XmlConstants.YDestination);
            
            var xElementX = XDestination.CreateXml();
            xElementX.SetAttributeValue(XmlConstants.Category, XmlConstants.XDestination);

            var xElementDuration = DurationInSeconds.CreateXml();
            xElementDuration.SetAttributeValue(XmlConstants.Category, XmlConstants.DurationInSeconds);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElementY);
            xFormulalist.Add(xElementX);
            xFormulalist.Add(xElementDuration);

            xRoot.Add(xFormulalist);

            

            return xRoot;
        }

        public override void LoadReference()
        {
            if (DurationInSeconds != null)
                DurationInSeconds.LoadReference();
            if (XDestination != null)
                XDestination.LoadReference();
            if (YDestination != null)
                YDestination.LoadReference();
        }
    }
}