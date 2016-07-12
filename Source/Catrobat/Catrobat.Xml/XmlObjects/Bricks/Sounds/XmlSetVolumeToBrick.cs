using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSetVolumeToBrick : XmlBrick
    {
        public XmlFormula Volume { get; set; }

        public XmlSetVolumeToBrick() {}

        public XmlSetVolumeToBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlSetVolumeToBrick b = obj as XmlSetVolumeToBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Volume.Equals(b.Volume);
        }

        public bool Equals(XmlSetVolumeToBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Volume.Equals(b.Volume);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Volume.GetHashCode();
        }
        #endregion


        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Volume = new XmlFormula(xRoot, XmlConstants.Volume);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVolumeToBrickType);

            var xElement = Volume.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Volume);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Volume != null)
                Volume.LoadReference();
        }
    }
}
