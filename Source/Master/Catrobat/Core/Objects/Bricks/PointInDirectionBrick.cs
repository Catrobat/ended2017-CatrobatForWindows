using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class PointInDirectionBrick : Brick
    {
        protected double degrees;

        public PointInDirectionBrick()
        {
        }

        public PointInDirectionBrick(Sprite parent) : base(parent)
        {
        }

        public PointInDirectionBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Degrees
        {
            get { return degrees; }
            set
            {
                if (degrees == value)
                    return;

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
            var xRoot = new XElement("pointInDirectionBrick");

            xRoot.Add(new XElement("degrees")
                {
                    Value = degrees.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PointInDirectionBrick(parent);
            newBrick.degrees = degrees;

            return newBrick;
        }
    }
}