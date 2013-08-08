using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects
{
    public class Project : DataRootObject
    {
        public const string ProjectCodePath = "code.xml";
        public const string ScreenshotPath = "screenshot.png";
        public const string ImagesPath = "images";
        public const string SoundsPath = "sounds";
        public const string AutomaticScreenshotPath = "automatic_screenshot.png";

        #region Properties

        private object _projectScreenshot;
        public object ProjectScreenshot
        {
            get
            {
                if (_projectScreenshot == null)
                {
                    var screenshotPath = Path.Combine(BasePath, ScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, AutomaticScreenshotPath);
                        
                    using (var storage = StorageSystem.GetStorage())
                    {
                        if(storage.FileExists(screenshotPath))
                            _projectScreenshot = storage.LoadImage(screenshotPath);
                        else if(storage.FileExists(automaticProjectScreenshotPath))
                            _projectScreenshot = storage.LoadImage(automaticProjectScreenshotPath);
                    }
                }

                if (ProjectDummyHeader != null)
                {
                    ProjectDummyHeader.Screenshot = _projectScreenshot;
                }

                return _projectScreenshot;
            }

            set
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(ScreenshotPath))
                    {
                        storage.DeleteFile(ScreenshotPath);
                    }

                    storage.SaveImage(ScreenshotPath, value);
                }

                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _broadcastMessages;
        public ObservableCollection<string> BroadcastMessages
        {
            get { return _broadcastMessages; }
            set
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
                {
                    return _projectDummyHeader;
                }

                object image = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    var screenshotPath = Path.Combine(BasePath, ScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, AutomaticScreenshotPath);

                    if (storage.FileExists(screenshotPath))
                        image = storage.LoadImageThumbnail(screenshotPath);
                    else if (storage.FileExists(automaticProjectScreenshotPath))
                        image = storage.LoadImageThumbnail(automaticProjectScreenshotPath);
                }

                _projectDummyHeader = new ProjectDummyHeader { ProjectName = ProjectHeader.ProgramName, Screenshot = image };

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


        public void SetProgramName(string newProgramName)
        {
            ProjectHeader.SetProgramName(newProgramName);
            RaisePropertyChanged(() => ProjectHeader);
        }

        protected override void LoadFromXML(String xml)
        {
            Document = XDocument.Load(new StringReader(xml));
            Document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            ProjectHolder.Project = this;

            //Converter.Converter.Convert(_document);

            var project = Document.Element("program");
            _projectHeader = new ProjectHeader(project.Element("header"));

            _spriteList = new SpriteList();
            _spriteList.LoadFromXML(project.Element("objectList"));

            _variableList = new VariableList(project.Element("variables"));
            
            LoadReference();
            LoadBroadcastMessages();
        }

        internal override XDocument CreateXML()
        {
            Document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            var xProject = new XElement("program");

            xProject.Add(_projectHeader.CreateXML());

            xProject.Add(_spriteList.CreateXML());

            xProject.Add(_variableList.CreateXML());

            Document.Add(xProject);

            return Document;
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


        public void Save(string path = null)
        {
            var xDocument = CreateXML();

            if (path == null)
            {
                path = BasePath + "/" + ProjectCodePath;
            }

            using (var storage = StorageSystem.GetStorage())
            {
                try
                {
                    var writer = new XmlStringWriter();
                    Document.Save(writer, SaveOptions.None);

                    var xml = writer.GetStringBuilder().ToString();
                    storage.WriteTextFile(path, xml);
                }
                catch
                {
                    throw new Exception("Cannot write Project");
                }
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

            if(!SpriteList.Equals(otherProject.SpriteList))
                return false;

            if (!VariableList.Equals(otherProject.VariableList))
                return false;

            return true;
        }
    }
}