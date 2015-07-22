using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public class XmlScriptList : XmlObjectNode
    {
        public List<XmlScript> Scripts { get; set; }

        public XmlScriptList()
        {
            Scripts = new List<XmlScript>();
        }

        public XmlScriptList(XElement xElement)
        {
            Scripts = new List<XmlScript>();
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            foreach (XElement element in xRoot.Elements())
            {
                //switch (element.Name.LocalName)
                switch (element.Attribute(XmlConstants.Type).Value.ToString())
                {
                    //case "StartScript":
                    case XmlConstants.XmlStartScriptType:
                        Scripts.Add(new XmlStartScript(element));
                        break;
                    //case "whenScript":
                    case XmlConstants.XmlWhenScriptType:
                        Scripts.Add(new XmlWhenScript(element));
                        break;
                    //case "broadcastScript":
                    case XmlConstants.XmlBroadcastScriptType:
                        Scripts.Add(new XmlBroadcastScript(element));
                        break;
                }
            }
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("scriptList");
            var xRoot = new XElement(XmlConstants.ScriptList);

            foreach (XmlScript script in Scripts)
            {
                xRoot.Add(script.CreateXml());
            }

            return xRoot;
        }
    }
}