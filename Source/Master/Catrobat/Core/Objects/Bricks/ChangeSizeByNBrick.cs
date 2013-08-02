using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeSizeByNBrick : Brick
    {
        protected Formula _size;
        public Formula Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }


        public ChangeSizeByNBrick() {}

        public ChangeSizeByNBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _size = new Formula(xRoot.Element("size"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeSizeByNBrick");

            var xVariable = new XElement("size");
            xVariable.Add(_size.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeSizeByNBrick();
            newBrick._size = _size.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeSizeByNBrick;

            if (otherBrick == null)
                return false;

            return Size.Equals(otherBrick.Size);
        }
    }
}