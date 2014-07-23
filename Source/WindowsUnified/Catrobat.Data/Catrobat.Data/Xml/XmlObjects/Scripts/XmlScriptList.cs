using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Scripts
{
    public class XmlScriptList : XmlObject
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
                switch (element.Name.LocalName)
                {
                    case "startScript":
                        Scripts.Add(new XmlStartScript(element));
                        break;
                    case "whenScript":
                        Scripts.Add(new XmlWhenScript(element));
                        break;
                    case "broadcastScript":
                        Scripts.Add(new XmlBroadcastScript(element));
                        break;
                }
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("scriptList");

            foreach (XmlScript script in Scripts)
            {
                xRoot.Add(script.CreateXml());
            }

            return xRoot;
        }
    }
}