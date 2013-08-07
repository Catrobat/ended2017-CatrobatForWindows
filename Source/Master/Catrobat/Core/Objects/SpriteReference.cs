using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects
{
    public class SpriteReference : DataObject
    {
        internal string _reference;

        private Sprite _sprite;
        public Sprite Sprite
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

        public SpriteReference()
        {
        }

        public SpriteReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("object");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sprite == null)
                Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newSpriteRef = new SpriteReference();
            newSpriteRef.Sprite = _sprite;

            return newSpriteRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as SpriteReference;

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