using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public partial class XmlUserVariable : XmlObject
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
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("userVariable");

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public XmlObject Copy()
        {
            var newUserVariable = new XmlUserVariable();
            newUserVariable.Name = _name;

            return newUserVariable;
        }

        public override bool Equals(XmlObject other)
        {
            var otherUserVariable = other as XmlUserVariable;

            if (otherUserVariable == null)
                return false;

            return Name == otherUserVariable.Name;
        }
    }
}
