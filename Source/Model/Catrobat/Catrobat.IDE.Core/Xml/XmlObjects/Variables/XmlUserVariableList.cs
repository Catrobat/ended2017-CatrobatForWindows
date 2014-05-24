using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlUserVariableList : XmlObject
    {
        public ObservableCollection<XmlUserVariable> UserVariables;

        public XmlUserVariableList() {UserVariables = new ObservableCollection<XmlUserVariable>();}

        public XmlUserVariableList(XElement xElement)
        {
            UserVariables = new ObservableCollection<XmlUserVariable>();
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                UserVariables.Add(new XmlUserVariable(element));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("list");

            foreach (XmlUserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXml());
            }

            return xRoot;
        }

        public XmlObject Copy()
        {
            var newUserVariableList = new XmlUserVariableList();

            foreach (var userVariable in UserVariables)
                newUserVariableList.UserVariables.Add(userVariable.Copy() as XmlUserVariable);

            return newUserVariableList;
        }

        public override bool Equals(XmlObject other)
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
