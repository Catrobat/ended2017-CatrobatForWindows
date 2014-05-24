using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    public class XmlNoteBrick : XmlBrick
    {
        protected string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note == value)
                {
                    return;
                }

                _note = value;
                RaisePropertyChanged();
            }
        }

        public XmlNoteBrick() {}

        public XmlNoteBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("note") != null)
            {
                _note = xRoot.Element("note").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("noteBrick");

            if (_note != null)
            {
                xRoot.Add(new XElement("note")
                {
                    Value = _note
                });
            }
            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNoteBrick();
            newBrick._note = _note;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNoteBrick;

            if (otherBrick == null)
                return false;

            if (Note != otherBrick.Note)
                return false;

            return true;
        }
    }
}