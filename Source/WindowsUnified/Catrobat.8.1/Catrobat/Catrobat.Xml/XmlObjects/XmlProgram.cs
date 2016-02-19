using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlProgram : XmlObjectRoot, IProject
    {
        #region Properties

        public List<string> BroadcastMessages { get; private set; }

        public XmlProjectHeader ProgramHeader { get; set; }

        public XmlSpriteList SpriteList { get; set; }

        public XmlVariableList VariableList { get; set; }

        public IList<IObject> Objects
        {
            get { throw new NotImplementedException(); }
            //get { return SpriteList.Sprites.Cast<IObject>().ToList(); }
            set { }
        }

        public IList<IUserVariable> Variables
        {
            get { throw new NotImplementedException(); }
            //get { return VariableList.ProgramVariableList.UserVariableReferences.Cast<IUserVariable>().ToList(); }
            set { }
        }

        IHeader IProject.Header { get { return ProgramHeader; } set { } }

        #endregion

        public XmlProgram()
        {
            SpriteList = new XmlSpriteList();
            BroadcastMessages = new List<string>();
            VariableList = new XmlVariableList();
        }

        public XmlProgram(String xmlSource) : base(xmlSource)
        {
            BroadcastMessages = new List<string>();
            LoadFromXml(xmlSource);
        }


        protected override sealed void LoadFromXml(string xml)
        {
            var document = XDocument.Load(new StringReader(xml));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            XmlParserTempProjectHelper.Program = this;

           var project = document.Element(XmlConstants.Program);
            ProgramHeader = new XmlProjectHeader(project.Element(XmlConstants.Header));
            SpriteList = new XmlSpriteList(project.Element(XmlConstants.ObjectList));
            VariableList = new XmlVariableList(project.Element(XmlConstants.Variables));

            foreach (var a in VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                a.Sprite.Variables = a.VariableList;
            }
            LoadReference();
            LoadBroadcastMessages();
        }

        internal override XDocument CreateXml()
        {
            XmlParserTempProjectHelper.currentObjectNum = 0;
            XmlParserTempProjectHelper.currentScriptNum = 0;
            XmlParserTempProjectHelper.currentBrickNum = 0;
            XmlParserTempProjectHelper.currentVariableNum = 0;

            XmlParserTempProjectHelper.Document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

            XmlParserTempProjectHelper.Program = this;

            var xProject = new XElement(XmlConstants.Program);
            xProject.Add(ProgramHeader.CreateXml());
            xProject.Add(SpriteList.CreateXml());
            xProject.Add(VariableList.CreateXml());
            XmlParserTempProjectHelper.Document.Add(xProject);

            return XmlParserTempProjectHelper.Document;
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

        //public async Task Save(string path = null)
        //{
        //    // TODO XML: move to IDE.Core

        //    if (path == null)
        //    {
        //        path = BasePath + "/" + StorageConstants.ProgramCodePath;
        //    }

        //    if (Debugger.IsAttached)
        //    {
        //        await SaveInternal(path);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            await SaveInternal(path);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Cannot write Project", ex);
        //        }
        //    }
        //}

        //private async Task SaveInternal(string path)
        //{
        //    // TODO XML: move to IDE.Core

        //    using (var storage = StorageSystem.GetStorage())
        //    {
        //        var writer = new XmlStringWriter();
        //        var document = CreateXml();
        //        document.Save(writer, SaveOptions.None);

        //        var xml = writer.GetStringBuilder().ToString();
        //        await storage.WriteTextFileAsync(path, xml);
        //    }
        //}


        public string ToXmlString()
        {
            var writer = new XmlStringWriter();
            var document = CreateXml();
            document.Save(writer, SaveOptions.None);
            return writer.ToString();
        }

    }
}
