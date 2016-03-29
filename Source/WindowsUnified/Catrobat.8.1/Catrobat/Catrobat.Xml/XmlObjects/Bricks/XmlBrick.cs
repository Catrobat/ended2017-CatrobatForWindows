using Catrobat_Player.NativeComponent;
using System;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    public abstract partial class XmlBrick : XmlObjectNode, IBrick
    {
        public override bool Equals(System.Object obj)
        {
            XmlBrick b = obj as XmlBrick;
            if ((object)b == null)
                return false;

            return base.Equals(b);
        }

        public bool Equals(XmlBrick b)
        {
            XElement XElementThis = this.CreateXml();
            XElement XElementB = b.CreateXml();

            return (XElementThis.ToString() == XElementB.ToString());            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

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
