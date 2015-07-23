using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlWhenScript : XmlScript
    {
        public enum WhenScriptAction
        {
            Tapped
        }

        private static readonly Dictionary<WhenScriptAction, string> ActionStringDictionary = new Dictionary
            <WhenScriptAction, string>
        {
            {WhenScriptAction.Tapped, "Tapped"}
        };

        private static readonly Dictionary<string, WhenScriptAction> StringActionDictionary = new Dictionary
            <string, WhenScriptAction>
        {
            {"Tapped", WhenScriptAction.Tapped}
            //TODO:{XmlConstants.Tapped, WhenScriptAction.Tapped}
        };

        private string _action;

        public WhenScriptAction Action
        {
            get { return StringActionDictionary[_action]; }
            set { _action = ActionStringDictionary[value]; }
        }


        public XmlWhenScript() {}

        public XmlWhenScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element(XmlConstants.Action) != null)
            {
                _action = xRoot.Element(XmlConstants.Action).Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.XmlWhenScriptType);

            CreateCommonXML(xRoot);

            if (_action != null)
            {
                xRoot.Add(new XElement(XmlConstants.Action)
                {
                    Value = _action
                });
            }

            return xRoot;
        }
    }
}