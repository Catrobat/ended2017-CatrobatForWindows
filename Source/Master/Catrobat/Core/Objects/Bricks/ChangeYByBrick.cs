using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
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

        public ChangeYByBrick(Sprite parent) : base(parent) {}

        public ChangeYByBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeYByBrick(parent);
            newBrick._yMovement = _yMovement.Copy(parent) as Formula;

            return newBrick;
        }
    }
}