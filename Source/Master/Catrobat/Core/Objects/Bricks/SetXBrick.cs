using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetXBrick : Brick
    {
        protected Formula _xPosition;
        public Formula XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }


        public SetXBrick() {}

        public SetXBrick(Sprite parent) : base(parent) {}

        public SetXBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _xPosition = new Formula(xRoot.Element("xPosition"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setXBrick");

            var xVariable = new XElement("xPosition");
            xVariable.Add(_xPosition.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetXBrick(parent);
            newBrick._xPosition = _xPosition.Copy(parent) as Formula;

            return newBrick;
        }
    }
}