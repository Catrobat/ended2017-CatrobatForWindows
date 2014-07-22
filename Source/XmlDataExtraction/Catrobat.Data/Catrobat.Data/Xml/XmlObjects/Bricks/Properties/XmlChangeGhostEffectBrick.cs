using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeGhostEffectBrick : XmlBrick
    {
        public XmlFormula ChangeGhostEffect { get; set; }

        public XmlChangeGhostEffectBrick() {}

        public XmlChangeGhostEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            ChangeGhostEffect = new XmlFormula(xRoot.Element("changeGhostEffect"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeGhostEffectByNBrick");

            var xVariable = new XElement("changeGhostEffect");
            xVariable.Add(ChangeGhostEffect.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (ChangeGhostEffect != null)
                ChangeGhostEffect.LoadReference();
        }
    }
}