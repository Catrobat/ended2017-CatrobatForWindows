using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetGhostEffectBrick : XmlBrick
    {
        public XmlFormula Transparency { get; set; }

        public XmlSetGhostEffectBrick() {}

        public XmlSetGhostEffectBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            Transparency = new XmlFormula(xRoot.Element("transparency"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("setGhostEffectBrick");

            var xVariable = new XElement("transparency");
            xVariable.Add(Transparency.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Transparency != null)
                Transparency.LoadReference();
        }
    }
}