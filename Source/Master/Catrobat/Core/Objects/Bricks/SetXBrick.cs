using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SetXBrick : Brick
    {
        protected int xPosition = 0;

        public SetXBrick()
        {
        }

        public SetXBrick(Sprite parent) : base(parent)
        {
        }

        public SetXBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int XPosition
        {
            get { return xPosition; }
            set
            {
                xPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XPosition"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            xPosition = int.Parse(xRoot.Element("xPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setXBrick");

            xRoot.Add(new XElement("xPosition")
                {
                    Value = xPosition.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetXBrick(parent);
            newBrick.xPosition = xPosition;

            return newBrick;
        }
    }
}