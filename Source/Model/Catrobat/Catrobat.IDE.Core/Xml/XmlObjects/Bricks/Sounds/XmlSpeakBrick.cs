using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public class XmlSpeakBrick : XmlBrick
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


        public XmlSpeakBrick() {}

        public XmlSpeakBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("text") != null)
            {
                _text = xRoot.Element("text").Value;
            }
        }

        internal override XElement CreateXml()
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

        public override XmlObject Copy()
        {
            var newBrick = new XmlSpeakBrick();
            newBrick._text = _text;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlSpeakBrick;

            if (otherBrick == null)
                return false;

            if (Text != otherBrick.Text)
                return false;

            return true;
        }
    }
}