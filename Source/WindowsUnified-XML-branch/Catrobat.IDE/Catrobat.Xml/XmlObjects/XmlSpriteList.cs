using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSpriteList : XmlObjectNode
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

        internal override void LoadFromXml(XElement xRoot)
        {
            //foreach (XElement xSprite in xRoot.Elements("object"))
            foreach (XElement xSprite in xRoot.Elements(XmlConstants.Object))
            {
                Sprites.Add(new XmlSprite());
            }

            var enumerator = Sprites.GetEnumerator();
            //foreach (XElement xSprite in xRoot.Elements("object"))
            foreach (XElement xSprite in xRoot.Elements(XmlConstants.Object))
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXml(xSprite);
            }
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("objectList");
            var xRoot = new XElement(XmlConstants.ObjectList);

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
    }
}