using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Variables
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

        public override bool Equals(DataObject other)
        {
            var otherUserVariable = other as UserVariable;

            if (otherUserVariable == null)
                return false;

            return Name != otherUserVariable.Name;
        }
    }
}
