using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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

        public SetSizeToBrick(XElement xElement) : base(xElement) {}

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

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_size != null)
                _size.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new SetSizeToBrick();
            newBrick._size = _size.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetSizeToBrick;

            if (otherBrick == null)
                return false;

            return Size.Equals(otherBrick.Size);
        }
    }
}