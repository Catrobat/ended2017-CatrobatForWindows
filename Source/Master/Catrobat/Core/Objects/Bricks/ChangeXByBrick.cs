using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeXByBrick : Brick
    {
        protected Formula _xMovement;
        public Formula XMovement
        {
            get { return _xMovement; }
            set
            {
                _xMovement = value;
                RaisePropertyChanged();
            }
        }


        public ChangeXByBrick() {}

        public ChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _xMovement = new Formula(xRoot.Element("xMovement"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeXByNBrick");

            var xVariable = new XElement("xMovement");
            xVariable.Add(_xMovement.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeXByBrick();
            newBrick._xMovement = _xMovement.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}