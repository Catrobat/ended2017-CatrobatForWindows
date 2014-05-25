using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlUserVariableReference : DataObject
    {
        internal string _reference;

        private XmlUserVariable _userVariable;
        public XmlUserVariable UserVariable
        {
            get { return _userVariable; }
            set
            {
                if (_userVariable == value)
                {
                    return;
                }

                _userVariable = value;
                RaisePropertyChanged();
            }
        }


        public XmlUserVariableReference()
        {
        }

        public XmlUserVariableReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("userVariable");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(UserVariable == null)
                UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as XmlUserVariable;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newUserVariableRef = new XmlUserVariableReference();
            newUserVariableRef.UserVariable = _userVariable;

            return newUserVariableRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as XmlUserVariableReference;

            if (otherReference == null)
                return false;

            if (UserVariable.Name != otherReference.UserVariable.Name)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}
