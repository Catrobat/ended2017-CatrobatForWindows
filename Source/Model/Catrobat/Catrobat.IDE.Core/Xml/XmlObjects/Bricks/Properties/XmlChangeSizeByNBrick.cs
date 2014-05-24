using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeSizeByNBrick : XmlBrick
    {
        protected XmlFormula _size;
        public XmlFormula Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeSizeByNBrick() {}

        public XmlChangeSizeByNBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _size = new XmlFormula(xRoot.Element("size"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeSizeByNBrick");

            var xVariable = new XElement("size");
            xVariable.Add(_size.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_size != null)
                _size.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeSizeByNBrick();
            newBrick._size = _size.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeSizeByNBrick;

            if (otherBrick == null)
                return false;

            return Size.Equals(otherBrick.Size);
        }
    }
}