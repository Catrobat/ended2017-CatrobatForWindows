using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeXByBrick : XmlBrick
    {
        protected XmlFormula _xMovement;
        public XmlFormula XMovement
        {
            get { return _xMovement; }
            set
            {
                _xMovement = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeXByBrick() {}

        public XmlChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _xMovement = new XmlFormula(xRoot.Element("xMovement"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeXByNBrick");

            var xVariable = new XElement("xMovement");
            xVariable.Add(_xMovement.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_xMovement != null)
                _xMovement.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeXByBrick();
            newBrick._xMovement = _xMovement.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeXByBrick;

            if (otherBrick == null)
                return false;

            return XMovement.Equals(otherBrick.XMovement);
        }
    }
}