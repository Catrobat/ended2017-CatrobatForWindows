using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SpeakBrick : Brick
    {
        protected string _text;

        public SpeakBrick() {}

        public SpeakBrick(Sprite parent) : base(parent) {}

        public SpeakBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("text") != null)
            {
                _text = xRoot.Element("text").Value;
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("speakBrick");

            if (_text != null)
            {
                xRoot.Add(new XElement("text")
                {
                    Value = _text
                });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SpeakBrick(parent);
            newBrick._text = _text;

            return newBrick;
        }
    }
}