using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects
{
    public class SpriteReference : DataObject
    {
        private readonly Sprite _parentSprite;
        private string _reference;
        private Sprite _sprite;

        public SpriteReference(Sprite parent)
        {
            _parentSprite = parent;
        }

        public SpriteReference(XElement xElement, Sprite parent)
        {
            _parentSprite = parent;
            LoadFromXML(xElement);
        }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Reference"));
            }
        }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            _sprite = XPathHelper.GetElement(_reference, _parentSprite) as Sprite;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointedSprite");

            xRoot.Add(new XAttribute("reference", XPathHelper.GetReference(_sprite, _parentSprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSpriteRef = new SpriteReference(parent);
            newSpriteRef._reference = _reference;
            newSpriteRef._sprite = XPathHelper.GetElement(_reference, parent) as Sprite;

            return newSpriteRef;
        }
    }
}