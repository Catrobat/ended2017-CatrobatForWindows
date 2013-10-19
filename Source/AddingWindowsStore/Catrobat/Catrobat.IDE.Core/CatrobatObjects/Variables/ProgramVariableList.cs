using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Variables
{
    public class ProgramVariableList : DataObject
    {
        public ObservableCollection<UserVariable> UserVariables;

        public ProgramVariableList() { UserVariables = new ObservableCollection<UserVariable>();}

        public ProgramVariableList(XElement xElement)
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
            var xRoot = new XElement("programVariableList");

            foreach (UserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newProgramVariableList = new ProgramVariableList();
            
            foreach(var userVariable in UserVariables)
                newProgramVariableList.UserVariables.Add(userVariable.Copy() as UserVariable);

            return newProgramVariableList;
        }

        public override bool Equals(DataObject other)
        {
            var otherProgramVariableList = other as ProgramVariableList;

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
