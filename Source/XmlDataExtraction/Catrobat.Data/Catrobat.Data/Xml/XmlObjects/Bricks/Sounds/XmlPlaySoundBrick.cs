using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlPlaySoundBrick : XmlBrick
    {
        public XmlSoundReference XmlSoundReference { get; set; }

        public XmlSound Sound
        {
            get
            {
                if (XmlSoundReference == null)
                {
                    return null;
                }

                return XmlSoundReference.Sound;
            }
            set
            {
                if (XmlSoundReference == null)
                    XmlSoundReference = new XmlSoundReference();

                if (XmlSoundReference.Sound == value)
                    return;

                XmlSoundReference.Sound = value;

                if (value == null)
                    XmlSoundReference = null;
            }
        }


        public XmlPlaySoundBrick() { }

        public XmlPlaySoundBrick(XElement xElement) : base(xElement) { }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("sound") != null)
            {
                XmlSoundReference = new XmlSoundReference(xRoot.Element("sound"));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("playSoundBrick");

            if (XmlSoundReference != null)
            {
                xRoot.Add(XmlSoundReference.CreateXml());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XmlSoundReference != null)
                XmlSoundReference.LoadReference();
        }
    }
}