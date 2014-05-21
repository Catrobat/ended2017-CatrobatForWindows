using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Formulas;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            base.LoadReference(converter);

            if (_timesToRepeat != null)
                _timesToRepeat.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new RepeatBrick();

            if(_timesToRepeat != null)
                newBrick._timesToRepeat = _timesToRepeat.Copy() as Formula;
            if(_loopEndBrickReference != null)
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