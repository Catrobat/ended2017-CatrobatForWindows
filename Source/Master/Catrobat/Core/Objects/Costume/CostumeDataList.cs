using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class CostumeDataList : DataObject
    {
        private Sprite sprite;

        public CostumeDataList(Sprite parent)
        {
            Costumes = new ObservableCollection<CostumeData>();
            sprite = parent;
        }

        public CostumeDataList(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<CostumeData> Costumes { get; set; }

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
            Costumes = new ObservableCollection<CostumeData>();
            foreach (XElement element in xRoot.Elements("costumeData"))
                Costumes.Add(new CostumeData(element, sprite));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("costumeDataList");

            foreach (CostumeData costume in Costumes)
                xRoot.Add(costume.CreateXML());

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeDataList = new CostumeDataList(parent);
            foreach (CostumeData costume in Costumes)
                newCostumeDataList.Costumes.Add(costume.Copy(parent) as CostumeData);

            return newCostumeDataList;
        }

        public void Delete()
        {
            foreach (CostumeData costume in Costumes)
                costume.Delete();
        }
    }
}