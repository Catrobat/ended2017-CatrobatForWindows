using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class NoteBrick : Brick
    {
        protected string note;

        public NoteBrick()
        {
        }

        public NoteBrick(Sprite parent) : base(parent)
        {
        }

        public NoteBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public string Note
        {
            get { return note; }
            set
            {
                if (note == value)
                    return;

                note = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Note"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("note") != null)
                note = xRoot.Element("note").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("noteBrick");

            if (note != null)
            {
                xRoot.Add(new XElement("note")
                    {
                        Value = note
                    });
            }
            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NoteBrick(parent);
            newBrick.note = note;

            return newBrick;
        }
    }
}