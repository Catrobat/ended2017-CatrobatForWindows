using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Costumes
{
    public class CostumeList : DataObject
    {
        public ObservableCollection<Costume> Costumes { get; set; }

        public CostumeList()
        {
            Costumes = new ObservableCollection<Costume>();
        }

        public CostumeList(XElement xElement)
        {
            LoadFromXML(xElement);
        } 

        internal override void LoadFromXML(XElement xRoot)
        {
            Costumes = new ObservableCollection<Costume>();
            foreach (XElement element in xRoot.Elements("look"))
            {
                Costumes.Add(new Costume(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("lookList");

            foreach (Costume costume in Costumes)
            {
                xRoot.Add(costume.CreateXML());
            }

            return xRoot;
        }

        public async Task<DataObject> Copy()
        {
            var newCostumeList = new CostumeList();
            foreach (Costume costume in Costumes)
            {
                newCostumeList.Costumes.Add(await costume.Copy() as Costume);
            }

            return newCostumeList;
        }

        public async Task Delete()
        {
            foreach (Costume costume in Costumes)
            {
                await costume.Delete();
            }
        }

        public override bool Equals(DataObject other)
        {
            var otherCostumeList = other as CostumeList;

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