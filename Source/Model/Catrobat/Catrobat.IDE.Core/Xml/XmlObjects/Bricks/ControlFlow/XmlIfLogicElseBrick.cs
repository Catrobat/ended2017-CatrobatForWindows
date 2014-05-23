using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicElseBrick : XmlBrick
    {
        private XmlIfLogicBeginBrickReference _ifLogicBeginBrickReference;
        internal XmlIfLogicBeginBrickReference IfLogicBeginBrickReference
        {
            get { return _ifLogicBeginBrickReference; }
            set
            {
                if (_ifLogicBeginBrickReference == value)
                    return;

                _ifLogicBeginBrickReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IfLogicBeginBrick);
            }
        }

        public XmlIfLogicBeginBrick IfLogicBeginBrick
        {
            get
            {
                if (_ifLogicBeginBrickReference == null)
                    return null;

                return _ifLogicBeginBrickReference.IfLogicBeginBrick;
            }
            set
            {
                if (_ifLogicBeginBrickReference == null)
                    _ifLogicBeginBrickReference = new XmlIfLogicBeginBrickReference();

                if (_ifLogicBeginBrickReference.IfLogicBeginBrick == value)
                    return;

                _ifLogicBeginBrickReference.IfLogicBeginBrick = value;

                if (value == null)
                    _ifLogicBeginBrickReference = null;

                RaisePropertyChanged();
            }
        }

        private XmlIfLogicEndBrickReference _ifLogicEndBrickReference;
        internal XmlIfLogicEndBrickReference IfLogicEndBrickReference
        {
            get { return _ifLogicEndBrickReference; }
            set
            {
                if (_ifLogicEndBrickReference == value)
                    return;

                _ifLogicEndBrickReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IfLogicEndBrick);
            }
        }

        public XmlIfLogicEndBrick IfLogicEndBrick
        {
            get
            {
                if (_ifLogicEndBrickReference == null)
                    return null;

                return _ifLogicEndBrickReference.IfLogicEndBrick;
            }
            set
            {
                if (_ifLogicEndBrickReference == null)
                    _ifLogicEndBrickReference = new XmlIfLogicEndBrickReference();

                if (_ifLogicEndBrickReference.IfLogicEndBrick == value)
                    return;

                _ifLogicEndBrickReference.IfLogicEndBrick = value;

                if (value == null)
                    _ifLogicEndBrickReference = null;

                RaisePropertyChanged();
            }
        }


        public XmlIfLogicElseBrick() {}

        public XmlIfLogicElseBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                _ifLogicBeginBrickReference = new XmlIfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                _ifLogicEndBrickReference = new XmlIfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicElseBrick");

                xRoot.Add(_ifLogicBeginBrickReference.CreateXml());

                xRoot.Add(_ifLogicEndBrickReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_ifLogicBeginBrickReference != null)
                _ifLogicBeginBrickReference.LoadReference();
            if (_ifLogicEndBrickReference != null)
                _ifLogicEndBrickReference.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlIfLogicElseBrick();

            if(_ifLogicBeginBrickReference != null)
                newBrick.IfLogicBeginBrickReference = _ifLogicBeginBrickReference.Copy() as XmlIfLogicBeginBrickReference;
            if(_ifLogicEndBrickReference != null)
                newBrick.IfLogicEndBrickReference = _ifLogicEndBrickReference.Copy() as XmlIfLogicEndBrickReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlIfLogicElseBrick;

            if (otherBrick == null)
                return false;

            if (!IfLogicBeginBrickReference.Equals(otherBrick.IfLogicBeginBrickReference))
                return false;

            if (!IfLogicEndBrickReference.Equals(otherBrick.IfLogicEndBrickReference))
                return false;

            return true;
        }
    }
}