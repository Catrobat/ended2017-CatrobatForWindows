using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlWaitBrick : XmlBrick
    {
        protected XmlFormula _timeToWaitInSeconds;
        public XmlFormula TimeToWaitInSeconds
        {
            get { return _timeToWaitInSeconds; }
            set
            {
                _timeToWaitInSeconds = value;
                RaisePropertyChanged();
            }
        }


        public XmlWaitBrick() {}

        public XmlWaitBrick(XElement xElement) : base(xElement) {}

        

        internal override void LoadFromXml(XElement xRoot)
        {
            _timeToWaitInSeconds = new XmlFormula(xRoot.Element("timeToWaitInSeconds"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("waitBrick");

            var xVariable = new XElement("timeToWaitInSeconds");
            xVariable.Add(_timeToWaitInSeconds.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_timeToWaitInSeconds != null)
                _timeToWaitInSeconds.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlWaitBrick();
            newBrick._timeToWaitInSeconds = _timeToWaitInSeconds.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlWaitBrick;

            if (otherBrick == null)
                return false;

            return TimeToWaitInSeconds.Equals(otherBrick.TimeToWaitInSeconds);
        }
    }
}