using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class Project : DataRootObject
    {
        public const string ProjectCodePath = "code.xml";
        public const string ScreenshotPath = "manual_screenshot.png‏";
        public const string AutomaticScreenshotPath = "automatic_screenshot.png";
        public const string ImagesPath = "images";
        public const string SoundsPath = "sounds";


        #region Properties

        private PortableImage _projectScreenshot;
        public PortableImage ProjectScreenshot
        {
            get
            {
                if (_projectScreenshot == null)
                {
                    var manualScreenshotPath = Path.Combine(BasePath, ScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, AutomaticScreenshotPath);
                    _projectScreenshot = new PortableImage();
                    _projectScreenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);

                    if (ProjectDummyHeader != null)
                    {
                        ProjectDummyHeader.Screenshot = _projectScreenshot;
                    }

                    return _projectScreenshot;
                }

                return _projectScreenshot;
            }

            set
            {
                //using (var storage = StorageSystem.GetStorage())
                //{
                //    if (storage.FileExists(ScreenshotPath))
                //    {
                //        storage.DeleteFile(ScreenshotPath);
                //    }

                //    if(value != null && !value.IsEmpty)
                //        storage.SaveImage(ScreenshotPath, value, true, ImageFormat.Png);
                //}

                //RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _broadcastMessages;
        public ObservableCollection<string> BroadcastMessages
        {
            get { return _broadcastMessages; }
            private set
            {
                if (value != null)
                {
                    _broadcastMessages = value;
                }
            }
        }

        private ProjectHeader _projectHeader;
        public ProjectHeader ProjectHeader
        {
            get { return _projectHeader; }
            set
            {
                if (_projectHeader == value)
                {
                    return;
                }

                _projectHeader = value;
                RaisePropertyChanged();
            }
        }

        private SpriteList _spriteList;
        public SpriteList SpriteList
        {
            get { return _spriteList; }
            set
            {
                if (_spriteList == value)
                {
                    return;
                }

                _spriteList = value;
                RaisePropertyChanged();
            }
        }

        private ProjectDummyHeader _projectDummyHeader;
        public ProjectDummyHeader ProjectDummyHeader
        {
            get
            {
                if (_projectDummyHeader != null)
                    return _projectDummyHeader;

                _projectDummyHeader = new ProjectDummyHeader { ProjectName = ProjectHeader.ProgramName, Screenshot = ProjectScreenshot };

                return _projectDummyHeader;
            }

            set { _projectDummyHeader = value; }
        }

        private VariableList _variableList;
        public VariableList VariableList
        {
            get { return _variableList; }
            set
            {
                if (_variableList == value)
                {
                    return;
                }

                _variableList = value;
                RaisePropertyChanged();
            }
        }

        public string BasePath
        {
            get { return CatrobatContextBase.ProjectsPath + "/" + ProjectHeader.ProgramName; }
        }

        #endregion

        public Project()
        {
            SpriteList = new SpriteList();
            _broadcastMessages = new ObservableCollection<string>();
            _variableList = new VariableList();
        }

        public Project(String xmlSource)
            : base(xmlSource)
        {
            _broadcastMessages = new ObservableCollection<string>();
            LoadFromXML(xmlSource);

            PreloadImages();
        }


        public async Task SetProgramNameAndRenameDirectory(string newProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.RenameDirectoryAsync("Projects/" + ProjectHeader.ProgramName, newProgramName);
            }

            ProjectHeader.SetProgramName(newProgramName);
            RaisePropertyChanged(() => ProjectHeader);
        }

        protected override sealed void LoadFromXML(string xml)
        {
            var document = XDocument.Load(new StringReader(xml));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            XmlParserTempProjectHelper.Project = this;

            var project = document.Element("program");
            _projectHeader = new ProjectHeader(project.Element("header"));

            _spriteList = new SpriteList(project.Element("objectList"));

            _variableList = new VariableList(project.Element("variables"));

            LoadReference();
            LoadBroadcastMessages();
        }

        internal override XDocument CreateXML()
        {
            var document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            var xProject = new XElement("program");

            xProject.Add(_projectHeader.CreateXML());

            xProject.Add(_spriteList.CreateXML());

            xProject.Add(_variableList.CreateXML());

            document.Add(xProject);

            return document;
        }

        internal void LoadReference()
        {
            _variableList.LoadReference();
            _spriteList.LoadReference();
        }          

        internal void LoadBroadcastMessages()
        {
            foreach (Sprite sprite in _spriteList.Sprites)
            {
                foreach (Script script in sprite.Scripts.Scripts)
                {
                    if (script is BroadcastScript)
                    {
                        var broadcastScript = script as BroadcastScript;
                        if (!_broadcastMessages.Contains(broadcastScript.ReceivedMessage))
                        {
                            _broadcastMessages.Add(broadcastScript.ReceivedMessage);
                        }
                    }
                    else
                    {
                        foreach (Brick brick in script.Bricks.Bricks)
                        {
                            if (brick is BroadcastBrick)
                            {
                                if (!_broadcastMessages.Contains((brick as BroadcastBrick).BroadcastMessage))
                                {
                                    _broadcastMessages.Add((brick as BroadcastBrick).BroadcastMessage);
                                }
                            }
                            if (brick is BroadcastWaitBrick)
                            {
                                if (!_broadcastMessages.Contains((brick as BroadcastWaitBrick).BroadcastMessage))
                                {
                                    _broadcastMessages.Add((brick as BroadcastWaitBrick).BroadcastMessage);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PreloadImages()
        {
            foreach (Sprite sprite in _spriteList.Sprites)
            {
                foreach (Costume costume in sprite.Costumes.Costumes)
                {
                    //var image = costume.Image; // Forces load of image
                }
            }
        }


        public async Task Save(string path = null)
        {
            if (path == null)
            {
                path = BasePath + "/" + ProjectCodePath;
            }

            if (Debugger.IsAttached)
            {
                await SaveInternal(path);
            }
            else
            {
                try
                {
                    await SaveInternal(path);
                }
                catch
                {
                    throw new Exception("Cannot write Project");
                }
            }
        }

        private async Task SaveInternal(string path)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var writer = new XmlStringWriter();
                var document = CreateXML();
                document.Save(writer, SaveOptions.None);

                var xml = writer.GetStringBuilder().ToString();
                await storage.WriteTextFileAsync(path, xml);
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

        public override bool Equals(DataRootObject other)
        {
            var otherProject = other as Project;

            if (otherProject == null)
                return false;

            var count = BroadcastMessages.Count;
            var otherCount = otherProject.BroadcastMessages.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!BroadcastMessages[i].Equals(otherProject.BroadcastMessages[i]))
                    return false;

            if (!ProjectHeader.Equals(otherProject.ProjectHeader))
                return false;

            if (!SpriteList.Equals(otherProject.SpriteList))
                return false;

            if (!VariableList.Equals(otherProject.VariableList))
                return false;

            return true;
        }
    }
}
