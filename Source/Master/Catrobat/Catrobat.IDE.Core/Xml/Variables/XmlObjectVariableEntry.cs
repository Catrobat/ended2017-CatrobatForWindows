using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlObjectVariableEntry : DataObject
    {
        private XmlSpriteReference _xmlSpriteReference;
        internal XmlSpriteReference XmlSpriteReference
        {
            get { return _xmlSpriteReference; }
            set
            {
                if (_xmlSpriteReference == value)
                {
                    return;
                }

                _xmlSpriteReference = value;
                RaisePropertyChanged();
            }
        }

        public XmlSprite Sprite
        {
            get
            {
                if (_xmlSpriteReference == null)
                {
                    return null;
                }

                return _xmlSpriteReference.Sprite;
            }
            set
            {
                if (_xmlSpriteReference == null)
                    _xmlSpriteReference = new XmlSpriteReference();

                if (_xmlSpriteReference.Sprite == value)
                    return;

                _xmlSpriteReference.Sprite = value;

                if (value == null)
                    _xmlSpriteReference = null;

                RaisePropertyChanged();
            }
        }

        public XmlUserVariableList VariableList;

        public XmlObjectVariableEntry() { VariableList = new XmlUserVariableList(); }

        public XmlObjectVariableEntry(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            XmlSpriteReference = new XmlSpriteReference(xRoot.Element("object"));
            VariableList = new XmlUserVariableList(xRoot.Element("list"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("entry");

            xRoot.Add(_xmlSpriteReference.CreateXML());
            xRoot.Add(VariableList.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(_xmlSpriteReference != null && XmlSpriteReference.Sprite == null)
                _xmlSpriteReference.LoadReference();
        }

        public DataObject Copy(XmlSprite newSprite)
        {
            var newObjectVariableEntry = new XmlObjectVariableEntry();
            newObjectVariableEntry.Sprite = newSprite;
            newObjectVariableEntry.VariableList = VariableList.Copy() as XmlUserVariableList;

            return newObjectVariableEntry;
        }

        public DataObject Copy()
        {
            var newObjectVariableEntry = new XmlObjectVariableEntry();
            newObjectVariableEntry.XmlSpriteReference = XmlSpriteReference.Copy() as XmlSpriteReference;
            newObjectVariableEntry.VariableList = VariableList.Copy() as XmlUserVariableList;

            return newObjectVariableEntry;
        }

        public override bool Equals(DataObject other)
        {
            var otherObjectVariableEntry = other as XmlObjectVariableEntry;

            if (otherObjectVariableEntry == null)
                return false;

            if (XmlSpriteReference != null && otherObjectVariableEntry.XmlSpriteReference != null)
            {
                if (!XmlSpriteReference.Equals(otherObjectVariableEntry.XmlSpriteReference))
                    return false;
            }
            else if (!(XmlSpriteReference == null && otherObjectVariableEntry.XmlSpriteReference == null))
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
