using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
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

        public WaitBrick(Sprite parent) : base(parent) {}

        public WaitBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new WaitBrick(parent);
            newBrick._timeToWaitInSeconds = _timeToWaitInSeconds.Copy(parent) as Formula;

            return newBrick;
        }
    }
}