using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class RepeatBrick : LoopBeginBrick
    {
        protected int timesToRepeat;

        public RepeatBrick()
        {
        }

        public RepeatBrick(Sprite parent) : base(parent)
        {
        }

        public RepeatBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int TimesToRepeat
        {
            get { return timesToRepeat; }
            set
            {
                if (timesToRepeat == value)
                    return;

                timesToRepeat = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TimesToRepeat"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            timesToRepeat = int.Parse(xRoot.Element("timesToRepeat").Value);
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("repeatBrick");
            base.CreateCommonXML(xRoot);

            xRoot.Add(new XElement("timesToRepeat")
                {
                    Value = timesToRepeat.ToString()
                });

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new RepeatBrick(parent);
            newBrick.timesToRepeat = timesToRepeat;

            return newBrick;
        }
    }
}