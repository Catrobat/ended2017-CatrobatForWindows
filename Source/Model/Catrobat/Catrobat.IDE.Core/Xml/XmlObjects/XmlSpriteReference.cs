using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSpriteReference : XmlObject
    {
        internal string _reference;

        private XmlSprite _sprite;
        public XmlSprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                RaisePropertyChanged();
            }
        }

        public XmlSpriteReference()
        {
        }

        public XmlSpriteReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("object");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sprite == null)
                Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSprite;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newSpriteRef = new XmlSpriteReference();
            newSpriteRef.Sprite = _sprite;

            return newSpriteRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlSpriteReference;

            if (otherReference == null)
                return false;

            if (Sprite.Name != otherReference.Sprite.Name)
                return false;
            if (Sprite.Costumes.Costumes.Count != otherReference.Sprite.Costumes.Costumes.Count)
                return false;
            if (Sprite.Sounds.Sounds.Count != otherReference.Sprite.Sounds.Sounds.Count)
                return false;
            if (Sprite.Scripts.Scripts.Count != otherReference.Sprite.Scripts.Scripts.Count)
                return false;

            return true;
        }
    }
}