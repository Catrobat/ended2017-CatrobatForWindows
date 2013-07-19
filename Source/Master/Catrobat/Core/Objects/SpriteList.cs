using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SpriteList : DataObject
    {
        public ObservableCollection<Sprite> Sprites { get; set; }


        public SpriteList()
        {
            Sprites = new ObservableCollection<Sprite>();
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Sprites = new ObservableCollection<Sprite>();

            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                Sprites.Add(new Sprite());
            }

            var enumerator = Sprites.GetEnumerator();
            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXML(xSprite);
            }

            LoadReference();
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("objectList");

            foreach (Sprite sprite in Sprites)
            {
                xRoot.Add(sprite.CreateXML());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            foreach (var sprite in Sprites)
                sprite.LoadReference();
        }
    }
}