using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrick : Brick
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
                    if (value is RepeatBrick)
                        _loopBeginBrickReference.Class = "repeat";
                    else if (value is ForeverBrick)
                        _loopBeginBrickReference.Class = "forever";
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

        public LoopEndBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                _loopBeginBrickReference = new LoopBeginBrickReference(xRoot.Element("loopBeginBrick"));
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
            if (_loopBeginBrickReference != null)
                _loopBeginBrickReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new LoopEndBrick();

            if(_loopBeginBrickReference != null)
                newBrick.LoopBeginBrickReference = _loopBeginBrickReference.Copy() as LoopBeginBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as LoopEndBrick;

            if (otherBrick == null)
                return false;

            return LoopBeginBrickReference.Equals(otherBrick.LoopBeginBrickReference);
        }
    }
}