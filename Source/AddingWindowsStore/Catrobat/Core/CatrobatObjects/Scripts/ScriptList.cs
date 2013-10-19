using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Scripts
{
    public class ScriptList : DataObject
    {
        public ObservableCollection<Script> Scripts { get; set; }

        public int ActionCount
        {
            get
            {
                return Scripts.Sum(script => script.Bricks.Bricks.Count + 1);
            }
        }

        public ScriptList()
        {
            Scripts = new ObservableCollection<Script>();
            Scripts.CollectionChanged += ScriptsOnCollectionChanged;
        }

        public ScriptList(XElement xElement)
        {
            Scripts = new ObservableCollection<Script>();
            Scripts.CollectionChanged += ScriptsOnCollectionChanged;
            LoadFromXML(xElement);
        }

        private void ScriptsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            RaisePropertyChanged(()=> ActionCount);

            foreach (var script in Scripts)
            {
                    script.Bricks.Bricks.CollectionChanged += BricksOnCollectionChanged;
            }
        }

        private void BricksOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            RaisePropertyChanged(() => ActionCount);
        }


        internal override void LoadFromXML(XElement xRoot)
        {
            foreach (XElement element in xRoot.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "startScript":
                        Scripts.Add(new StartScript(element));
                        break;
                    case "whenScript":
                        Scripts.Add(new WhenScript(element));
                        break;
                    case "broadcastScript":
                        Scripts.Add(new BroadcastScript(element));
                        break;
                }
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("scriptList");

            foreach (Script script in Scripts)
            {
                xRoot.Add(script.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newScriptList = new ScriptList();
            foreach (Script script in Scripts)
            {
                newScriptList.Scripts.Add(script.Copy() as Script);
            }

            return newScriptList;
        }

        public override bool Equals(DataObject other)
        {
            var otherScriptList = other as ScriptList;

            if (otherScriptList == null)
                return false;

            var count = Scripts.Count;
            var otherCount = otherScriptList.Scripts.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!Scripts[i].Equals(otherScriptList.Scripts[i]))
                    return false;

            return true;
        }
    }
}