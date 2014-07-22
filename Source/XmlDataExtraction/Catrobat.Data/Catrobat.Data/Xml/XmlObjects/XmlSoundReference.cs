using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects
{
    public class XmlSoundReference : XmlObject
    {
        private string _reference;

        public XmlSound Sound { get; set; }

        public XmlSoundReference()
        {
        }

        public XmlSoundReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(Sound == null)
                Sound = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSound;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}