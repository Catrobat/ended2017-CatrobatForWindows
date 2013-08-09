using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class UserVariableList : DataObject
    {
        public ObservableCollection<UserVariable> UserVariables;

        public UserVariableList() {UserVariables = new ObservableCollection<UserVariable>();}

        public UserVariableList(XElement xElement)
        {
            UserVariables = new ObservableCollection<UserVariable>();
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                UserVariables.Add(new UserVariable(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("list");

            foreach (UserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newUserVariableList = new UserVariableList();

            foreach (var userVariable in UserVariables)
                newUserVariableList.UserVariables.Add(userVariable.Copy() as UserVariable);

            return newUserVariableList;
        }

        public override bool Equals(DataObject other)
        {
            var otherUserVariableList = other as UserVariableList;

            if (otherUserVariableList == null)
                return false;

            var count = UserVariables.Count;
            var otherCount = otherUserVariableList.UserVariables.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (UserVariables[i].Equals(otherUserVariableList.UserVariables[i]))
                    return false;

            return true;
        }
    }
}
