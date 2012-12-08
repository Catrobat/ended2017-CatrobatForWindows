using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

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

        private string action;

        public WhenScript()
        {
        }

        public WhenScript(Sprite parent) : base(parent)
        {
        }

        public WhenScript(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public WhenScriptAction Action
        {
            get { return StringActionDictionary[action]; }
            set
            {
                string stringValue = ActionStringDictionary[value];

                if (action == stringValue)
                    return;

                action = stringValue;
                OnPropertyChanged(new PropertyChangedEventArgs("Action"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("action") != null)
                action = xRoot.Element("action").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("whenScript");

            CreateCommonXML(xRoot);

            if (action != null)
            {
                xRoot.Add(new XElement("action")
                    {
                        Value = action
                    });
            }

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newWhenScript = new WhenScript(parent);
            newWhenScript.action = action;
            if (bricks != null)
                newWhenScript.bricks = bricks.Copy(parent) as BrickList;

            return newWhenScript;
        }
    }
}