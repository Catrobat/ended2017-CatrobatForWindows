using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class ScriptList : DataObject
    {
        private readonly Sprite parentSprite;

        public ScriptList(Sprite parent)
        {
            Scripts = new ObservableCollection<Script>();
            parentSprite = parent;
        }

        public ScriptList(XElement xElement, Sprite parent)
        {
            parentSprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<Script> Scripts { get; set; }

        internal override void LoadFromXML(XElement xRoot)
        {
            Scripts = new ObservableCollection<Script>();
            foreach (XElement element in xRoot.Elements())
                switch (element.Name.LocalName)
                {
                    case "startScript":
                        Scripts.Add(new StartScript(element, parentSprite));
                        break;
                    case "whenScript":
                        Scripts.Add(new WhenScript(element, parentSprite));
                        break;
                    case "broadcastScript":
                        Scripts.Add(new BroadcastScript(element, parentSprite));
                        break;
                    default:
                        break;
                }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("scriptList");

            foreach (Script script in Scripts)
                xRoot.Add(script.CreateXML());

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newScriptList = new ScriptList(parent);
            foreach (Script script in Scripts)
                newScriptList.Scripts.Add(script.Copy(parent) as Script);

            return newScriptList;
        }

        public void CopyReference(Sprite copiedFrom, Sprite parent)
        {
            int pos = 0;
            foreach (Script script in Scripts)
            {
                script.CopyReference(copiedFrom.Scripts.Scripts[pos], parent);
                pos++;
            }
        }
    }
}