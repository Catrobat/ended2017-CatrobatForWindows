using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class GoNStepsBackBrick : Brick
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

        public GoNStepsBackBrick() {}

        public GoNStepsBackBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _steps = new Formula(xRoot.Element("steps"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("goNStepsBackBrick");

            var xVariable = new XElement("steps");
            xVariable.Add(_steps.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new GoNStepsBackBrick();
            newBrick._steps = _steps.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as GoNStepsBackBrick;

            if (otherBrick == null)
                return false;

            return Steps.Equals(otherBrick.Steps);
        }
    }
}