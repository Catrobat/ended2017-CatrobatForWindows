using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Core.CatrobatObjects.Costumes
{
    public class Costume : DataObject
    {
        private PortableImage _thumbnail;

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

        public PortableImage Image
        {
            get
            {
                if (_thumbnail == null)
                {
                    try
                    {
                        _thumbnail = new PortableImage();
                        var fileName = XmlParserTempProjectHelper.Project.BasePath + "/" + 
                            Project.ImagesPath + "/" + _fileName;
                        _thumbnail.LoadAsync(fileName, null, false);

                        //using (var storage = StorageSystem.GetStorage())
                        //{
                        //    _thumbnail =
                        //        storage.LoadImageThumbnail(XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/" +
                        //                                   _fileName);
                        //}
                    }
                    catch
                    {
                        if (Debugger.IsAttached)
                            Debugger.Break();
                    }
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


        public Costume() { }

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

        public async Task<DataObject> Copy()
        {
            var newCostume = new Costume(_name);

            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/";
            var absoluteFileNameOld = path + _fileName;
            var absoluteFileNameNew = path + newCostume._fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(absoluteFileNameOld, absoluteFileNameNew);
            }

            return newCostume;
        }

        public async Task Delete()
        {
            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/" + _fileName;
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (await storage.FileExistsAsync(path))
                    {
                        await storage.DeleteImageAsync(path);
                    }
                }
            }
            catch { }
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