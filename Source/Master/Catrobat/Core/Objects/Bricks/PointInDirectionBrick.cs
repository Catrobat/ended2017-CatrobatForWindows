using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class PointInDirectionBrick : Brick
    {
        protected Formula _degrees;
        public Formula Degrees
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
            _degrees = new Formula(xRoot.Element("degrees"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("pointInDirectionBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(_degrees.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PointInDirectionBrick(parent);
            newBrick._degrees = _degrees.Copy(parent) as Formula;

            return newBrick;
        }
    }
}