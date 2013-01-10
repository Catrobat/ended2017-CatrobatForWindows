using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects
{
    public class SpriteReference : DataObject
    {
        private readonly Sprite parentSprite;

        private string reference;

        private Sprite sprite;

        public SpriteReference(Sprite parent)
        {
            parentSprite = parent;
        }

        public SpriteReference(XElement xElement, Sprite parent)
        {
            parentSprite = parent;
            LoadFromXML(xElement);
        }

        public string Reference
        {
            get { return reference; }
            set
            {
                if (reference == value)
                    return;

                reference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Reference"));
            }
        }

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if (sprite == value)
                    return;

                sprite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            reference = xRoot.Attribute("reference").Value;
            sprite = XPathHelper.getElement(reference, parentSprite) as Sprite;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointedSprite");

            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(sprite, parentSprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSpriteRef = new SpriteReference(parent);
            newSpriteRef.reference = reference;
            newSpriteRef.sprite = XPathHelper.getElement(reference, parent) as Sprite;

            return newSpriteRef;
        }
    }
}