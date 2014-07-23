using System;
using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects
{
    public abstract class XmlRootObject
    {
        protected XElement Root;

        public XmlRootObject() {}

        public XmlRootObject(String xml)
        {
        }

        protected abstract void LoadFromXML(String xmlSource);

        public abstract XDocument CreateXML();
    }
}