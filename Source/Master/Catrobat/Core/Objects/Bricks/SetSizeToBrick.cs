using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetSizeToBrick : Brick
    {
        protected double _size = 100.0f;

        public SetSizeToBrick() {}

        public SetSizeToBrick(Sprite parent) : base(parent) {}

        public SetSizeToBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Size"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _size = double.Parse(xRoot.Element("size").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setSizeToBrick");

            xRoot.Add(new XElement("size")
            {
                Value = _size.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetSizeToBrick(parent);
            newBrick._size = _size;

            return newBrick;
        }
    }
}