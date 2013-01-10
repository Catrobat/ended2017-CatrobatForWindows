using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeList : DataObject
    {
        private Sprite sprite;

        public CostumeList(Sprite parent)
        {
            Costumes = new ObservableCollection<Costumes.Costume>();
            sprite = parent;
        }

        public CostumeList(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<Costumes.Costume> Costumes { get; set; }

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if (sprite == value)
                    return;

                sprite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Costumes = new ObservableCollection<Costumes.Costume>();
            foreach (XElement element in xRoot.Elements("costumeData"))
                Costumes.Add(new Costumes.Costume(element, sprite));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("costumeDataList");

            foreach (Costumes.Costume costume in Costumes)
                xRoot.Add(costume.CreateXML());

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeDataList = new CostumeList(parent);
            foreach (Costumes.Costume costume in Costumes)
                newCostumeDataList.Costumes.Add(costume.Copy(parent) as Costumes.Costume);

            return newCostumeDataList;
        }

        public void Delete()
        {
            foreach (Costumes.Costume costume in Costumes)
                costume.Delete();
        }
    }
}