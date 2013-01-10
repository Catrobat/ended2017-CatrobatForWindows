using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class GoNStepsBackBrick : Brick
    {
        protected int steps = 10;

        public GoNStepsBackBrick()
        {
        }

        public GoNStepsBackBrick(Sprite parent) : base(parent)
        {
        }

        public GoNStepsBackBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int Steps
        {
            get { return steps; }
            set
            {
                steps = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Steps"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            steps = int.Parse(xRoot.Element("steps").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("goNStepsBackBrick");

            xRoot.Add(new XElement("steps")
                {
                    Value = steps.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new GoNStepsBackBrick(parent);
            newBrick.steps = steps;

            return newBrick;
        }
    }
}