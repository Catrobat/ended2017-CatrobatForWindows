using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlWaitBrick : XmlBrick
    {
        public XmlFormula TimeToWaitInSeconds { get; set; }

        public XmlWaitBrick() {}

        public XmlWaitBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            TimeToWaitInSeconds = new XmlFormula(xRoot.Element("timeToWaitInSeconds"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("waitBrick");

            var xVariable = new XElement("timeToWaitInSeconds");
            xVariable.Add(TimeToWaitInSeconds.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (TimeToWaitInSeconds != null)
                TimeToWaitInSeconds.LoadReference();
        }
    }
}