using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Interpreter.Objects.Costumes
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

        public DataObject Copy()
        {
            var newCostumeList = new CostumeList();
            foreach (Costume costume in Costumes)
            {
                newCostumeList.Costumes.Add(costume.Copy() as Costume);
            }

            return newCostumeList;
        }

        public void Delete()
        {
            foreach (Costume costume in Costumes)
            {
                costume.Delete();
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