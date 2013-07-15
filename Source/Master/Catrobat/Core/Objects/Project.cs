using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        public const string ProjectCodePath = "projectcode.xml";
        public const string ScreenshotPath = "screenshot.png";
        public const string ImagesPath = "images";
        public const string SoundsPath = "sounds";

        private ObservableCollection<string> _broadcastMessages;


        private object _projectScreenshot;
        public object ProjectScreenshot
        {
            get
            {
                if (_projectScreenshot == null)
                {
                    using (var storage = StorageSystem.GetStorage())
                    {
                        _projectScreenshot =
                            storage.LoadImage(CatrobatContext.GetContext().CurrentProject.BasePath + "/screenshot.png");
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
                else
                {
                    object image = null;

                    using (var storage = StorageSystem.GetStorage())
                    {
                        image = storage.LoadImageThumbnail(BasePath + "/" + ScreenshotPath);
                    }

                    _projectDummyHeader = new ProjectDummyHeader { ProjectName = ProjectHeader.ProgramName, Screenshot = image };
                }

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
            get { return "Projects/" + ProjectHeader.ProgramName; }
        }


        public Project()
        {
            _broadcastMessages = new ObservableCollection<string>();
        }

        public Project(String xmlSource)
            : base(xmlSource)
        {
            _broadcastMessages = new ObservableCollection<string>();
            LoadBroadcastMessages();
            PreloadImages();
        }


        public void SetSetProgramName(string newProgramName)
        {
            ProjectHeader.SetProgramName(newProgramName);
        }

        protected override void LoadFromXML(String xml)
        {
            ReferenceHelper.Project = this;

            _document = XDocument.Load(new StringReader(xml));
            _document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            //Converter.Converter.Convert(_document);

            var project = _document.Element("program");
            _projectHeader = new ProjectHeader(project.Element("header"));
            _spriteList = new SpriteList(this);
            _spriteList.LoadFromXML(project.Element("objectList"));
            _variableList = new VariableList(project.Element("variables"));

            ReferenceHelper.Project = null;
        }

        internal override XDocument CreateXML()
        {
            ReferenceHelper.Project = this;

            _document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            var xProject = new XElement("project");

            xProject.Add(_projectHeader.CreateXML());

            xProject.Add(_spriteList.CreateXML());

            xProject.Add(_variableList.CreateXML());

            _document.Add(xProject);

            ReferenceHelper.Project = null;

            return _document;
        }

        private void LoadBroadcastMessages()
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
                    _document.Save(writer, SaveOptions.None);

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
    }
}