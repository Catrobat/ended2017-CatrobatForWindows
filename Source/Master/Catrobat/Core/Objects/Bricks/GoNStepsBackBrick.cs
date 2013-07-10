using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class GoNStepsBackBrick : Brick
    {
        protected int _steps = 10;
        public int Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged();
            }
        }

        public GoNStepsBackBrick() {}

        public GoNStepsBackBrick(Sprite parent) : base(parent) {}

        public GoNStepsBackBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _steps = int.Parse(xRoot.Element("steps").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("goNStepsBackBrick");

            xRoot.Add(new XElement("steps")
            {
                Value = _steps.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new GoNStepsBackBrick(parent);
            newBrick._steps = _steps;

            return newBrick;
        }
    }
}