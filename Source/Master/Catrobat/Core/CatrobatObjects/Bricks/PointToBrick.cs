using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class PointToBrick : Brick
    {
        private SpriteReference _pointedSpriteReference;
        public SpriteReference PointedSpriteReference
        {
            get
            {
                if (_pointedSpriteReference == null)
                {
                    return null;
                }

                return _pointedSpriteReference;
            }
            set
            {
                if (_pointedSpriteReference == value)
                {
                    return;
                }

                _pointedSpriteReference = value;
                RaisePropertyChanged();
            }
        }

        public Sprite PointedSprite
        {
            get
            {
                if (_pointedSpriteReference == null)
                {
                    return null;
                }

                return _pointedSpriteReference.Sprite;
            }
            set
            {
                if (_pointedSpriteReference == null)
                    _pointedSpriteReference = new SpriteReference();

                if (_pointedSpriteReference.Sprite == value)
                    return;

                _pointedSpriteReference.Sprite = value;

                if (value == null)
                    _pointedSpriteReference = null;

                RaisePropertyChanged();
            }
        }


        public PointToBrick() {}

        public PointToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("object") != null)
                _pointedSpriteReference = new SpriteReference(xRoot.Element("object"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointToBrick");

            if (_pointedSpriteReference != null)
                xRoot.Add(_pointedSpriteReference.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_pointedSpriteReference != null && _pointedSpriteReference.Sprite == null)
                _pointedSpriteReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new PointToBrick();
            newBrick.PointedSprite = PointedSprite;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as PointToBrick;

            if (otherBrick == null)
                return false;

            return PointedSpriteReference.Equals(otherBrick.PointedSpriteReference);
        }
    }
}