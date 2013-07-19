using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
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
            if (xRoot.Element("pointedObject") != null)
                _pointedSpriteReference = new SpriteReference(xRoot.Element("pointedObject"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointToBrick");

            if (_pointedSpriteReference != null)
            {
                xRoot.Add(_pointedSpriteReference.CreateXML());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        internal override void LoadReference()
        {
            _pointedSpriteReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new PointToBrick();

            return newBrick;
        }

        public void CopyReference(PointToBrick copiedFrom)
        {
            if (copiedFrom._pointedSpriteReference != null)
            {
                _pointedSpriteReference = copiedFrom._pointedSpriteReference.Copy() as SpriteReference;
            }
        }
    }
}