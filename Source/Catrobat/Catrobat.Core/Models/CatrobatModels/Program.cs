using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models.CatrobatModels;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Program : CatrobatModelBase, ITestEquatable<Program>
    {
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
            get { return StorageConstants.ProgramsPath + "\\" + Name; }
        }

        private PortableImage _screenshot;
        public PortableImage Screenshot
        {
            get
            {
                if (_screenshot == null)
                {
                    var manualScreenshotPath = Path.Combine(BasePath, StorageConstants.ProgramManualScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, StorageConstants.ProgramAutomaticScreenshotPath);
                    _screenshot = new PortableImage();
                    _screenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);
                    if (LocalProgramHeader != null)
                    {
                        LocalProgramHeader.Screenshot = _screenshot;
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

        private LocalProgramHeader _localProgramHeader;
        public LocalProgramHeader LocalProgramHeader
        {
            get
            {
                if (_localProgramHeader != null)
                    return _localProgramHeader;

                _localProgramHeader = new LocalProgramHeader
                {
                    ProjectName = Name, 
                    Screenshot = Screenshot
                };

                return _localProgramHeader;
            }

            set { _localProgramHeader = value; }
        }

        #endregion

        public Program()
        {
            
        }

        ~Program()
        {

        }

        public async Task SetProgramNameAndRenameDirectory(string newProgramName)
        {
            if (newProgramName == Name) 
                return;
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.RenameDirectoryAsync(BasePath, newProgramName);
            }
            Name = newProgramName;
        }

        public async Task Save(string path = null)
        {
                if (path == null)
                    path = Path.Combine(BasePath, StorageConstants.ProgramCodePath);

                var programConverter = new ProgramConverter();
                var xmlProgram = programConverter.Convert(this);

                var xmlString = xmlProgram.ToXmlString();

                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.WriteTextFileAsync(path, xmlString);
                }
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

        protected override bool TestEquals(ModelBase other)
        {
            return base.TestEquals(other) && TestEquals((Program) other);
        }

        bool ITestEquatable<Program>.TestEquals(Program other)
        {
            return TestEquals(other);
        }

        public bool TestEquals(Program other)
        {
            bool equalNames = string.Equals(_name, other._name);
            bool equalDescriptions = string.Equals(_description, other._description);
            bool equalBroadcastMessages = CollectionExtensions.TestEquals(_broadcastMessages, other._broadcastMessages);
            bool equalGlobalVariables = CollectionExtensions.TestEquals(_globalVariables, other._globalVariables);
            bool equalSprites = CollectionExtensions.TestEquals(_sprites, other._sprites);
            bool equalUploadHeaders = TestEquals(_uploadHeader, other._uploadHeader);

            return
                equalNames &&
                equalDescriptions &&
                equalBroadcastMessages &&
                equalGlobalVariables &&
                equalSprites &&
                equalUploadHeaders;
        }

        #endregion
    }
}
