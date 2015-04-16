using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks
{
    public abstract partial class XmlBrick : XmlObject
    {
        protected XmlBrick() {}

        protected XmlBrick(XElement xElement)
        {
            LoadFromXml(xElement);

            //LoadFromCommonXML(xElement); 
        }

        internal abstract override void LoadFromXml(XElement xRoot);

        protected virtual void LoadFromCommonXML(XElement xRoot)
        {
            //  if (xRoot.Element("object") != null)
            //    sprite = new SpriteReference(xRoot.Element("object"));
        }

        internal abstract override XElement CreateXml();

        protected virtual void CreateCommonXML(XElement xRoot)
        {
            //  //if (sprite != null)
            //  //  xRoot.Add(sprite.CreateXML());
        }
    }
}
