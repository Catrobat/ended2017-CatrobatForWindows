using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetGhostEffectBrick : XmlBrick
    {
        public XmlFormula Transparency { get; set; }

        public XmlSetGhostEffectBrick() {}

        public XmlSetGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Transparency = new XmlFormula(xRoot.Element("transparency"));
            Transparency = new XmlFormula(xRoot.Element(XmlConstants.Transparency));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setGhostEffectBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setGhostEffectBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetGhostEffectBrickType);

            //var xVariable = new XElement("transparency");
            var xVariable = new XElement(XmlConstants.Transparency);
            xVariable.Add(Transparency.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Transparency != null)
                Transparency.LoadReference();
        }
    }
}