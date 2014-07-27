using System.Diagnostics;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Costume : Model, IAsyncCloneable<Program>
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { Set(ref _fileName, value); }
        }

        private PortableImage _image;
        public PortableImage Image
        {
            get
            {
                if (_image == null)
                {
                    try
                    {
                        _image = new PortableImage();
                        var fileName = XmlParserTempProjectHelper.Project.BasePath + "/" + StorageConstants.ProgramImagesPath + "/" + _fileName;
                        _image.LoadAsync(fileName, null, false);

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
                return _image;
            }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        #endregion

        public Costume()
        {
            
        }

        public Costume(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        public async Task Delete(Program project)
        {
            var path = project.BasePath + "/" + StorageConstants.ProgramImagesPath + "/" + _fileName;
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
            catch
            {
                
            }
        }

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((Costume) other);
        }

        protected bool TestEquals(Costume other)
        {
            return string.Equals(_fileName, other._fileName) && 
                string.Equals(_name, other._name);
        }

        #endregion

        #region Implements IAsyncCloneable

        async Task<object> IAsyncCloneable<Program>.CloneInstance(Program project)
        {
            var result = new Costume(Name);
            var directory = project.BasePath + "/" + StorageConstants.ProgramImagesPath + "/";
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(directory + FileName, directory + result.FileName);
            }
            return result;
        }

        #endregion
    }
}
