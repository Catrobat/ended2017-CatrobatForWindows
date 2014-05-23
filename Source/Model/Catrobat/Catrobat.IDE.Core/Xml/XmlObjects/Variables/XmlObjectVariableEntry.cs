using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlObjectVariableEntry : XmlObject
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
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            XmlSpriteReference = new XmlSpriteReference(xRoot.Element("object"));
            VariableList = new XmlUserVariableList(xRoot.Element("list"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("entry");

            xRoot.Add(_xmlSpriteReference.CreateXml());
            xRoot.Add(VariableList.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(_xmlSpriteReference != null && XmlSpriteReference.Sprite == null)
                _xmlSpriteReference.LoadReference();
        }

        public XmlObject Copy(XmlSprite newSprite)
        {
            var newObjectVariableEntry = new XmlObjectVariableEntry();
            newObjectVariableEntry.Sprite = newSprite;
            newObjectVariableEntry.VariableList = VariableList.Copy() as XmlUserVariableList;

            return newObjectVariableEntry;
        }

        public XmlObject Copy()
        {
            var newObjectVariableEntry = new XmlObjectVariableEntry();
            newObjectVariableEntry.XmlSpriteReference = XmlSpriteReference.Copy() as XmlSpriteReference;
            newObjectVariableEntry.VariableList = VariableList.Copy() as XmlUserVariableList;

            return newObjectVariableEntry;
        }

        public override bool Equals(XmlObject other)
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
