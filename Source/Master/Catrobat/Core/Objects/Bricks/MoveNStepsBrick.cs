using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class MoveNStepsBrick : Brick
    {
        protected double steps = 10.0f;

        public MoveNStepsBrick()
        {
        }

        public MoveNStepsBrick(Sprite parent) : base(parent)
        {
        }

        public MoveNStepsBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Steps
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
            steps = double.Parse(xRoot.Element("steps").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("moveNStepsBrick");

            xRoot.Add(new XElement("steps")
                {
                    Value = steps.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new MoveNStepsBrick(parent);
            newBrick.steps = steps;

            return newBrick;
        }
    }
}