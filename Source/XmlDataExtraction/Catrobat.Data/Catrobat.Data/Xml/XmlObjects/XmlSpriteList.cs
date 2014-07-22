using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects
{
    public class XmlSpriteList : XmlObject
    {
        public List<XmlSprite> Sprites { get; set; }

        public XmlSpriteList()
        {
            Sprites = new List<XmlSprite>();
        }

        public XmlSpriteList(XElement xRoot)
        {
            Sprites = new List<XmlSprite>();
            LoadFromXml(xRoot);
        }

        public override void LoadFromXml(XElement xRoot)
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

        public override XElement CreateXml()
        {
            var xRoot = new XElement("objectList");

            foreach (XmlSprite sprite in Sprites)
            {
                xRoot.Add(sprite.CreateXml());
            }

            return xRoot;
        }

        public override void LoadReference()
        {
            foreach (var sprite in Sprites)
                sprite.LoadReference();
        }
    }
}