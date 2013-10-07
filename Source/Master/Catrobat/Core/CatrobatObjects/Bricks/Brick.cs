using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public abstract class Brick : DataObject
    {
        protected Brick() {}

        protected Brick(XElement xElement)
        {
            LoadFromXML(xElement);

            //LoadFromCommonXML(xElement); 
        }

        internal abstract override void LoadFromXML(XElement xRoot);

        protected virtual void LoadFromCommonXML(XElement xRoot)
        {
            //  if (xRoot.Element("object") != null)
            //    sprite = new SpriteReference(xRoot.Element("object"));
        }

        internal abstract override XElement CreateXML();

        protected virtual void CreateCommonXML(XElement xRoot)
        {
            //  //if (sprite != null)
            //  //  xRoot.Add(sprite.CreateXML());
        }

        public abstract DataObject Copy();
    }
}