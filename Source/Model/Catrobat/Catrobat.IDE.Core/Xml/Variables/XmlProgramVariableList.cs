using System.Collections.ObjectModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlProgramVariableList : DataObject
    {
        public ObservableCollection<XmlUserVariable> UserVariables;

        public XmlProgramVariableList() { UserVariables = new ObservableCollection<XmlUserVariable>();}

        public XmlProgramVariableList(XElement xElement)
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
            var xRoot = new XElement("programVariableList");

            foreach (XmlUserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newProgramVariableList = new XmlProgramVariableList();

            foreach (var userVariable in UserVariables)
                newProgramVariableList.UserVariables.Add(userVariable.Copy() as XmlUserVariable);

            return newProgramVariableList;
        }

        public override bool Equals(DataObject other)
        {
            var otherProgramVariableList = other as XmlProgramVariableList;

            if (otherProgramVariableList == null)
                return false;

            var count = UserVariables.Count;
            var otherCount = otherProgramVariableList.UserVariables.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!UserVariables[i].Equals(otherProgramVariableList.UserVariables[i]))
                    return false;

            return true;
        }
    }
}
