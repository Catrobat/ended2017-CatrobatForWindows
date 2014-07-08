using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlCostumeList : XmlObject
    {
        public List<XmlCostume> Costumes { get; set; }

        public XmlCostumeList()
        {
            Costumes = new List<XmlCostume>();
        }

        public XmlCostumeList(XElement xElement)
        {
            LoadFromXml(xElement);
        } 

        internal override void LoadFromXml(XElement xRoot)
        {
            Costumes = new List<XmlCostume>();
            foreach (var element in xRoot.Elements("look"))
            {
                Costumes.Add(new XmlCostume(element));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("lookList");

            foreach (XmlCostume costume in Costumes)
            {
                xRoot.Add(costume.CreateXml());
            }

            return xRoot;
        }
    }
}