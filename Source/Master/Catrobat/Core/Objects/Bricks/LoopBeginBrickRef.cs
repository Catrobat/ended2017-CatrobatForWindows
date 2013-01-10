using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopBeginBrickRef : DataObject
    {
        private readonly Sprite sprite;

        protected string classField;

        private LoopBeginBrick loopBeginBrick;
        protected string reference;

        public LoopBeginBrickRef(Sprite parent)
        {
            sprite = parent;
        }

        public LoopBeginBrickRef(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public string Class
        {
            get { return classField; }
            set
            {
                if (classField == value)
                    return;

                classField = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Class"));
            }
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

        public LoopBeginBrick LoopBeginBrick
        {
            get { return loopBeginBrick; }
            set
            {
                if (loopBeginBrick == value)
                    return;

                loopBeginBrick = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoopBeginBrick"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            classField = xRoot.Attribute("class").Value;
            reference = xRoot.Attribute("reference").Value;
            loopBeginBrick = XPathHelper.getElement(reference, sprite) as LoopBeginBrick;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopBeginBrick");
            xRoot.Add(new XAttribute("class", classField));
            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(loopBeginBrick, sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopBeginBrickRef = new LoopBeginBrickRef(parent);
            newLoopBeginBrickRef.classField = classField;
            newLoopBeginBrickRef.reference = reference;
            newLoopBeginBrickRef.loopBeginBrick = XPathHelper.getElement(reference, sprite) as LoopBeginBrick;

            return newLoopBeginBrickRef;
        }
    }
}