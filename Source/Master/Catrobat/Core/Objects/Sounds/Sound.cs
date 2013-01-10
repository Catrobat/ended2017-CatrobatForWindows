using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects.Sounds
{
    public class Sound: DataObject
    {
        private string fileName;
        private string name;

        private Sprite sprite;

        public Sound(string name, Sprite parent)
        {
            this.name = name;
            fileName = FileNameGenerator.generate() + this.name;
            sprite = parent;
        }

        internal Sound(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                    return;

                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName == value)
                    return;

                fileName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FileName"));
            }
        }

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
            fileName = xRoot.Element("fileName").Value;
            name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("Sound");

            xRoot.Add(new XElement("fileName", fileName));

            xRoot.Add(new XElement("name", name));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundInfo = new Sound(name, parent);

            string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/";
            string absoluteFileNameOld = path + fileName;
            string absoluteFileNameNew = path + newSoundInfo.fileName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
                //if (storage.FileExists(absoluteFileNameOld))
                storage.CopyFile(absoluteFileNameOld, absoluteFileNameNew);
                //else
                //  MessageBox.Show("Der Klang konnte nicht kopiert werden.", "Kopieren nicht möglich", MessageBoxButton.OK); // TODO: is this used? names should be unique
            }

            return newSoundInfo;
        }

        public void Delete()
        {
            string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + fileName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
                if (storage.FileExists(path))
                    storage.DeleteFile(path);
            }
        }
    }
}