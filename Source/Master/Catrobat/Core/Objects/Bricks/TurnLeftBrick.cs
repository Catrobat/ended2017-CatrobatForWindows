using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class TurnLeftBrick : Brick
    {
        protected double degrees = 15.0f;

        public TurnLeftBrick()
        {
        }

        public TurnLeftBrick(Sprite parent) : base(parent)
        {
        }

        public TurnLeftBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Degrees
        {
            get { return degrees; }
            set
            {
                degrees = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Degrees"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            degrees = double.Parse(xRoot.Element("degrees").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("turnLeftBrick");

            xRoot.Add(new XElement("degrees")
                {
                    Value = degrees.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new TurnLeftBrick(parent);
            newBrick.degrees = degrees;

            return newBrick;
        }
    }
}