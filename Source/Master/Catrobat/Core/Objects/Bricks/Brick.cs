using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public abstract class Brick : DataObject
    {
        protected Sprite sprite;

        public Brick()
        {
        }

        public Brick(Sprite parent)
        {
            sprite = parent;
        }

        public Brick(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);

            //LoadFromCommonXML(xElement); 
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