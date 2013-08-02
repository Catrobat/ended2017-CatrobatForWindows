using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class RepeatBrick : LoopBeginBrick
    {
        protected Formula _timesToRepeat;
        public Formula TimesToRepeat
        {
            get { return _timesToRepeat; }
            set
            {
                if (_timesToRepeat == value)
                {
                    return;
                }

                _timesToRepeat = value;
                RaisePropertyChanged();
            }
        }


        public RepeatBrick() {}

        public RepeatBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _timesToRepeat = new Formula(xRoot.Element("timesToRepeat"));
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("repeatBrick");
            base.CreateCommonXML(xRoot);

            var xVariable = new XElement("timesToRepeat");
            xVariable.Add(_timesToRepeat.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new RepeatBrick();
            newBrick._timesToRepeat = _timesToRepeat.Copy() as Formula;
            newBrick.LoopEndBrickReference = _loopEndBrickReference.Copy() as LoopEndBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as RepeatBrick;

            if (otherBrick == null)
                return false;

            if (!TimesToRepeat.Equals(otherBrick.TimesToRepeat))
                return false;

            return LoopEndBrickReference.Equals(otherBrick.LoopEndBrickReference);
        }
    }
}