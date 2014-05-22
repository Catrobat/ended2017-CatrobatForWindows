using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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

        public PointInDirectionBrick(XElement xElement) : base(xElement) {}

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

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_degrees != null)
                _degrees.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new PointInDirectionBrick();
            newBrick._degrees = _degrees.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as PointInDirectionBrick;

            if (otherBrick == null)
                return false;

            return Degrees.Equals(otherBrick.Degrees);
        }
    }
}