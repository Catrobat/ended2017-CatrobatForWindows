using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class IfLogicEndBrick : Brick
    {
        private IfLogicBeginBrickReference _ifLogicBeginBrickReference;
        internal IfLogicBeginBrickReference IfLogicBeginBrickReference
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

        public IfLogicBeginBrick IfLogicBeginBrick
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
                    _ifLogicBeginBrickReference = new IfLogicBeginBrickReference();

                if (_ifLogicBeginBrickReference.IfLogicBeginBrick == value)
                    return;

                _ifLogicBeginBrickReference.IfLogicBeginBrick = value;

                if (value == null)
                    _ifLogicBeginBrickReference = null;

                RaisePropertyChanged();
            }
        }

        private IfLogicElseBrickReference _ifLogicElseBrickReference;
        internal IfLogicElseBrickReference IfLogicElseBrickReference
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

        public IfLogicElseBrick IfLogicElseBrick
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
                    _ifLogicElseBrickReference = new IfLogicElseBrickReference();

                if (_ifLogicElseBrickReference.IfLogicElseBrick == value)
                    return;

                _ifLogicElseBrickReference.IfLogicElseBrick = value;

                if (value == null)
                    _ifLogicElseBrickReference = null;

                RaisePropertyChanged();
            }
        }


        public IfLogicEndBrick() {}

        public IfLogicEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                _ifLogicBeginBrickReference = new IfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                _ifLogicElseBrickReference = new IfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifLogicEndBrick");

            if(_ifLogicBeginBrickReference != null)
                xRoot.Add(_ifLogicBeginBrickReference.CreateXML());

            if (_ifLogicElseBrickReference != null)
                xRoot.Add(_ifLogicElseBrickReference.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_ifLogicBeginBrickReference != null)
                _ifLogicBeginBrickReference.LoadReference();
            if(_ifLogicElseBrickReference != null)
                _ifLogicElseBrickReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new IfLogicEndBrick();

            if(_ifLogicBeginBrickReference != null)
                newBrick.IfLogicBeginBrickReference = _ifLogicBeginBrickReference.Copy() as IfLogicBeginBrickReference;
            if(_ifLogicElseBrickReference != null)
                newBrick.IfLogicElseBrickReference = _ifLogicElseBrickReference.Copy() as IfLogicElseBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as IfLogicEndBrick;

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