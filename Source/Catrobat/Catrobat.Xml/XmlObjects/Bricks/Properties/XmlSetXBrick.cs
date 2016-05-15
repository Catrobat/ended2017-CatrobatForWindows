using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetXBrick : XmlBrick, ISetXBrick
    {
        #region NativeInterface
        public IFormulaTree PositionX
        {
            get
            {
                return XPosition == null ? null : XPosition.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula XPosition { get; set; }

        public XmlSetXBrick() { }

        public XmlSetXBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                XPosition = new XmlFormula(xRoot, XmlConstants.XPosition);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetXBrickType);

            var xElement = XPosition.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.XPosition);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
        }
    }
}
