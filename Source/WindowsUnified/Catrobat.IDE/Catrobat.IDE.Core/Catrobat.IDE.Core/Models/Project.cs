using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Xml.Converter;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Project : Model, ITestEquatable<Project>
    {
        public const string ProjectCodePath = "code.xml";
        public const string ScreenshotPath = "manual_screenshot.png‏";
        public const string AutomaticScreenshotPath = "automatic_screenshot.png";
        public const string ImagesPath = "images";
        public const string SoundsPath = "sounds";

        #region Properties

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private UploadHeader _uploadHeader;
        public UploadHeader UploadHeader
        {
            get { return _uploadHeader; }
            set { Set(ref _uploadHeader, value); }
        }

        private ObservableCollection<Sprite> _sprites = new ObservableCollection<Sprite>();
        public ObservableCollection<Sprite> Sprites
        {
            get { return _sprites; }
            set { Set(ref _sprites, value); }
        }

        private ObservableCollection<GlobalVariable> _globalVariables = new ObservableCollection<GlobalVariable>();
        public ObservableCollection<GlobalVariable> GlobalVariables
        {
            get { return _globalVariables; }
            set { Set(ref _globalVariables, value); }
        }

        private ObservableCollection<BroadcastMessage> _broadcastMessages = new ObservableCollection<BroadcastMessage>();
        public ObservableCollection<BroadcastMessage> BroadcastMessages
        {
            get { return _broadcastMessages; }
            set { Set(ref _broadcastMessages, value); }
        }

        public string BasePath
        {
            get { return StorageConstants.ProjectsPath + "/" + Name; }
        }

        private PortableImage _screenshot;
        public PortableImage Screenshot
        {
            get
            {
                if (_screenshot == null)
                {
                    var manualScreenshotPath = Path.Combine(BasePath, ScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, AutomaticScreenshotPath);
                    _screenshot = new PortableImage();
                    _screenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);
                    if (ProjectDummyHeader != null)
                    {
                        ProjectDummyHeader.Screenshot = _screenshot;
                    }
                }
                return _screenshot;
            }
            //set
            //{
            //    using (var storage = StorageSystem.GetStorage())
            //    {
            //        if (storage.FileExists(ScreenshotPath))
            //        {
            //            storage.DeleteFile(ScreenshotPath);
            //        }

            //        if (value != null && !value.IsEmpty)
            //            storage.SaveImage(ScreenshotPath, value, true, ImageFormat.Png);
            //    }

            //    RaisePropertyChanged();
            //}
        }

        #endregion

        #region Strange properties

        private LocalProjectHeader _projectDummyHeader;
        public LocalProjectHeader ProjectDummyHeader
        {
            get
            {
                if (_projectDummyHeader != null)
                    return _projectDummyHeader;

                _projectDummyHeader = new LocalProjectHeader
                {
                    ProjectName = Name, 
                    Screenshot = Screenshot
                };

                return _projectDummyHeader;
            }

            set { _projectDummyHeader = value; }
        }

        #endregion

        public Project()
        {
            
        }

        public async Task SetProgramNameAndRenameDirectory(string newProgramName)
        {
            if (newProgramName == Name) return;
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.RenameDirectoryAsync(BasePath, newProgramName);
            }
            Name = newProgramName;
        }

        public async Task Save(string path = null)
        {
            var xmlProject = new XmlProjectConverter().ConvertBack(this);
            await xmlProject.Save(path);
        }

        public void Undo()
        {
            // TODO: implement me
            //throw new NotImplementedException();
        }

        public void Redo()
        {
            // TODO: implement me
            //throw new NotImplementedException();
        }

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((Project) other);
        }

        bool ITestEquatable<Project>.TestEquals(Project other)
        {
            return TestEquals(other);
        }

        protected bool TestEquals(Project other)
        {
            return
                string.Equals(_name, other._name) &&
                string.Equals(_description, other._description) &&
                CollectionExtensions.TestEquals(_broadcastMessages, other._broadcastMessages) &&
                CollectionExtensions.TestEquals(_globalVariables, other._globalVariables) &&
                CollectionExtensions.TestEquals(_sprites, other._sprites) &&
                TestEquals(_uploadHeader, other._uploadHeader);
        }

        #endregion
    }
}
