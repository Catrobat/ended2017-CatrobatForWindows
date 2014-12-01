using System;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public abstract class XmlObjectRoot : XmlObject
    {
        protected XElement Root;

        protected XmlObjectRoot() {}

        protected XmlObjectRoot(String xml)
        {
        }

        protected abstract void LoadFromXml(String xmlSource);

        internal abstract XDocument CreateXml();
    }
}