using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeSizeByNBrick : XmlBrick, IChangeSizeByBrick
    {
        #region NativeInterface
        public IFormulaTree Scale
        {
            get
            {
                return Size == null ? null : Size.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula Size { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlChangeSizeByNBrick b = obj as XmlChangeSizeByNBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Size.Equals(b.Size);
        }

        public bool Equals(XmlChangeSizeByNBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Size.Equals(b.Size);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Size.GetHashCode();
        }

        public XmlChangeSizeByNBrick() { }

        public XmlChangeSizeByNBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Size = new XmlFormula(xRoot, XmlConstants.SizeChange);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeSizeByNBrickType);

            var xElement = Size.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.SizeChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Size != null)
                Size.LoadReference();
        }
    }
}
