using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrick : Brick
    {
        protected LoopBeginBrickRef _loopBeginBrickReference;

        public LoopEndBrick() {}

        public LoopEndBrick(Sprite parent) : base(parent) {}

        public LoopEndBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal LoopBeginBrickRef LoopBeginBrickReference
        {
            get { return _loopBeginBrickReference; }
            set
            {
                if (_loopBeginBrickReference == value)
                {
                    return;
                }

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
                    _loopBeginBrickReference = new LoopBeginBrickRef(_sprite);
                    if (value is RepeatBrick)
                    {
                        _loopBeginBrickReference.Class = "RepeatBrick";
                    }
                    else if (value is ForeverBrick)
                    {
                        _loopBeginBrickReference.Class = "ForeverBrick";
                    }
                    _loopBeginBrickReference.Reference = XPathHelper.GetReference(value, _sprite);
                }

                if (_loopBeginBrickReference.LoopBeginBrick == value)
                {
                    return;
                }

                _loopBeginBrickReference.LoopBeginBrick = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
            {
                _loopBeginBrickReference = new LoopBeginBrickRef(xRoot.Element("loopBeginBrick"), _sprite);
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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new LoopEndBrick(parent);

            return newBrick;
        }

        public void CopyReference(LoopEndBrick copiedFrom, Sprite parent)
        {
            if (copiedFrom._loopBeginBrickReference != null)
            {
                _loopBeginBrickReference = copiedFrom._loopBeginBrickReference.Copy(parent) as LoopBeginBrickRef;
            }
        }
    }
}