using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeGhostEffectBrick : XmlBrick, IChangeGhostEffectByBrick
    {
        #region NativeInterface
        public IFormulaTree Transparency
        {
            get
            {
                return ChangeGhostEffect == null ? null : ChangeGhostEffect.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula ChangeGhostEffect { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlChangeGhostEffectBrick b = obj as XmlChangeGhostEffectBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.ChangeGhostEffect.Equals(b.ChangeGhostEffect);
        }

        public bool Equals(XmlChangeGhostEffectBrick b)
        {
            return this.Equals((XmlBrick)b) && this.ChangeGhostEffect.Equals(b.ChangeGhostEffect);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ ChangeGhostEffect.GetHashCode();
        }

        public XmlChangeGhostEffectBrick() { }

        public XmlChangeGhostEffectBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                ChangeGhostEffect = new XmlFormula(xRoot, XmlConstants.ChangeGhostEffect);
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

        public override void LoadReference()
        {
            if (ChangeGhostEffect != null)
                ChangeGhostEffect.LoadReference();
        }
    }
}
