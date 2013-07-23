using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class ProgramVariableList : DataObject
    {
        public ObservableCollection<UserVariable> UserVariables;

        public ProgramVariableList() { UserVariables = new ObservableCollection<UserVariable>();}

        public ProgramVariableList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot == null)
            {
                UserVariables = new ObservableCollection<UserVariable>();
                return;
            }
            UserVariables = new ObservableCollection<UserVariable>();
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
    }
}
