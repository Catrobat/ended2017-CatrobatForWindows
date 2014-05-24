using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSpriteList : XmlObject
    {
        public ObservableCollection<XmlSprite> Sprites { get; set; }


        public XmlSpriteList()
        {
            Sprites = new ObservableCollection<XmlSprite>();
        }

        public XmlSpriteList(XElement xRoot)
        {
            Sprites = new ObservableCollection<XmlSprite>();
            LoadFromXml(xRoot);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                Sprites.Add(new XmlSprite());
            }

            var enumerator = Sprites.GetEnumerator();
            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXml(xSprite);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("objectList");

            foreach (XmlSprite sprite in Sprites)
            {
                xRoot.Add(sprite.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            foreach (var sprite in Sprites)
                sprite.LoadReference();
        }

        public override bool Equals(XmlObject other)
        {
            var otherSpriteList = other as XmlSpriteList;

            if (otherSpriteList == null)
                return false;

            var count = Sprites.Count;
            var otherCount = otherSpriteList.Sprites.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!Sprites[i].Equals(otherSpriteList.Sprites[i]))
                    return false;

            return true;
        }
    }
}