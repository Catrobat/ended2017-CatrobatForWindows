using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetYBrick : XmlBrick, ISetYBrick
    {
        #region NativeInterface
        public IFormulaTree PositionY
        {
            get
            {
                return YPosition == null ? null : YPosition.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula YPosition { get; set; }

        public XmlSetYBrick() {}

        public XmlSetYBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlSetYBrick b = obj as XmlSetYBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.YPosition.Equals(b.YPosition);
        }

        public bool Equals(XmlSetYBrick b)
        {
            return this.Equals((XmlBrick)b) && this.YPosition.Equals(b.YPosition);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ YPosition.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                YPosition = new XmlFormula(xRoot, XmlConstants.YPosition);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetYBrickType);

            var xElement = YPosition.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.YPosition);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}
