using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlWaitBrick : XmlBrick
    {
        public XmlFormula TimeToWaitInSeconds { get; set; }

        public XmlWaitBrick() {}

        public XmlWaitBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            TimeToWaitInSeconds = new XmlFormula(xRoot.Element(XmlConstants.TimeToWaitInSeconds));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlWaitBrickType);
            
            var xElement = TimeToWaitInSeconds.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.TimeToWaitInSeconds);
            xRoot.Add(xElement);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (TimeToWaitInSeconds != null)
                TimeToWaitInSeconds.LoadReference();
        }
    }
}