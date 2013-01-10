using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SpeakBrick : Brick
    {
        protected string text;

        public SpeakBrick()
        {
        }

        public SpeakBrick(Sprite parent) : base(parent)
        {
        }

        public SpeakBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text == value)
                    return;

                text = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("text") != null)
                text = xRoot.Element("text").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("speakBrick");

            if (text != null)
                xRoot.Add(new XElement("text")
                    {
                        Value = text
                    });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SpeakBrick(parent);
            newBrick.text = text;

            return newBrick;
        }
    }
}