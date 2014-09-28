using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models.CatrobatModels;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using System;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Look : CatrobatModelBase, IAsyncCloneable<Program>, ISelectable
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
                        string fileName = null;
                        if (_fileName != null)
                        {
                            fileName = Path.Combine(StorageConstants.ProgramsPath,
                             XmlParserTempProjectHelper.Project.ProjectHeader.ProgramName,
                             StorageConstants.ProgramLooksPath, _fileName);

                            //XmlParserTempProjectHelper.Project.BasePath + "/" + 
                            //StorageConstants.ProgramLooksPath + "/" + _fileName;
                            //_image.LoadAsync(fileName, null, false);
                        }
                        _image.ImagePath = fileName;
                        //using (var storage = StorageSystem.GetStorage())
                        //{
                        //    _thumbnail =
                        //        storage.LoadImageThumbnail(XmlParserTempProjectHelper.Project.BasePath + "/" + Project.ImagesPath + "/" +
                        //                                   _fileName);
                        //}
                    }
                    catch (Exception exc)
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

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #endregion

        public Look()
        {
            
        }

        public Look(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        public async Task Delete(Program project)
        {
            var path = project.BasePath + "/" + StorageConstants.ProgramLooksPath + "/" + _fileName;
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

        protected override bool TestEquals(ModelBase other)
        {
            return base.TestEquals(other) && TestEquals((Look) other);
        }

        protected bool TestEquals(Look other)
        {
            return string.Equals(_fileName, other._fileName) && 
                string.Equals(_name, other._name);
        }

        #endregion

        #region Implements IAsyncCloneable

        async Task<object> IAsyncCloneable<Program>.CloneInstance(Program program)
        {
            var result = new Look(Name);
            var directory = program.BasePath + "/" + StorageConstants.ProgramLooksPath + "/";
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(directory + FileName, directory + result.FileName);
            }
            return result;
        }
        
        #endregion
    }
}
