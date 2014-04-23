using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class TurnLeftBrick : Brick
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


        public TurnLeftBrick() {}

        public TurnLeftBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = new Formula(xRoot.Element("degrees"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("turnLeftBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new TurnLeftBrick();
            newBrick._degrees = _degrees.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as TurnLeftBrick;

            if (otherBrick == null)
                return false;

            return Degrees.Equals(otherBrick.Degrees);
        }
    }
}