using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlUserVariable : DataObject
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
        

        public XmlUserVariable() {}

        public XmlUserVariable(XElement xElement)
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
            var newUserVariable = new XmlUserVariable();
            newUserVariable.Name = _name;

            return newUserVariable;
        }

        public override bool Equals(DataObject other)
        {
            var otherUserVariable = other as XmlUserVariable;

            if (otherUserVariable == null)
                return false;

            return Name == otherUserVariable.Name;
        }
    }
}
