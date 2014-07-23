using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects
{
    public abstract class XmlObject
    {
        public abstract void LoadFromXml(XElement xRoot);

        public abstract XElement CreateXml();

        public virtual void LoadReference()
        {
        }
    }
}