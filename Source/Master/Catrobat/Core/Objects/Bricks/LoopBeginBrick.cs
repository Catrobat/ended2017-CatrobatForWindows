using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public abstract class LoopBeginBrick : Brick
    {
        protected LoopEndBrickRef _loopEndBrickReference;
        public LoopEndBrickRef LoopEndBrickReference
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
            }
        }

        public LoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrickReference.LoopEndBrick; }
            set
            {
                if (_loopEndBrickReference == null)
                    _loopEndBrickReference = new LoopEndBrickRef(_sprite);

                if (_loopEndBrickReference.LoopEndBrick == value)
                    return;

                _loopEndBrickReference.LoopEndBrick = value;

                if (value == null)
                    _loopEndBrickReference = null;

                RaisePropertyChanged();
            }
        }


        protected LoopBeginBrick() {}

        protected LoopBeginBrick(Sprite parent) : base(parent) {}

        protected LoopBeginBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal abstract override void LoadFromXML(XElement xRoot);

        internal abstract override XElement CreateXML();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopEndBrick") != null)
            {
                _loopEndBrickReference = new LoopEndBrickRef(xRoot.Element("loopEndBrick"), _sprite);
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(_loopEndBrickReference.CreateXML());
        }

        internal override void LoadReference()
        {
            _loopEndBrickReference.LoadReference();
        }

        public abstract override DataObject Copy(Sprite parent);

        public void CopyReference(LoopBeginBrick copiedFrom, Sprite parent)
        {
            if (copiedFrom._loopEndBrickReference != null)
            {
                _loopEndBrickReference = copiedFrom._loopEndBrickReference.Copy(parent) as LoopEndBrickRef;
            }
        }
    }
}