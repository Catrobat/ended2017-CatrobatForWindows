using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeList : DataObject
    {
        private Sprite _sprite;

        public CostumeList(Sprite parent)
        {
            Costumes = new ObservableCollection<Costume>();
            _sprite = parent;
        }

        public CostumeList(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<Costume> Costumes { get; set; }

        public Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Costumes = new ObservableCollection<Costume>();
            foreach (XElement element in xRoot.Elements("costumeData"))
            {
                Costumes.Add(new Costume(element, _sprite));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("costumeDataList");

            foreach (Costume costume in Costumes)
            {
                xRoot.Add(costume.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeDataList = new CostumeList(parent);
            foreach (Costume costume in Costumes)
            {
                newCostumeDataList.Costumes.Add(costume.Copy(parent) as Costume);
            }

            return newCostumeDataList;
        }

        public void Delete()
        {
            foreach (Costume costume in Costumes)
            {
                costume.Delete();
            }
        }
    }
}