using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeGhostEffectBrick : XmlBrick
    {
        public XmlFormula ChangeGhostEffect { get; set; }

        public XmlChangeGhostEffectBrick() {}

        public XmlChangeGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            ChangeGhostEffect = new XmlFormula(xRoot.Element(XmlConstants.ChangeGhostEffect));
        }

        internal override XElement CreateXml()
        {
           var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeGhostEffectBrickType);

            var xElement = ChangeGhostEffect.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.ChangeGhostEffect);
            xRoot.Add(xElement);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (ChangeGhostEffect != null)
                ChangeGhostEffect.LoadReference();
        }
    }
}