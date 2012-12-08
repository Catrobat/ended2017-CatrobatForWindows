using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class NxtPlayToneBrick : Brick
    {
        protected int durationInMs;

        protected int hertz;

        public NxtPlayToneBrick()
        {
        }

        public NxtPlayToneBrick(Sprite parent) : base(parent)
        {
        }

        public NxtPlayToneBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int DurationInMs
        {
            get { return durationInMs; }
            set
            {
                if (durationInMs == value)
                    return;

                durationInMs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DurationInMs"));
            }
        }

        public int Hertz
        {
            get { return hertz; }
            set
            {
                if (hertz == value)
                    return;

                hertz = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Hertz"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            durationInMs = int.Parse(xRoot.Element("durationInMs").Value);
            hertz = int.Parse(xRoot.Element("hertz").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nxtPlayToneBrick");

            xRoot.Add(new XElement("durationInMs")
                {
                    Value = durationInMs.ToString()
                });

            xRoot.Add(new XElement("hertz")
                {
                    Value = hertz.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NxtPlayToneBrick(parent);
            newBrick.durationInMs = durationInMs;
            newBrick.hertz = hertz;

            return newBrick;
        }
    }
}