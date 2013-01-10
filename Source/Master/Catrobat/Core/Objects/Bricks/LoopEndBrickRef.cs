using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrickRef : DataObject
    {
        private readonly Sprite sprite;

        private LoopEndBrick loopEndBrick;
        protected string reference;

        public LoopEndBrickRef(Sprite parent)
        {
            sprite = parent;
        }

        public LoopEndBrickRef(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public string Reference
        {
            get { return reference; }
            set
            {
                if (reference == value)
                    return;

                reference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Reference"));
            }
        }

        public LoopEndBrick LoopEndBrick
        {
            get { return loopEndBrick; }
            set
            {
                if (loopEndBrick == value)
                    return;

                loopEndBrick = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopEndBrick"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            reference = xRoot.Attribute("reference").Value;
            loopEndBrick = XPathHelper.getElement(reference, sprite) as LoopEndBrick;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(loopEndBrick, sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopEndBrickRef = new LoopEndBrickRef(parent);
            newLoopEndBrickRef.reference = reference;
            newLoopEndBrickRef.loopEndBrick = XPathHelper.getElement(reference, parent) as LoopEndBrick;

            return newLoopEndBrickRef;
        }
    }
}