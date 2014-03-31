using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class WaitBrick : Brick
    {
        protected Formula _timeToWaitInSeconds;
        public Formula TimeToWaitInSeconds
        {
            get { return _timeToWaitInSeconds; }
            set
            {
                _timeToWaitInSeconds = value;
                RaisePropertyChanged();
            }
        }


        public WaitBrick() {}

        public WaitBrick(XElement xElement) : base(xElement) {}

        

        internal override void LoadFromXML(XElement xRoot)
        {
            _timeToWaitInSeconds = new Formula(xRoot.Element("timeToWaitInSeconds"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("waitBrick");

            var xVariable = new XElement("timeToWaitInSeconds");
            xVariable.Add(_timeToWaitInSeconds.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_timeToWaitInSeconds != null)
                _timeToWaitInSeconds.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new WaitBrick();
            newBrick._timeToWaitInSeconds = _timeToWaitInSeconds.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as WaitBrick;

            if (otherBrick == null)
                return false;

            return TimeToWaitInSeconds.Equals(otherBrick.TimeToWaitInSeconds);
        }
    }
}