using Catrobat_Player.NativeComponent;
using System.Xml.Linq;
using System;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPointToBrick : XmlBrick, IPointToBrick
    {
        #region NativeInterface
        public IFormulaTree Rotation
        {
            get
            {
                throw new NotImplementedException();
            }
            set { }
        }

        #endregion

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

        public override bool Equals(System.Object obj)
        {
            XmlPointToBrick b = obj as XmlPointToBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.PointedXmlSpriteReference.Equals(b.PointedXmlSpriteReference);
            //TODO: && this.PointedSprite.Equals(b.PointedSprite)
        }

        public bool Equals(XmlPointToBrick b)
        {
            return this.Equals((XmlBrick)b) && this.PointedXmlSpriteReference.Equals(b.PointedXmlSpriteReference);
            //TODO: && this.PointedSprite.Equals(b.PointedSprite)
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ PointedXmlSpriteReference.GetHashCode();
        }

        public XmlPointToBrick() {}

        public XmlPointToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null && xRoot.Attribute(XmlConstants.Type).Value == XmlConstants.XmlPointToBrickType)
            {
                PointedXmlSpriteReference = new XmlSpriteReference(xRoot.Element(XmlConstants.Object));//.Element(XmlConstants.XmlPointToBrickType));
        }
            //if (xRoot.Element(XmlConstants.PointedObject) != null)
            //    PointedXmlSpriteReference = new XmlSpriteReference(xRoot.Element(XmlConstants.PointedObject));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPointToBrickType);

            if (PointedXmlSpriteReference != null)
                xRoot.Add(PointedXmlSpriteReference.CreateXml());

            return xRoot;
        }

        public override void LoadReference()
        {
            if (PointedXmlSpriteReference != null && PointedXmlSpriteReference.Sprite == null)
                PointedXmlSpriteReference.LoadReference();
        }
    }
}
