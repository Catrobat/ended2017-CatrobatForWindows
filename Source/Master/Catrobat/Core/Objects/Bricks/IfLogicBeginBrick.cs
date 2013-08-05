using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class IfLogicBeginBrick : Brick
    {
        private Formula _ifCondition;
        public Formula IfCondition
        {
            get { return _ifCondition; }
            set
            {
                if (_ifCondition == value)
                    return;

                _ifCondition = value;
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

        private IfLogicEndBrickReference _ifLogicEndBrickReference;
        internal IfLogicEndBrickReference IfLogicEndBrickReference
        {
            get { return _ifLogicEndBrickReference; }
            set
            {
                if (_ifLogicEndBrickReference == value)
                    return;

                _ifLogicEndBrickReference = value;
                RaisePropertyChanged();
            }
        }

        public IfLogicEndBrick IfLogicEndBrick
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
                    _ifLogicEndBrickReference = new IfLogicEndBrickReference();

                if (_ifLogicEndBrickReference.IfLogicEndBrick == value)
                    return;

                _ifLogicEndBrickReference.IfLogicEndBrick = value;

                if (value == null)
                    _ifLogicEndBrickReference = null;

                RaisePropertyChanged();
            }
        }

        public IfLogicBeginBrick() {}

        public IfLogicBeginBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("ifCondition") != null)
            {
                _ifCondition = new Formula(xRoot.Element("ifCondition"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                _ifLogicElseBrickReference = new IfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                _ifLogicEndBrickReference = new IfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifLogicBeginBrick");

            if (_ifCondition != null)
            {
                var xVariable1 = new XElement("ifCondition");
                xVariable1.Add(_ifCondition.CreateXML());
                xRoot.Add(xVariable1);
            }

            if(_ifLogicElseBrickReference != null)
                xRoot.Add(_ifLogicElseBrickReference.CreateXML());

            if(_ifLogicEndBrickReference != null)
                xRoot.Add(_ifLogicEndBrickReference.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_ifLogicElseBrickReference != null && _ifLogicElseBrickReference.IfLogicElseBrick == null)
                _ifLogicElseBrickReference.LoadReference();
            if (_ifLogicEndBrickReference != null && _ifLogicEndBrickReference.IfLogicEndBrick == null)
                _ifLogicEndBrickReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new IfLogicBeginBrick();

            newBrick.IfCondition = _ifCondition.Copy() as Formula;
            newBrick.IfLogicElseBrickReference = _ifLogicElseBrickReference.Copy() as IfLogicElseBrickReference;
            newBrick.IfLogicEndBrickReference = _ifLogicEndBrickReference.Copy() as IfLogicEndBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as IfLogicBeginBrick;

            if (otherBrick == null)
                return false;

            if (!IfLogicElseBrickReference.Equals(otherBrick.IfLogicElseBrickReference))
                return false;

            if (!IfLogicEndBrickReference.Equals(otherBrick.IfLogicEndBrickReference))
                return false;

            return true;
        }
    }
}