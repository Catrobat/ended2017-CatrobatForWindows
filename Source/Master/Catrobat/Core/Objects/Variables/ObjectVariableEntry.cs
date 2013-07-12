using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class ObjectVariableEntry : DataObject
    {
        private SpriteReference _spriteReference;
        public SpriteReference SpriteReference
        {
            get { return _spriteReference; }
            set
            {
                if (_spriteReference == value)
                {
                    return;
                }

                _spriteReference = value;
                RaisePropertyChanged();
            }
        }

        public UserVariableList VariableList;

        public ObjectVariableEntry() { VariableList = new UserVariableList();}

        public ObjectVariableEntry(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            SpriteReference = new SpriteReference(xRoot.Element("object"), this);
            VariableList = new UserVariableList(xRoot.Element("list"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("entry");

            xRoot.Add(_spriteReference.CreateXML());
            xRoot.Add(VariableList.CreateXML());

            return xRoot;
        }
    }
}
