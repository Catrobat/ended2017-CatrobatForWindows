using System.Xml.Linq;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class IfLogicElseBrick : Brick
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
                RaisePropertyChanged(() => IfLogicEndBrick);
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


        public IfLogicElseBrick() {}

        public IfLogicElseBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("ifBeginBrick") != null)
            {
                _ifLogicBeginBrickReference = new IfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                _ifLogicEndBrickReference = new IfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifLogicElseBrick");

                xRoot.Add(_ifLogicBeginBrickReference.CreateXML());

                xRoot.Add(_ifLogicEndBrickReference.CreateXML());

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_ifLogicBeginBrickReference != null)
                _ifLogicBeginBrickReference.LoadReference();
            if (_ifLogicEndBrickReference != null)
                _ifLogicEndBrickReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new IfLogicElseBrick();

            if(_ifLogicBeginBrickReference != null)
                newBrick.IfLogicBeginBrickReference = _ifLogicBeginBrickReference.Copy() as IfLogicBeginBrickReference;
            if(_ifLogicEndBrickReference != null)
                newBrick.IfLogicEndBrickReference = _ifLogicEndBrickReference.Copy() as IfLogicEndBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as IfLogicElseBrick;

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