using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.Interpreter.Objects.Bricks;

namespace Catrobat.Interpreter.Objects.Scripts
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
                RaisePropertyChanged();
            }
        }


        public WhenScript() {}

        public WhenScript(XElement xElement) : base(xElement) {}

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

        public override DataObject Copy()
        {
            var newWhenScript = new WhenScript();
            newWhenScript._action = _action;
            if (_bricks != null)
            {
                newWhenScript._bricks = _bricks.Copy() as BrickList;
            }

            return newWhenScript;
        }

        public override bool Equals(DataObject other)
        {
            var otherScript = other as WhenScript;

            if (otherScript == null)
                return false;

            if (Action != otherScript.Action)
                return false;

            return Bricks.Equals(otherScript.Bricks);
        }
    }
}