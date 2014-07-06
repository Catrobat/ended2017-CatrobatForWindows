using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlRepeatBrick : XmlLoopBeginBrick
    {
        public XmlFormula TimesToRepeat { get; set; }

        public XmlRepeatBrick() {}

        public XmlRepeatBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            TimesToRepeat = new XmlFormula(xRoot.Element("timesToRepeat"));
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("repeatBrick");
            base.CreateCommonXML(xRoot);

            var xVariable = new XElement("timesToRepeat");
            xVariable.Add(TimesToRepeat.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            base.LoadReference();

            if (TimesToRepeat != null)
                TimesToRepeat.LoadReference();
        }
    }
}