using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlCostumeList : XmlObject
    {
        public ObservableCollection<XmlCostume> Costumes { get; set; }

        public XmlCostumeList()
        {
            Costumes = new ObservableCollection<XmlCostume>();
        }

        public XmlCostumeList(XElement xElement)
        {
            LoadFromXml(xElement);
        } 

        internal override void LoadFromXml(XElement xRoot)
        {
            Costumes = new ObservableCollection<XmlCostume>();
            foreach (XElement element in xRoot.Elements("look"))
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

        public async Task<XmlObject> Copy()
        {
            var newCostumeList = new XmlCostumeList();
            foreach (XmlCostume costume in Costumes)
            {
                newCostumeList.Costumes.Add(await costume.Copy() as XmlCostume);
            }

            return newCostumeList;
        }

        public async Task Delete()
        {
            foreach (XmlCostume costume in Costumes)
            {
                await costume.Delete();
            }
        }

        public override bool Equals(XmlObject other)
        {
            var otherCostumeList = other as XmlCostumeList;

            if (otherCostumeList == null)
                return false;

            var count = Costumes.Count;
            var otherCount = otherCostumeList.Costumes.Count;

            if (count != otherCount)
                return false;

            for(int i = 0; i < count; i++)
                if(!Costumes[i].Equals(otherCostumeList.Costumes[i]))
                    return false;
            
            return true;
        }
    }
}