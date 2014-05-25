using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlProject : DataRootObject
    {
        #region Properties

        private PortableImage _projectScreenshot;
        public PortableImage ProjectScreenshot
        {
            get
            {
                if (_projectScreenshot == null)
                {
                    var manualScreenshotPath = Path.Combine(BasePath, Project.ScreenshotPath);
                    var automaticProjectScreenshotPath = Path.Combine(BasePath, Project.AutomaticScreenshotPath);
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

        private XmlProjectHeader _projectHeader;
        public XmlProjectHeader ProjectHeader
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

        private XmlSpriteList _spriteList;
        public XmlSpriteList SpriteList
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

        private XmlVariableList _variableList;
        public XmlVariableList VariableList
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

        public XmlProject()
        {
            SpriteList = new XmlSpriteList();
            _broadcastMessages = new ObservableCollection<string>();
            _variableList = new XmlVariableList();
        }

        public XmlProject(String xmlSource)
            : base(xmlSource)
        {
            _broadcastMessages = new ObservableCollection<string>();
            LoadFromXML(xmlSource);

            PreloadImages();
        }


        protected override sealed void LoadFromXML(string xml)
        {
            var document = XDocument.Load(new StringReader(xml));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            XmlParserTempProjectHelper.Project = this;

            var project = document.Element("program");
            _projectHeader = new XmlProjectHeader(project.Element("header"));
            _spriteList = new XmlSpriteList(project.Element("objectList"));
            _variableList = new XmlVariableList(project.Element("variables"));

            LoadReference();
            LoadBroadcastMessages();
        }

        internal override XDocument CreateXML()
        {
            var document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            XmlParserTempProjectHelper.Project = this;

            var xProject = new XElement("program");
            xProject.Add(_projectHeader.CreateXml());
            xProject.Add(_spriteList.CreateXml());
            xProject.Add(_variableList.CreateXml());
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
            foreach (XmlSprite sprite in _spriteList.Sprites)
            {
                foreach (XmlScript script in sprite.Scripts.Scripts)
                {
                    if (script is XmlBroadcastScript)
                    {
                        var broadcastScript = script as XmlBroadcastScript;
                        if (!_broadcastMessages.Contains(broadcastScript.ReceivedMessage))
                        {
                            _broadcastMessages.Add(broadcastScript.ReceivedMessage);
                        }
                    }
                    else
                    {
                        foreach (XmlBrick brick in script.Bricks.Bricks)
                        {
                            if (brick is XmlBroadcastBrick)
                            {
                                if (!_broadcastMessages.Contains((brick as XmlBroadcastBrick).BroadcastMessage))
                                {
                                    _broadcastMessages.Add((brick as XmlBroadcastBrick).BroadcastMessage);
                                }
                            }
                            if (brick is XmlBroadcastWaitBrick)
                            {
                                if (!_broadcastMessages.Contains((brick as XmlBroadcastWaitBrick).BroadcastMessage))
                                {
                                    _broadcastMessages.Add((brick as XmlBroadcastWaitBrick).BroadcastMessage);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PreloadImages()
        {
            foreach (XmlSprite sprite in _spriteList.Sprites)
            {
                foreach (XmlCostume costume in sprite.Costumes.Costumes)
                {
                    //var image = costume.Image; // Forces load of image
                }
            }
        }


        public async Task Save(string path = null)
        {
            if (path == null)
            {
                path = BasePath + "/" + Project.ProjectCodePath;
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
                catch (Exception ex)
                {
                    throw new Exception("Cannot write Project", ex);
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

        public override bool Equals(DataRootObject other)
        {
            var otherProject = other as XmlProject;

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
