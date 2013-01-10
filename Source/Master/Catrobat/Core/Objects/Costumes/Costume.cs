using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects.Costumes
{
    public class Costume : DataObject
    {
        private string fileName;
        private byte[] image;
        private string name;
        private Sprite sprite;
        private byte[] thumbnail;

        public Costume(string name, Sprite parent)
        {
            this.name = name;
            fileName = FileNameGenerator.generate() + this.name;
            sprite = parent;
        }

        internal Costume(XElement xElement, Sprite parent)
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

        public byte[] Image
        {
            get
            {
                if (image == null)
                {
                    try
                    {
                        using (IStorage storage = StorageSystem.GetStorage())
                        {
                            image =
                                storage.LoadImage(CatrobatContext.Instance.CurrentProject.BasePath + "/images/" +
                                                  fileName);
                        }
                    }
                    catch
                    {
                    }
                }

                return image;
            }
        }

        public byte[] Thumbnail
        {
            get
            {
                if (thumbnail == null)
                {
                    thumbnail = ImageHelper.CreateThumbnailImage(Image, StaticApplicationSettings.ThumbnailWidth);
                }

                return thumbnail;
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
            var xRoot = new XElement("costumeData");

            xRoot.Add(new XElement("fileName", fileName));

            xRoot.Add(new XElement("name", name));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeData = new Costume(name, parent);

            string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath + "/";
            string absoluteFileNameOld = path + fileName;
            string absoluteFileNameNew = path + newCostumeData.fileName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
                //if (storage.FileExists(absoluteFileNameOld))
                storage.CopyFile(absoluteFileNameOld, absoluteFileNameNew);
                //else
                //  MessageBox.Show("Das Kostüm konnte nicht kopiert werden.", "Kopieren nicht möglich", MessageBoxButton.OK); // TODO: is this used? names should be unique
            }

            return newCostumeData;
        }

        public void Delete()
        {
            string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath + "/" + fileName;
            try
            {
                using (IStorage storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(path))
                        storage.DeleteFile(path);
                }
            }
            catch
            {
            }
        }
    }
}