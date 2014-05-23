using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicEndBrick : XmlBrick
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

        private XmlIfLogicElseBrickReference _ifLogicElseBrickReference;
        internal XmlIfLogicElseBrickReference IfLogicElseBrickReference
        {
            get { return _ifLogicElseBrickReference; }
            set
            {
                if (_ifLogicElseBrickReference == value)
                    return;

                _ifLogicElseBrickReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IfLogicElseBrick);
            }
        }

        public XmlIfLogicElseBrick IfLogicElseBrick
        {
            get
            {
                if (_ifLogicElseBrickReference == null)
                    return null;

                return _ifLogicElseBrickReference.IfLogicElseBrick;
            }
            set
            {
                if (_ifLogicElseBrickReference == null)
                    _ifLogicElseBrickReference = new XmlIfLogicElseBrickReference();

                if (_ifLogicElseBrickReference.IfLogicElseBrick == value)
                    return;

                _ifLogicElseBrickReference.IfLogicElseBrick = value;

                if (value == null)
                    _ifLogicElseBrickReference = null;

                RaisePropertyChanged();
            }
        }


        public XmlIfLogicEndBrick() {}

        public XmlIfLogicEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                _ifLogicBeginBrickReference = new XmlIfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                _ifLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicEndBrick");

                xRoot.Add(_ifLogicBeginBrickReference.CreateXml());

                xRoot.Add(_ifLogicElseBrickReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_ifLogicBeginBrickReference != null)
                _ifLogicBeginBrickReference.LoadReference();
            if(_ifLogicElseBrickReference != null)
                _ifLogicElseBrickReference.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlIfLogicEndBrick();

            if(_ifLogicBeginBrickReference != null)
                newBrick.IfLogicBeginBrickReference = _ifLogicBeginBrickReference.Copy() as XmlIfLogicBeginBrickReference;
            if(_ifLogicElseBrickReference != null)
                newBrick.IfLogicElseBrickReference = _ifLogicElseBrickReference.Copy() as XmlIfLogicElseBrickReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlIfLogicEndBrick;

            if (otherBrick == null)
                return false;

            if (!IfLogicBeginBrickReference.Equals(otherBrick.IfLogicBeginBrickReference))
                return false;

            if (!IfLogicElseBrickReference.Equals(otherBrick.IfLogicElseBrickReference))
                return false;

            return true;
        }
    }
}