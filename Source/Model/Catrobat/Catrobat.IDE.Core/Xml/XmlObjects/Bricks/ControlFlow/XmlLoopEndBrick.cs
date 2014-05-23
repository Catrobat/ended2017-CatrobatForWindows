using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract partial class XmlLoopEndBrick : XmlBrick
    {
        protected XmlLoopBeginBrickReference _loopBeginBrickReference;
        internal XmlLoopBeginBrickReference LoopBeginBrickReference
        {
            get { return _loopBeginBrickReference; }
            set
            {
                if (_loopBeginBrickReference == value)
                    return;

                _loopBeginBrickReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => LoopBeginBrick);
            }
        }

        public XmlLoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrickReference.LoopBeginBrick; }
            set
            {
                if (_loopBeginBrickReference == null)
                {
                    _loopBeginBrickReference = new XmlLoopBeginBrickReference();
                }

                if (_loopBeginBrickReference.LoopBeginBrick == value)
                    return;

                _loopBeginBrickReference.LoopBeginBrick = value;

                if (value == null)
                    _loopBeginBrickReference = null;

                RaisePropertyChanged();
            }
        }

        protected XmlLoopEndBrick() {}

        protected XmlLoopEndBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXml(XElement xRoot);

        internal abstract override XElement CreateXml();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                _loopBeginBrickReference = new XmlLoopBeginBrickReference(xRoot.Element("loopBeginBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(_loopBeginBrickReference.CreateXml());
        }

        internal override void LoadReference()
        {
            if (_loopBeginBrickReference != null)
                _loopBeginBrickReference.LoadReference();
        }

        public abstract override XmlObject Copy();

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlLoopEndBrick;

            if (otherBrick == null)
                return false;

            return LoopBeginBrickReference.Equals(otherBrick.LoopBeginBrickReference);
        }
    }
}