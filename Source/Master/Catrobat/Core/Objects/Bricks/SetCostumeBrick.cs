using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetCostumeBrick : Brick
    {
        private CostumeReference costumeReference;

        public SetCostumeBrick()
        {
        }

        public SetCostumeBrick(Sprite parent) : base(parent)
        {
        }

        public SetCostumeBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal CostumeReference CostumeReference
        {
            get { return costumeReference; }
            set
            {
                if (costumeReference == value)
                    return;

                costumeReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CostumeReference"));
            }
        }

        public Costume Costume
        {
            get
            {
                if (costumeReference == null)
                    return null;

                return costumeReference.Costume;
            }
            set
            {
                if (costumeReference == null)
                {
                    costumeReference = new CostumeReference(sprite);
                    costumeReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (costumeReference.Costume == value)
                    return;

                costumeReference.Costume = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Costume"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("costumeData") != null)
                costumeReference = new CostumeReference(xRoot.Element("costumeData"), sprite);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setCostumeBrick");

            if (costumeReference != null)
                xRoot.Add(costumeReference.CreateXML());

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetCostumeBrick(parent);
            if (costumeReference != null)
                newBrick.costumeReference = costumeReference.Copy(parent) as CostumeReference;

            return newBrick;
        }
    }
}