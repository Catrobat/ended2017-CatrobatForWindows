using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSetVolumeToBrick : XmlBrick, ISetVolumeToBrick
    {
        #region NativeInterface
        public IFormulaTree Volume
        {
            get
            {
                return VolumeXML == null ? null : VolumeXML.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula VolumeXML { get; set; }

        public XmlSetVolumeToBrick() { }

        public XmlSetVolumeToBrick(XElement xElement) : base(xElement) { }

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlSetVolumeToBrick b = obj as XmlSetVolumeToBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.VolumeXML.Equals(b.VolumeXML);
        }

        public bool Equals(XmlSetVolumeToBrick b)
        {
            return this.Equals((XmlBrick)b) && this.VolumeXML.Equals(b.VolumeXML);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ VolumeXML.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                VolumeXML = new XmlFormula(xRoot, XmlConstants.Volume);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetVolumeToBrickType);

            var xElement = VolumeXML.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Volume);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (VolumeXML != null)
                VolumeXML.LoadReference();
        }
    }
}
