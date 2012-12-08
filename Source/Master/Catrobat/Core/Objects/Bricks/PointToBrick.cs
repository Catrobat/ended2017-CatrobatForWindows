using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Helpers;

namespace Catrobat.Core.Objects
{
    public class PointToBrick : Brick
    {
        private SpriteReference pointedSpriteReference;

        public PointToBrick()
        {
        }

        public PointToBrick(Sprite parent) : base(parent)
        {
        }

        public PointToBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal SpriteReference PointedSpriteReference
        {
            get
            {
                if (pointedSpriteReference == null)
                    return null;

                return pointedSpriteReference;
            }
            set
            {
                if (pointedSpriteReference == value)
                    return;

                pointedSpriteReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PointedSpriteReference"));
            }
        }

        public Sprite PointedSprite
        {
            get
            {
                if (pointedSpriteReference == null)
                    return null;

                return pointedSpriteReference.Sprite;
            }
            set
            {
                if (pointedSpriteReference == null)
                {
                    pointedSpriteReference = new SpriteReference(sprite);
                    pointedSpriteReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (pointedSpriteReference.Sprite == value)
                    return;

                pointedSpriteReference.Sprite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PointedSprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("pointedSprite") != null)
                pointedSpriteReference = new SpriteReference(xRoot.Element("pointedSprite"), sprite);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointToBrick");

            if (pointedSpriteReference != null)
                xRoot.Add(pointedSpriteReference.CreateXML());

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PointToBrick(parent);

            return newBrick;
        }

        public void CopyReference(PointToBrick copiedFrom, Sprite parent)
        {
            if (copiedFrom.pointedSpriteReference != null)
                pointedSpriteReference = copiedFrom.pointedSpriteReference.Copy(parent) as SpriteReference;
        }
    }
}