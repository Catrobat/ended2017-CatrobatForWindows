using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.Storage;
using Catrobat.Core.Services.Common;

namespace Catrobat.Core.CatrobatObjects.Costumes
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
                            _thumbnail = storage.LoadImageThumbnail(XmlParserTempProjectHelper.Project.BasePath + "/images/" + _fileName);
                        }
                    }
                    catch { }
                }

                return _thumbnail;
            }

            set
            {
                _thumbnail = value;
                RaisePropertyChanged(() => Image);
            }
        }

        public void ResetImage()
        {
            _thumbnail = null;
            RaisePropertyChanged(() => Image);
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
            _fileName = FileNameGenerationHelper.Generate() + _name;
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
            var newCostume = new Costume(_name);

            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/";
            var absoluteFileNameOld = path + _fileName;
            var absoluteFileNameNew = path + newCostume._fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                //if (storage.FileExists(absoluteFileNameOld))
                storage.CopyFile(absoluteFileNameOld, absoluteFileNameNew);
                //else
                //  MessageBox.Show("Das Kostüm konnte nicht kopiert werden.", "Kopieren nicht möglich", MessageBoxButton.OK); // TODO: is this used? names should be unique
            }

            return newCostume;
        }

        public void Delete()
        {
            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/" + _fileName;
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

        public override bool Equals(DataObject other)
        {
            var otherCostume = other as Costume;

            if (otherCostume == null)
                return false;

            if (Name != otherCostume.Name)
                return false;
            if (FileName != otherCostume.FileName)
                return false;

            return true;
        }
    }
}