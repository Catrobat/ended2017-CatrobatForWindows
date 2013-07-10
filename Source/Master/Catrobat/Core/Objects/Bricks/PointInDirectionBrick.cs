using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class PointInDirectionBrick : Brick
    {
        protected double _degrees;
        public double Degrees
        {
            get { return _degrees; }
            set
            {
                if (_degrees == value)
                {
                    return;
                }

                _degrees = value;
                RaisePropertyChanged();
            }
        }


        public PointInDirectionBrick() {}

        public PointInDirectionBrick(Sprite parent) : base(parent) {}

        public PointInDirectionBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = double.Parse(xRoot.Element("degrees").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointInDirectionBrick");

            xRoot.Add(new XElement("degrees")
            {
                Value = _degrees.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PointInDirectionBrick(parent);
            newBrick._degrees = _degrees;

            return newBrick;
        }
    }
}