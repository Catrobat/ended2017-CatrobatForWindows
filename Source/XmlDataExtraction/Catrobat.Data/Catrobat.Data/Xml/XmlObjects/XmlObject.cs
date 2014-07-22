using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects
{
    public abstract class XmlObject
    {
        internal abstract void LoadFromXml(XElement xRoot);

        internal abstract XElement CreateXml();

        internal virtual void LoadReference()
        {
        }
    }
}