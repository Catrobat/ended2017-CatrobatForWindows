using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetGhostEffectBrick : XmlBrick, ISetGhostEffectBrick
    {
        #region NativeInterface
        IFormulaTree ISetGhostEffectBrick.Transparency
        {
            get
            {
                return Transparency == null ? null : Transparency.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula Transparency { get; set; }

        public XmlSetGhostEffectBrick() {}

        public XmlSetGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Transparency = new XmlFormula(xRoot, XmlConstants.Transparency);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetGhostEffectBrickType);

            var xElement = Transparency.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Transparency);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Transparency != null)
                Transparency.LoadReference();
        }
    }
}
