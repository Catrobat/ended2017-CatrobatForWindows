using System.ComponentModel;
using System.Xml.Linq;
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
                RaisePropertyChanged();
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
                    _costumeReference = new CostumeReference();

                if (_costumeReference.Costume == value)
                    return;

                _costumeReference.Costume = value;

                if (value == null)
                    _costumeReference = null;

                RaisePropertyChanged();
            }
        }


        public SetCostumeBrick() { }

        public SetCostumeBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("look") != null)
            {
                _costumeReference = new CostumeReference(xRoot.Element("look"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setLookBrick");

            if (_costumeReference != null)
            {
                xRoot.Add(_costumeReference.CreateXML());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(_costumeReference.Costume == null)
            _costumeReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new SetCostumeBrick();
            if (_costumeReference != null)
            {
                newBrick._costumeReference = _costumeReference.Copy() as CostumeReference;
            }

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}