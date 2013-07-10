using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class TurnLeftBrick : Brick
    {
        protected double _degrees = 15.0f;

        public TurnLeftBrick() {}

        public TurnLeftBrick(Sprite parent) : base(parent) {}

        public TurnLeftBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

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
            var xRoot = new XElement("turnLeftBrick");

            xRoot.Add(new XElement("degrees")
            {
                Value = _degrees.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new TurnLeftBrick(parent);
            newBrick._degrees = _degrees;

            return newBrick;
        }
    }
}