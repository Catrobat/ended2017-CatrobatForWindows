using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class TurnRightBrick : Brick
    {
        protected double _degrees = 15.0f;

        public TurnRightBrick() {}

        public TurnRightBrick(Sprite parent) : base(parent) {}

        public TurnRightBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public double Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _degrees = double.Parse(xRoot.Element("degrees").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("turnRightBrick");

            xRoot.Add(new XElement("degrees")
            {
                Value = _degrees.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new TurnRightBrick(parent);
            newBrick._degrees = _degrees;

            return newBrick;
        }
    }
}