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
            UserVariable = ReferenceHelper.GetReferenceObject(this, xRoot.Attribute("reference").Value) as UserVariable;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("userVariableRef");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newUserVariableRef = new UserVariableReference(parent);
            newUserVariableRef.UserVariable = _userVariable;

            return newUserVariableRef;
        }
    }
}
