using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeGhostEffectBrick : XmlBrick
    {
        public XmlFormula ChangeGhostEffect { get; set; }

        public XmlChangeGhostEffectBrick() {}

        public XmlChangeGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
                foreach (XElement xElement in elements)
                {
                    if (xElement.Attribute(XmlConstants.Category).Value == XmlConstants.ChangeGhostEffect)
                        ChangeGhostEffect = new XmlFormula(xElement);

                }
                //ChangeGhostEffect = new XmlFormula(xRoot.Element(XmlConstants.ChangeGhostEffect));
            }
            
        }

        internal override XElement CreateXml()
        {
           var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeGhostEffectBrickType);

            var xElement = ChangeGhostEffect.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.ChangeGhostEffect);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (ChangeGhostEffect != null)
                ChangeGhostEffect.LoadReference();
        }
    }
}