using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeSizeByNBrick : Brick
    {
        protected double _size = 20.0f;

        public ChangeSizeByNBrick() {}

        public ChangeSizeByNBrick(Sprite parent) : base(parent) {}

        public ChangeSizeByNBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _size = double.Parse(xRoot.Element("size").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeSizeByNBrick");

            xRoot.Add(new XElement("size")
            {
                Value = _size.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeSizeByNBrick(parent);
            newBrick._size = _size;

            return newBrick;
        }
    }
}