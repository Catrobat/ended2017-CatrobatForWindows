using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes
{
    public partial class XmlSetCostumeBrick : XmlBrick
    {
        private XmlCostumeReference _xmlCostumeReference;
        internal XmlCostumeReference XmlCostumeReference
        {
            get { return _xmlCostumeReference; }
            set
            {
                if (_xmlCostumeReference == value)
                {
                    return;
                }

                _xmlCostumeReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => Costume);
            }
        }

        public XmlCostume Costume
        {
            get
            {
                if (_xmlCostumeReference == null)
                {
                    return null;
                }

                return _xmlCostumeReference.Costume;
            }
            set
            {
                if (_xmlCostumeReference == null)
                    _xmlCostumeReference = new XmlCostumeReference();

                if (_xmlCostumeReference.Costume == value)
                    return;

                _xmlCostumeReference.Costume = value;

                if (value == null)
                    _xmlCostumeReference = null;

                RaisePropertyChanged();
            }
        }


        public XmlSetCostumeBrick() { }

        public XmlSetCostumeBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("look") != null)
            {
                _xmlCostumeReference = new XmlCostumeReference(xRoot.Element("look"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setLookBrick");

            if (_xmlCostumeReference != null)
            {
                xRoot.Add(_xmlCostumeReference.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(_xmlCostumeReference != null)
            _xmlCostumeReference.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlSetCostumeBrick();
            if (_xmlCostumeReference != null)
                newBrick._xmlCostumeReference = _xmlCostumeReference.Copy() as XmlCostumeReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSetCostumeBrick;

            if (otherBrick == null)
                return false;

            return XmlCostumeReference.Equals(otherBrick.XmlCostumeReference);
        }
    }
}