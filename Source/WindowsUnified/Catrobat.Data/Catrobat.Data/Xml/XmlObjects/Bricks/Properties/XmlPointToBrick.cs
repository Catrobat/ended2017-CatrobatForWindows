using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPointToBrick : XmlBrick
    {
        public XmlSpriteReference PointedXmlSpriteReference { get; set; }

        public XmlSprite PointedSprite
        {
            get
            {
                if (PointedXmlSpriteReference == null)
                {
                    return null;
                }

                return PointedXmlSpriteReference.Sprite;
            }
            set
            {
                if (PointedXmlSpriteReference == null)
                    PointedXmlSpriteReference = new XmlSpriteReference();

                if (PointedXmlSpriteReference.Sprite == value)
                    return;

                PointedXmlSpriteReference.Sprite = value;

                if (value == null)
                    PointedXmlSpriteReference = null;
            }
        }


        public XmlPointToBrick() {}

        public XmlPointToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("object") != null)
                PointedXmlSpriteReference = new XmlSpriteReference(xRoot.Element("object"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("pointToBrick");

            if (PointedXmlSpriteReference != null)
                xRoot.Add(PointedXmlSpriteReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (PointedXmlSpriteReference != null && PointedXmlSpriteReference.Sprite == null)
                PointedXmlSpriteReference.LoadReference();
        }
    }
}