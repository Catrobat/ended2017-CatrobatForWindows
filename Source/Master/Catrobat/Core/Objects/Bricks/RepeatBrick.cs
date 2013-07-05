using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class RepeatBrick : LoopBeginBrick
    {
        protected int _timesToRepeat;

        public RepeatBrick() {}

        public RepeatBrick(Sprite parent) : base(parent) {}

        public RepeatBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public int TimesToRepeat
        {
            get { return _timesToRepeat; }
            set
            {
                if (_timesToRepeat == value)
                {
                    return;
                }

                _timesToRepeat = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TimesToRepeat"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _timesToRepeat = int.Parse(xRoot.Element("timesToRepeat").Value);
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("repeatBrick");
            base.CreateCommonXML(xRoot);

            xRoot.Add(new XElement("timesToRepeat")
            {
                Value = _timesToRepeat.ToString()
            });

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new RepeatBrick(parent);
            newBrick._timesToRepeat = _timesToRepeat;

            return newBrick;
        }
    }
}