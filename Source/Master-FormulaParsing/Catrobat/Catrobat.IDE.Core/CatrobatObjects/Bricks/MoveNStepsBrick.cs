using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class MoveNStepsBrick : Brick
    {
        protected Formula _steps;
        public Formula Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged();
            }
        }


        public MoveNStepsBrick() {}

        public MoveNStepsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _steps = new Formula(xRoot.Element("steps"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("moveNStepsBrick");

            var xVariable = new XElement("steps");
            xVariable.Add(_steps.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new MoveNStepsBrick();
            newBrick._steps = _steps.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as MoveNStepsBrick;

            if (otherBrick == null)
                return false;

            return Steps.Equals(otherBrick.Steps);
        }
    }
}