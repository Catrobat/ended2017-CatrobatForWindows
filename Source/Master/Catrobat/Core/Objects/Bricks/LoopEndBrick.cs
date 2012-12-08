using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Helpers;

namespace Catrobat.Core.Objects
{
    public class LoopEndBrick : Brick
    {
        protected LoopBeginBrickRef loopBeginBrickReference;

        public LoopEndBrick()
        {
        }

        public LoopEndBrick(Sprite parent) : base(parent)
        {
        }

        public LoopEndBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal LoopBeginBrickRef LoopBeginBrickReference
        {
            get { return loopBeginBrickReference; }
            set
            {
                if (loopBeginBrickReference == value)
                    return;

                loopBeginBrickReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopBeginBrickReference"));
            }
        }

        public LoopBeginBrick LoopBeginBrick
        {
            get { return loopBeginBrickReference.LoopBeginBrick; }
            set
            {
                if (loopBeginBrickReference == null)
                {
                    loopBeginBrickReference = new LoopBeginBrickRef(sprite);
                    if (value is RepeatBrick)
                        loopBeginBrickReference.Class = "RepeatBrick";
                    else if (value is ForeverBrick)
                        loopBeginBrickReference.Class = "ForeverBrick";
                    loopBeginBrickReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (loopBeginBrickReference.LoopBeginBrick == value)
                    return;

                loopBeginBrickReference.LoopBeginBrick = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopBeginBrick"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("loopBeginBrick") != null)
                loopBeginBrickReference = new LoopBeginBrickRef(xRoot.Element("loopBeginBrick"), sprite);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");

            if (loopBeginBrickReference != null)
                xRoot.Add(loopBeginBrickReference.CreateXML());

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new LoopEndBrick(parent);

            return newBrick;
        }

        public void CopyReference(LoopEndBrick copiedFrom, Sprite parent)
        {
            if (copiedFrom.loopBeginBrickReference != null)
                loopBeginBrickReference = copiedFrom.loopBeginBrickReference.Copy(parent) as LoopBeginBrickRef;
        }
    }
}