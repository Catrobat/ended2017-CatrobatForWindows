using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlCostume : XmlObject
    {

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

        private PortableImage _thumbnail;
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


        public XmlCostume() { }

        public XmlCostume(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        internal XmlCostume(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _fileName = xRoot.Element("fileName").Value;
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XElement("fileName", _fileName));

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public async Task<XmlObject> Copy()
        {
            var newCostume = new XmlCostume(_name);

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

        public override bool Equals(XmlObject other)
        {
            var otherCostume = other as XmlCostume;

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