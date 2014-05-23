using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public abstract partial class XmlLoopBeginBrick : XmlBrick
    {
        protected XmlLoopEndBrickReference _loopEndBrickReference;
        public XmlLoopEndBrickReference LoopEndBrickReference
        {
            get { return _loopEndBrickReference; }
            set
            {
                if (_loopEndBrickReference == value)
                {
                    return;
                }

                _loopEndBrickReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => LoopEndBrick);
            }
        }

        public XmlLoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrickReference.LoopEndBrick; }
            set
            {
                if (_loopEndBrickReference == null)
                {
                    _loopEndBrickReference = new XmlLoopEndBrickReference();
                    //if (value is XmlRepeatLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndBrick";
                    //else if (value is XmlForeverLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndlessBrick";
                }

                if (_loopEndBrickReference == null)
                    _loopEndBrickReference = new XmlLoopEndBrickReference();

                if (_loopEndBrickReference.LoopEndBrick == value)
                    return;

                _loopEndBrickReference.LoopEndBrick = value;

                if (value == null)
                    _loopEndBrickReference = null;

                RaisePropertyChanged();
            }
        }


        protected XmlLoopBeginBrick() {}

        protected XmlLoopBeginBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXml(XElement xRoot);

        internal abstract override XElement CreateXml();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopEndBrick") != null)
            {
                _loopEndBrickReference = new XmlLoopEndBrickReference(xRoot.Element("loopEndBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(_loopEndBrickReference.CreateXml());
        }

        internal override void LoadReference()
        {
            if (_loopEndBrickReference != null)
                _loopEndBrickReference.LoadReference();
        }

        public abstract override XmlObject Copy();
    }
}