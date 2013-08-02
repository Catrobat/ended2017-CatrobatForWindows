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
            Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
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

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}