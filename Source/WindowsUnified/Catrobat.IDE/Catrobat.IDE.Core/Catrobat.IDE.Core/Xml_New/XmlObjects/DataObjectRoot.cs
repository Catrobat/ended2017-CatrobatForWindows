using System;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public abstract class DataObjectRoot
    {
        protected XElement Root;

        protected DataObjectRoot() {}

        protected DataObjectRoot(String xml)
        {
        }

        protected abstract void LoadFromXml(String xmlSource);

        internal abstract XDocument CreateXml();
    }
}