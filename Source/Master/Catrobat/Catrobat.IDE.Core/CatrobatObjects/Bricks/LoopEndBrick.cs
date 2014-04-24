using System.Xml.Linq;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public abstract class LoopEndBrick : Brick
    {
        protected LoopBeginBrickReference _loopBeginBrickReference;
        internal LoopBeginBrickReference LoopBeginBrickReference
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

        public LoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrickReference.LoopBeginBrick; }
            set
            {
                if (_loopBeginBrickReference == null)
                {
                    _loopBeginBrickReference = new LoopBeginBrickReference();
                }

                if (_loopBeginBrickReference.LoopBeginBrick == value)
                    return;

                _loopBeginBrickReference.LoopBeginBrick = value;

                if (value == null)
                    _loopBeginBrickReference = null;

                RaisePropertyChanged();
            }
        }

        protected LoopEndBrick() {}

        protected LoopEndBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXML(XElement xRoot);

        internal abstract override XElement CreateXML();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                _loopBeginBrickReference = new LoopBeginBrickReference(xRoot.Element("loopBeginBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(_loopBeginBrickReference.CreateXML());
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_loopBeginBrickReference != null)
                _loopBeginBrickReference.LoadReference();
        }

        public abstract override DataObject Copy();

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as LoopEndBrick;

            if (otherBrick == null)
                return false;

            return LoopBeginBrickReference.Equals(otherBrick.LoopBeginBrickReference);
        }
    }
}