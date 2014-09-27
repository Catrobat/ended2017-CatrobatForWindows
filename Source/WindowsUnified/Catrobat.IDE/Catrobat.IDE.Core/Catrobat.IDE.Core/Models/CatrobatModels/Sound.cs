using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models.CatrobatModels;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Sound : CatrobatModelBase, IAsyncCloneable<Program>, ISelectable
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

        public Sound()
        {
            
        }

        public Sound(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        public async Task Delete(Program project)
        {
            var path = project.BasePath + "/" + StorageConstants.ProgramSoundsPath + "/" + _fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteFileAsync(path);
            }
        }

        #region Implements ITestEquatable

        protected override bool TestEquals(ModelBase other)
        {
            return base.TestEquals(other) && TestEquals((Sound) other);
        }

        protected bool TestEquals(Sound other)
        {
            return string.Equals(_fileName, other._fileName) && 
                string.Equals(_name, other._name);
        }

        #endregion

        #region Implements IAsyncCloneable

        async Task<object> IAsyncCloneable<Program>.CloneInstance(Program program)
        {
            var result = new Sound(Name);
            var directory = program.BasePath + "/" + StorageConstants.ProgramSoundsPath + "/";
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(directory + FileName, directory + result.FileName);
            }
            return result;
        }

        #endregion
    }
}
