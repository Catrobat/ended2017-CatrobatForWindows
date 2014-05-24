using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public class XmlScriptList : XmlObject
    {
        public ObservableCollection<XmlScript> Scripts { get; set; }

        public int ActionCount
        {
            get
            {
                return Scripts.Sum(script => script.Bricks.Bricks.Count + 1);
            }
        }

        public XmlScriptList()
        {
            Scripts = new ObservableCollection<XmlScript>();
            Scripts.CollectionChanged += ScriptsOnCollectionChanged;
        }

        public XmlScriptList(XElement xElement)
        {
            Scripts = new ObservableCollection<XmlScript>();
            Scripts.CollectionChanged += ScriptsOnCollectionChanged;
            LoadFromXml(xElement);
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

        public XmlObject Copy()
        {
            var newScriptList = new XmlScriptList();
            foreach (XmlScript script in Scripts)
            {
                newScriptList.Scripts.Add(script.Copy() as XmlScript);
            }

            return newScriptList;
        }

        public override bool Equals(XmlObject other)
        {
            var otherScriptList = other as XmlScriptList;

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