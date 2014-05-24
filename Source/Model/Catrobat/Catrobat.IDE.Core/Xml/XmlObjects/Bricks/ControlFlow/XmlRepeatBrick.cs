using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlRepeatBrick : XmlLoopBeginBrick
    {
        protected XmlFormula _timesToRepeat;
        public XmlFormula TimesToRepeat
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


        public XmlRepeatBrick() {}

        public XmlRepeatBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _timesToRepeat = new XmlFormula(xRoot.Element("timesToRepeat"));
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("repeatBrick");
            base.CreateCommonXML(xRoot);

            var xVariable = new XElement("timesToRepeat");
            xVariable.Add(_timesToRepeat.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            base.LoadReference();

            if (_timesToRepeat != null)
                _timesToRepeat.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlRepeatBrick();

            if(_timesToRepeat != null)
                newBrick._timesToRepeat = _timesToRepeat.Copy() as XmlFormula;
            if(_loopEndBrickReference != null)
                newBrick.LoopEndBrickReference = _loopEndBrickReference.Copy() as XmlLoopEndBrickReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlRepeatBrick;

            if (otherBrick == null)
                return false;

            if (!TimesToRepeat.Equals(otherBrick.TimesToRepeat))
                return false;

            return LoopEndBrickReference.Equals(otherBrick.LoopEndBrickReference);
        }
    }
}