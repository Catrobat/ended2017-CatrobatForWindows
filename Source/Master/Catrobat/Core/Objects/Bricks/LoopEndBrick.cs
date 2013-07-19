using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrick : Brick
    {
        protected LoopBeginBrickRef _loopBeginBrickReference;
        internal LoopBeginBrickRef LoopBeginBrickReference
        {
            get { return _loopBeginBrickReference; }
            set
            {
                if (_loopBeginBrickReference == value)
                    return;

                _loopBeginBrickReference = value;
                RaisePropertyChanged();
            }
        }

        public LoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrickReference.LoopBeginBrick; }
            set
            {
                if (_loopBeginBrickReference == null)
                {
                    _loopBeginBrickReference = new LoopBeginBrickRef();
                    if (value is RepeatBrick)
                        _loopBeginBrickReference.Class = "RepeatBrick";
                    else if (value is ForeverBrick)
                        _loopBeginBrickReference.Class = "ForeverBrick";
                }

                if (_loopBeginBrickReference.LoopBeginBrick == value)
                    return;

                _loopBeginBrickReference.LoopBeginBrick = value;

                if (value == null)
                    _loopBeginBrickReference = null;

                RaisePropertyChanged();
            }
        }

        public LoopEndBrick() {}

        public LoopEndBrick(LoopBeginBrick loopBeginBrick)
        {
            LoopBeginBrickReference.LoopBeginBrick = loopBeginBrick;
        }

        public LoopEndBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                _loopBeginBrickReference = new LoopBeginBrickRef(xRoot.Element("loopBeginBrick"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");

            if (_loopBeginBrickReference != null)
            {
                xRoot.Add(_loopBeginBrickReference.CreateXML());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            _loopBeginBrickReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new LoopEndBrick();

            return newBrick;
        }

        public void CopyReference(LoopEndBrick copiedFrom)
        {
            if (copiedFrom._loopBeginBrickReference != null)
                _loopBeginBrickReference = copiedFrom._loopBeginBrickReference.Copy() as LoopBeginBrickRef;
        }
    }
}