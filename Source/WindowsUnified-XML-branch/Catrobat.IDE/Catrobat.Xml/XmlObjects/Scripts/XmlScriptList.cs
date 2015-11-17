using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

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
            if (xRoot != null)
            {
                foreach (XElement element in xRoot.Elements())
                {
                    switch (element.Attribute(XmlConstants.Type).Value.ToString())
                    {
                        case XmlConstants.XmlStartScriptType:
                            Scripts.Add(new XmlStartScript(element));
                            break;
                        case XmlConstants.XmlWhenScriptType:
                            Scripts.Add(new XmlWhenScript(element));
                            break;
                        case XmlConstants.XmlBroadcastScriptType:
                            Scripts.Add(new XmlBroadcastScript(element));
                            break;
                    }
                }
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.ScriptList);

            foreach (XmlScript script in Scripts)
            {
                XmlParserTempProjectHelper.currentScriptNum++;
                XmlParserTempProjectHelper.currentBrickNum = 0;
                XmlParserTempProjectHelper.currentVariableNum = 0;
                xRoot.Add(script.CreateXml());
            }

            return xRoot;
        }
    }
}