using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlChangeVolumeByBrick : XmlBrick, IChangeVolumeByNBrick
    {
        public IFormulaTree Volume
        {
            get
            {
                return VolumeXML == null ? null : VolumeXML.FormulaTree;
            }
            set { }
        }

        public XmlFormula VolumeXML { get; set; }

        public XmlChangeVolumeByBrick() { }

        public XmlChangeVolumeByBrick(XElement xElement) : base(xElement) { }

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
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeVolumeByBricksType);

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
