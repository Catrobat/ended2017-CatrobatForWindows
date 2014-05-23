using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeYByBrick : XmlBrick
    {
        protected XmlFormula _yMovement;
        public XmlFormula YMovement
        {
            get { return _yMovement; }
            set
            {
                _yMovement = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeYByBrick() {}

        public XmlChangeYByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _yMovement = new XmlFormula(xRoot.Element("yMovement"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeYByNBrick");

            var xVariable = new XElement("yMovement");
            xVariable.Add(_yMovement.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_yMovement != null)
                _yMovement.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeYByBrick();
            newBrick._yMovement = _yMovement.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeYByBrick;

            if (otherBrick == null)
                return false;

            return YMovement.Equals(otherBrick.YMovement);
        }
    }
}