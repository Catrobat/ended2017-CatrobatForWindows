using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetYBrick : Brick
    {
        protected int _yPosition = 0;

        public SetYBrick() {}

        public SetYBrick(Sprite parent) : base(parent) {}

        public SetYBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YPosition"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _yPosition = int.Parse(xRoot.Element("yPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setYBrick");

            xRoot.Add(new XElement("yPosition")
            {
                Value = _yPosition.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetYBrick(parent);
            newBrick._yPosition = _yPosition;

            return newBrick;
        }
    }
}