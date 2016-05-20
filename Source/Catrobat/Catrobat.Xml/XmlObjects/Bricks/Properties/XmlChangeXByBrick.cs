using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeXByBrick : XmlBrick, IChangeXByBrick
    {
        #region NativeInterface
        public IFormulaTree OffsetX
        {
            get
            {
                return XMovement == null ? null : XMovement.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula XMovement { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlChangeXByBrick b = obj as XmlChangeXByBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.XMovement.Equals(b.XMovement);
        }

        public bool Equals(XmlChangeXByBrick b)
        {
            return this.Equals((XmlBrick)b) && this.XMovement.Equals(b.XMovement);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ XMovement.GetHashCode();
        }

        public XmlChangeXByBrick() {}

        public XmlChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                XMovement = new XmlFormula(xRoot, XmlConstants.XPositionChange);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeXByBrickType);

            var xElement = XMovement.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.XPositionChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XMovement != null)
                XMovement.LoadReference();
        }
    }
}
