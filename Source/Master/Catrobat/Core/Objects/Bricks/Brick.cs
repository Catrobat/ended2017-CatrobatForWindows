using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public abstract class Brick : DataObject
    {
        protected Sprite _sprite;

        public Brick() {}

        public Brick(Sprite parent)
        {
            _sprite = parent;
        }

        public Brick(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);

            //LoadFromCommonXML(xElement); 
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
                RaisePropertyChanged();
            }
        }

        internal abstract override void LoadFromXML(XElement xRoot);

        protected virtual void LoadFromCommonXML(XElement xRoot)
        {
            //  if (xRoot.Element("sprite") != null)
            //    sprite = new SpriteReference(xRoot.Element("sprite"));
        }

        internal abstract override XElement CreateXML();

        protected virtual void CreateCommonXML(XElement xRoot)
        {
            //  //if (sprite != null)
            //  //  xRoot.Add(sprite.CreateXML());
        }

        public abstract DataObject Copy(Sprite parent);
    }
}