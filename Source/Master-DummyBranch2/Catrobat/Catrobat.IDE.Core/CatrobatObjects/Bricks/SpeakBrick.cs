using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class SpeakBrick : Brick
    {
        protected string _text;
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


        public SpeakBrick() {}

        public SpeakBrick(XElement xElement) : base(xElement) {}

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

        public override DataObject Copy()
        {
            var newBrick = new SpeakBrick();
            newBrick._text = _text;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SpeakBrick;

            if (otherBrick == null)
                return false;

            if (Text != otherBrick.Text)
                return false;

            return true;
        }
    }
}