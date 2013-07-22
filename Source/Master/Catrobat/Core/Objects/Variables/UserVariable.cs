using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Variables
{
    public class UserVariable : DataObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }
        

        public UserVariable() {}

        public UserVariable(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("userVariable");

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public DataObject Copy()
        {
            var newUserVariable = new UserVariable();
            newUserVariable.Name = _name;

            return newUserVariable;
        }
    }
}
