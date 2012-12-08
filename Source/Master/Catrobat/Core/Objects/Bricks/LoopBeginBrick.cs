using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Helpers;

namespace Catrobat.Core.Objects
{
    public abstract class LoopBeginBrick : Brick
    {
        protected LoopEndBrickRef loopEndBrickReference;

        public LoopBeginBrick()
        {
        }

        public LoopBeginBrick(Sprite parent) : base(parent)
        {
        }

        public LoopBeginBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public LoopEndBrickRef LoopEndBrickReference
        {
            get { return loopEndBrickReference; }
            set
            {
                if (loopEndBrickReference == value)
                    return;

                loopEndBrickReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopEndBrickReference"));
            }
        }

        public LoopEndBrick LoopEndBrick
        {
            get { return loopEndBrickReference.LoopEndBrick; }
            set
            {
                if (loopEndBrickReference == null)
                {
                    loopEndBrickReference = new LoopEndBrickRef(sprite);
                    loopEndBrickReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (loopEndBrickReference.LoopEndBrick == value)
                    return;

                loopEndBrickReference.LoopEndBrick = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopEndBrick"));
            }
        }

        internal abstract override void LoadFromXML(XElement xRoot);

        internal abstract override XElement CreateXML();

        protected override void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("loopEndBrick") != null)
                loopEndBrickReference = new LoopEndBrickRef(xRoot.Element("loopEndBrick"), sprite);
        }

        protected override void CreateCommonXML(XElement xRoot)
        {
            xRoot.Add(loopEndBrickReference.CreateXML());
        }

        public abstract override DataObject Copy(Sprite parent);

        public void CopyReference(LoopBeginBrick copiedFrom, Sprite parent)
        {
            if (copiedFrom.loopEndBrickReference != null)
                loopEndBrickReference = copiedFrom.loopEndBrickReference.Copy(parent) as LoopEndBrickRef;
        }
    }
}