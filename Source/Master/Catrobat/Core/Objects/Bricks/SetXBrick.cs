using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetXBrick : Brick
    {
        protected int _xPosition = 0;

        public SetXBrick() {}

        public SetXBrick(Sprite parent) : base(parent) {}

        public SetXBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _xPosition = int.Parse(xRoot.Element("xPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setXBrick");

            xRoot.Add(new XElement("xPosition")
            {
                Value = _xPosition.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetXBrick(parent);
            newBrick._xPosition = _xPosition;

            return newBrick;
        }
    }
}