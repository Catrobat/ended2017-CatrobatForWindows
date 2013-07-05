using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;

namespace Catrobat.Core.Objects
{
    public class WhenScript : Script
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
        };

        private string _action;

        public WhenScript() {}

        public WhenScript(Sprite parent) : base(parent) {}

        public WhenScript(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public WhenScriptAction Action
        {
            get { return StringActionDictionary[_action]; }
            set
            {
                var stringValue = ActionStringDictionary[value];

                if (_action == stringValue)
                {
                    return;
                }

                _action = stringValue;
                OnPropertyChanged(new PropertyChangedEventArgs("Action"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("action") != null)
            {
                _action = xRoot.Element("action").Value;
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("whenScript");

            CreateCommonXML(xRoot);

            if (_action != null)
            {
                xRoot.Add(new XElement("action")
                {
                    Value = _action
                });
            }

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newWhenScript = new WhenScript(parent);
            newWhenScript._action = _action;
            if (bricks != null)
            {
                newWhenScript.bricks = bricks.Copy(parent) as BrickList;
            }

            return newWhenScript;
        }
    }
}