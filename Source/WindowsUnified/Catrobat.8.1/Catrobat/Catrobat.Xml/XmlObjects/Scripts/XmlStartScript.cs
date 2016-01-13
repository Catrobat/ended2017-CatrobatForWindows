using Catrobat_Player.NativeComponent;
using System.Xml.Linq;
using System;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlStartScript : XmlScript, IStartScript
    {
        public XmlStartScript() {}

        public XmlStartScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("startScript");

            CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}