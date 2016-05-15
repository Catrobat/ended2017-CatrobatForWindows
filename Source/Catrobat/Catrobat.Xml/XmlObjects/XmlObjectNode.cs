using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public abstract class XmlObjectNode : XmlObject
    {
        internal abstract void LoadFromXml(XElement xRoot);

        internal abstract XElement CreateXml();

        public virtual void LoadReference()
        {
        }
    }
}
