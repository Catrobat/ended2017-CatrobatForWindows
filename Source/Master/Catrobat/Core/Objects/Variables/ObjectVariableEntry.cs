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
        internal SpriteReference SpriteReference
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

        public Sprite Sprite
        {
            get
            {
                if (_spriteReference == null)
                {
                    return null;
                }

                return _spriteReference.Sprite;
            }
            set
            {
                if (_spriteReference == null)
                    _spriteReference = new SpriteReference();

                if (_spriteReference.Sprite == value)
                    return;

                _spriteReference.Sprite = value;

                if (value == null)
                    _spriteReference = null;

                RaisePropertyChanged();
            }
        }

        public UserVariableList VariableList;

        public ObjectVariableEntry() { VariableList = new UserVariableList(); }

        public ObjectVariableEntry(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            SpriteReference = new SpriteReference(xRoot.Element("object"));
            VariableList = new UserVariableList(xRoot.Element("list"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("entry");

            xRoot.Add(_spriteReference.CreateXML());
            xRoot.Add(VariableList.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(_spriteReference != null && SpriteReference.Sprite == null)
                _spriteReference.LoadReference();
        }

        public DataObject Copy(Sprite newSprite)
        {
            var newObjectVariableEntry = new ObjectVariableEntry();
            newObjectVariableEntry.Sprite = newSprite;
            newObjectVariableEntry.VariableList = VariableList.Copy() as UserVariableList;

            return newObjectVariableEntry;
        }

        public DataObject Copy()
        {
            var newObjectVariableEntry = new ObjectVariableEntry();
            newObjectVariableEntry.SpriteReference = SpriteReference.Copy() as SpriteReference;
            newObjectVariableEntry.VariableList = VariableList.Copy() as UserVariableList;

            return newObjectVariableEntry;
        }

        public override bool Equals(DataObject other)
        {
            var otherObjectVariableEntry = other as ObjectVariableEntry;

            if (otherObjectVariableEntry == null)
                return false;

            if (SpriteReference != null && otherObjectVariableEntry.SpriteReference != null)
            {
                if (!SpriteReference.Equals(otherObjectVariableEntry.SpriteReference))
                    return false;
            }
            else if (!(SpriteReference == null && otherObjectVariableEntry.SpriteReference == null))
                return false;

            if (VariableList != null && otherObjectVariableEntry.VariableList != null)
            {
                if (!VariableList.Equals(otherObjectVariableEntry.VariableList))
                    return false;
            }
            else if (!(VariableList == null && otherObjectVariableEntry.VariableList == null))
                return false;

            return true;
        }
    }
}
