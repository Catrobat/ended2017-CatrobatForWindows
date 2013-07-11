using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetSizeToBrick : Brick
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


        public SetSizeToBrick() {}

        public SetSizeToBrick(Sprite parent) : base(parent) {}

        public SetSizeToBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _size = new Formula(xRoot.Element("size"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setSizeToBrick");

            var xVariable = new XElement("size");
            xVariable.Add(_size.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetSizeToBrick(parent);
            newBrick._size = _size.Copy(parent) as Formula;

            return newBrick;
        }
    }
}