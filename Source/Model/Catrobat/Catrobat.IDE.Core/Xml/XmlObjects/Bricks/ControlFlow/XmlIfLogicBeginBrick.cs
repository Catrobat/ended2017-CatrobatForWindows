using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicBeginBrick : XmlBrick
    {
        private XmlFormula _ifCondition;
        public XmlFormula IfCondition
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

        public XmlIfLogicBeginBrick() {}

        public XmlIfLogicBeginBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("ifCondition") != null)
            {
                _ifCondition = new XmlFormula(xRoot.Element("ifCondition"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                _ifLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
            if (xRoot.Element("ifEndBrick") != null)
            {
                _ifLogicEndBrickReference = new XmlIfLogicEndBrickReference(xRoot.Element("ifEndBrick"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifLogicBeginBrick");

            if (_ifCondition != null)
            {
                var xVariable1 = new XElement("ifCondition");
                xVariable1.Add(_ifCondition.CreateXml());
                xRoot.Add(xVariable1);
            }

                xRoot.Add(_ifLogicElseBrickReference.CreateXml());

                xRoot.Add(_ifLogicEndBrickReference.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_ifLogicElseBrickReference != null)
                _ifLogicElseBrickReference.LoadReference();
            if (_ifLogicEndBrickReference != null)
                _ifLogicEndBrickReference.LoadReference();
            if (_ifCondition != null)
                _ifCondition.LoadReference();

        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlIfLogicBeginBrick();

            if(IfCondition != null)
                newBrick.IfCondition = _ifCondition.Copy() as XmlFormula;
            if(_ifLogicElseBrickReference != null)
                newBrick.IfLogicElseBrickReference = _ifLogicElseBrickReference.Copy() as XmlIfLogicElseBrickReference;
            if(_ifLogicEndBrickReference != null)
                newBrick.IfLogicEndBrickReference = _ifLogicEndBrickReference.Copy() as XmlIfLogicEndBrickReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlIfLogicBeginBrick;

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