using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
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

        public List<string> BroadcastMessages { get; private set; }

        public XmlProjectHeader ProjectHeader { get; set; }

        public XmlSpriteList SpriteList { get; set; }

        public XmlVariableList VariableList { get; set; }

        public string BasePath
        {
            get { return CatrobatContextBase.ProjectsPath + "/" + ProjectHeader.ProgramName; }
        }

        #endregion

        public XmlProject()
        {
            SpriteList = new XmlSpriteList();
            BroadcastMessages = new List<string>();
            VariableList = new XmlVariableList();
        }

        public XmlProject(String xmlSource) : base(xmlSource)
        {
            BroadcastMessages = new List<string>();
            LoadFromXML(xmlSource);
        }


        protected override sealed void LoadFromXML(string xml)
        {
            var document = XDocument.Load(new StringReader(xml));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            XmlParserTempProjectHelper.Project = this;

            var project = document.Element("program");
            ProjectHeader = new XmlProjectHeader(project.Element("header"));
            SpriteList = new XmlSpriteList(project.Element("objectList"));
            VariableList = new XmlVariableList(project.Element("variables"));

            LoadReference();
            LoadBroadcastMessages();
        }

        internal override XDocument CreateXML()
        {
            var document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            XmlParserTempProjectHelper.Project = this;

            var xProject = new XElement("program");
            xProject.Add(ProjectHeader.CreateXml());
            xProject.Add(SpriteList.CreateXml());
            xProject.Add(VariableList.CreateXml());
            document.Add(xProject);

            return document;
        }

        internal void LoadReference()
        {
            VariableList.LoadReference();
            SpriteList.LoadReference();
        }          

        internal void LoadBroadcastMessages()
        {
            foreach (XmlSprite sprite in SpriteList.Sprites)
            {
                foreach (XmlScript script in sprite.Scripts.Scripts)
                {
                    if (script is XmlBroadcastScript)
                    {
                        var broadcastScript = script as XmlBroadcastScript;
                        if (!BroadcastMessages.Contains(broadcastScript.ReceivedMessage))
                        {
                            BroadcastMessages.Add(broadcastScript.ReceivedMessage);
                        }
                    }
                    else
                    {
                        foreach (XmlBrick brick in script.Bricks.Bricks)
                        {
                            if (brick is XmlBroadcastBrick)
                            {
                                if (!BroadcastMessages.Contains((brick as XmlBroadcastBrick).BroadcastMessage))
                                {
                                    BroadcastMessages.Add((brick as XmlBroadcastBrick).BroadcastMessage);
                                }
                            }
                            if (brick is XmlBroadcastWaitBrick)
                            {
                                if (!BroadcastMessages.Contains((brick as XmlBroadcastWaitBrick).BroadcastMessage))
                                {
                                    BroadcastMessages.Add((brick as XmlBroadcastWaitBrick).BroadcastMessage);
                                }
                            }
                        }
                    }
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
    }
}
