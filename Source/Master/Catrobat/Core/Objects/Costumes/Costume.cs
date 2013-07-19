using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects.Costumes
{
    public class Costume : DataObject
    {
        private object _thumbnail;

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName == value)
                {
                    return;
                }

                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }

        public object Image
        {
            get
            {
                if (_thumbnail == null)
                {
                    try
                    {
                        using (var storage = StorageSystem.GetStorage())
                        {
                            _thumbnail = storage.LoadImageThumbnail(CatrobatContext.GetContext().CurrentProject.BasePath + "/images/" + _fileName);
                        }
                    }
                    catch { }
                }

                return _thumbnail;
            }
        }

        //public byte[] Thumbnail
        //{
        //  get
        //  {
        //    if (_thumbnail == null)
        //    {
        //      _thumbnail = ImageHelper.CreateThumbnailImage(Image, StaticApplicationSettings.ThumbnailWidth);
        //    }

        //    return _thumbnail;
        //  }
        //}
        

        public Costume() {}

        public Costume(string name)
        {
            _name = name;
            _fileName = FileNameGenerator.Generate() + _name;
        }

        internal Costume(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _fileName = xRoot.Element("fileName").Value;
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XElement("fileName", _fileName));

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public DataObject Copy()
        {
            var newCostumeData = new Costume(_name);

            var path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath + "/";
            var absoluteFileNameOld = path + _fileName;
            var absoluteFileNameNew = path + newCostumeData._fileName;

            using (var storage = StorageSystem.GetStorage())
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
            var path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath + "/" + _fileName;
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(path))
                    {
                        storage.DeleteFile(path);
                    }
                }
            }
            catch {}
        }
    }
}