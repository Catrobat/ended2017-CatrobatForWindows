using System.Collections.ObjectModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlUserVariableList : DataObject
    {
        public ObservableCollection<XmlUserVariable> UserVariables;

        public XmlUserVariableList() {UserVariables = new ObservableCollection<XmlUserVariable>();}

        public XmlUserVariableList(XElement xElement)
        {
            UserVariables = new ObservableCollection<XmlUserVariable>();
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                UserVariables.Add(new XmlUserVariable(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("list");

            foreach (XmlUserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newUserVariableList = new XmlUserVariableList();

            foreach (var userVariable in UserVariables)
                newUserVariableList.UserVariables.Add(userVariable.Copy() as XmlUserVariable);

            return newUserVariableList;
        }

        public override bool Equals(DataObject other)
        {
            var otherUserVariableList = other as XmlUserVariableList;

            if (otherUserVariableList == null)
                return false;

            var count = UserVariables.Count;
            var otherCount = otherUserVariableList.UserVariables.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!(UserVariables[i].Equals(otherUserVariableList.UserVariables[i])))
                    return false;

            return true;
        }
    }
}
