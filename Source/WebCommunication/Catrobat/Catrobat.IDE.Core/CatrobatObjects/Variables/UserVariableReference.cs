using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Variables
{
    public class UserVariableReference : DataObject
    {
        internal string _reference;

        private UserVariable _userVariable;
        public UserVariable UserVariable
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


        public UserVariableReference()
        {
        }

        public UserVariableReference(XElement xElement)
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
                UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as UserVariable;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newUserVariableRef = new UserVariableReference();
            newUserVariableRef.UserVariable = _userVariable;

            return newUserVariableRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as UserVariableReference;

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
