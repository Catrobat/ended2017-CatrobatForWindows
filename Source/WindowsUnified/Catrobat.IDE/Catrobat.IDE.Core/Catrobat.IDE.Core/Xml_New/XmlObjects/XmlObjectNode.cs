using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public abstract class XmlObjectNode
    {
        internal abstract void LoadFromXml(XElement xRoot);

        internal abstract XElement CreateXml();

        internal virtual void LoadReference()
        {
        }
    }
}