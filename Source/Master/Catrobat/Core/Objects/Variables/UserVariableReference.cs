using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Variables
{
    public class UserVariableReference : DataObject
    {
        private readonly Sprite _sprite;

        private string _reference;
        public string Reference
        {
            get { return _reference; }
            set
            {
                if (_reference == value)
                {
                    return;
                }

                _reference = value;
                RaisePropertyChanged();
            }
        }

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

        public UserVariableReference(Sprite parent)
        {
            _sprite = parent;
        }

        public UserVariableReference(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            UserVariable = XPathHelper.GetElement(_reference, _sprite) as UserVariable;
        }

        internal override XElement CreateXML()
        {
            throw new NotImplementedException();

            //var xRoot = new XElement("userVariable");

            //xRoot.Add(new XAttribute("reference", XPathHelper.GetReference(UserVariable)));

            //return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newUserVariableRef = new UserVariableReference(parent);
            newUserVariableRef._reference = _reference;
            newUserVariableRef.UserVariable = XPathHelper.GetElement(_reference, parent) as UserVariable;

            return newUserVariableRef;
        }
    }
}
