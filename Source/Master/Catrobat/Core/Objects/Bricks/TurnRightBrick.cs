using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class TurnRightBrick : Brick
    {
        protected Formula _degrees;
        public Formula Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                RaisePropertyChanged();
            }
        }


        public TurnRightBrick() {}

        public TurnRightBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = new Formula(xRoot.Element("degrees"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("turnRightBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new TurnRightBrick();
            newBrick._degrees = _degrees.Copy() as Formula;

            return newBrick;
        }
    }
}