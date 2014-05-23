using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPointToBrick : XmlBrick
    {
        private XmlSpriteReference _pointedXmlSpriteReference;
        public XmlSpriteReference PointedXmlSpriteReference
        {
            get
            {
                if (_pointedXmlSpriteReference == null)
                {
                    return null;
                }

                return _pointedXmlSpriteReference;
            }
            set
            {
                if (_pointedXmlSpriteReference == value)
                {
                    return;
                }

                _pointedXmlSpriteReference = value;
                RaisePropertyChanged();
            }
        }

        public XmlSprite PointedSprite
        {
            get
            {
                if (_pointedXmlSpriteReference == null)
                {
                    return null;
                }

                return _pointedXmlSpriteReference.Sprite;
            }
            set
            {
                if (_pointedXmlSpriteReference == null)
                    _pointedXmlSpriteReference = new XmlSpriteReference();

                if (_pointedXmlSpriteReference.Sprite == value)
                    return;

                _pointedXmlSpriteReference.Sprite = value;

                if (value == null)
                    _pointedXmlSpriteReference = null;

                RaisePropertyChanged();
            }
        }


        public XmlPointToBrick() {}

        public XmlPointToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("object") != null)
                _pointedXmlSpriteReference = new XmlSpriteReference(xRoot.Element("object"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("pointToBrick");

            if (_pointedXmlSpriteReference != null)
                xRoot.Add(_pointedXmlSpriteReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_pointedXmlSpriteReference != null && _pointedXmlSpriteReference.Sprite == null)
                _pointedXmlSpriteReference.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlPointToBrick();
            newBrick.PointedSprite = PointedSprite;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlPointToBrick;

            if (otherBrick == null)
                return false;

            return PointedXmlSpriteReference.Equals(otherBrick.PointedXmlSpriteReference);
        }
    }
}