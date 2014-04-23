using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public abstract class LoopBeginBrick : Brick
    {
        protected LoopEndBrickReference _loopEndBrickReference;
        public LoopEndBrickReference LoopEndBrickReference
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

        public LoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrickReference.LoopEndBrick; }
            set
            {
                if (_loopEndBrickReference == null)
                {
                    _loopEndBrickReference = new LoopEndBrickReference();
                    //if (value is RepeatLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndBrick";
                    //else if (value is ForeverLoopEndBrick)
                    //    _loopEndBrickReference.Class = "loopEndlessBrick";
                }

                if (_loopEndBrickReference == null)
                    _loopEndBrickReference = new LoopEndBrickReference();

                if (_loopEndBrickReference.LoopEndBrick == value)
                    return;

                _loopEndBrickReference.LoopEndBrick = value;

                if (value == null)
                    _loopEndBrickReference = null;

                RaisePropertyChanged();
            }
        }


        protected LoopBeginBrick() {}

        protected LoopBeginBrick(XElement xElement) : base(xElement) {}

        internal abstract override void LoadFromXML(XElement xRoot);

        internal abstract override XElement CreateXML();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopEndBrick") != null)
            {
                _loopEndBrickReference = new LoopEndBrickReference(xRoot.Element("loopEndBrick"));
            }
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(_loopEndBrickReference.CreateXML());
        }

        internal override void LoadReference()
        {
            if (_loopEndBrickReference != null)
                _loopEndBrickReference.LoadReference();
        }

        public abstract override DataObject Copy();
    }
}