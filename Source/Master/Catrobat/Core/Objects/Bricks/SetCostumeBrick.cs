using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetCostumeBrick : Brick
    {
        private CostumeReference _costumeReference;

        internal CostumeReference CostumeReference
        {
            get { return _costumeReference; }
            set
            {
                if (_costumeReference == value)
                {
                    return;
                }

                _costumeReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CostumeReference"));
            }
        }

        public Costume Costume
        {
            get
            {
                if (_costumeReference == null)
                {
                    return null;
                }

                return _costumeReference.Costume;
            }
            set
            {
                if (_costumeReference == null)
                {
                    _costumeReference = new CostumeReference(_sprite);
                    _costumeReference.Reference = XPathHelper.getReference(value, _sprite);
                }

                if (_costumeReference.Costume == value)
                {
                    return;
                }

                _costumeReference.Costume = value;

                if (value == null)
                {
                    _costumeReference = null;
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Costume"));
            }
        }

        public SetCostumeBrick() {}

        public SetCostumeBrick(Sprite parent) : base(parent) {}

        public SetCostumeBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("costumeData") != null)
            {
                _costumeReference = new CostumeReference(xRoot.Element("costumeData"), _sprite);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setCostumeBrick");

            if (_costumeReference != null)
            {
                xRoot.Add(_costumeReference.CreateXML());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetCostumeBrick(parent);
            if (_costumeReference != null)
            {
                newBrick._costumeReference = _costumeReference.Copy(parent) as CostumeReference;
            }

            return newBrick;
        }

        public void UpdateReference()
        {
            if (_costumeReference != null)
            {
                _costumeReference.Reference = XPathHelper.getReference(_costumeReference.Costume, _sprite);
            }
        }
    }
}