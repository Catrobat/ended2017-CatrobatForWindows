using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeYByBrick : XmlBrick, IChangeYByBrick
    {
        #region NativeInterface
        public IFormulaTree OffsetY
        {
            get
            {
                return YMovement == null ? null : YMovement.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula YMovement { get; set; }

        public XmlChangeYByBrick() { }

        public XmlChangeYByBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                YMovement = new XmlFormula(xRoot, XmlConstants.YPositionChange);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeYByBrickType);

            var xElement = YMovement.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.YPositionChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (YMovement != null)
                YMovement.LoadReference();
        }
    }
}
