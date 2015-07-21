using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlStartScript : XmlScript
    {
        public XmlStartScript() {}

        public XmlStartScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("startScript");
            var xRoot = new XElement(XmlConstants.Script);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlStartScriptType);

            CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}