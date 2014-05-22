using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class ChangeYByBrick : Brick
    {
        protected Formula _yMovement;
        public Formula YMovement
        {
            get { return _yMovement; }
            set
            {
                _yMovement = value;
                RaisePropertyChanged();
            }
        }


        public ChangeYByBrick() {}

        public ChangeYByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _yMovement = new Formula(xRoot.Element("yMovement"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeYByNBrick");

            var xVariable = new XElement("yMovement");
            xVariable.Add(_yMovement.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_yMovement != null)
                _yMovement.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeYByBrick();
            newBrick._yMovement = _yMovement.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeYByBrick;

            if (otherBrick == null)
                return false;

            return YMovement.Equals(otherBrick.YMovement);
        }
    }
}