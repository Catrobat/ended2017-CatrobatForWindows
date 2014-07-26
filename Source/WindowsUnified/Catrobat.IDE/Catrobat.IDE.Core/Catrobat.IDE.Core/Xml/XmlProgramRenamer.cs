using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml
{
    public enum XmlRenameStatus {Success, Error}

    public class XmlProjectRenamerResult
    {
        public XmlRenameStatus Status { get; set; }

        public string NewProjectName { get; set; }

        public string NewProjectCode { get; set; }
    }

    public class XmlProgramRenamer
    {
        public static XmlProjectRenamerResult RenameProjectFromCode(
            string projectCode, string newProjectName)
        {
            try
            {
                var document = XDocument.Load(new StringReader(projectCode));
                document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                var project = document.Element("program");
                var header = project.Element("header");
                var programName = header.Element("programName");
                programName.SetValue(newProjectName);

                var writer = new XmlStringWriter();
                document.Save(writer, SaveOptions.None);
                var newProjectCode = writer.ToString();

                return new XmlProjectRenamerResult
                {
                    Status = XmlRenameStatus.Success,
                    NewProjectCode = newProjectCode
                };
            }
            catch (Exception)
            {
                return new XmlProjectRenamerResult
                {
                    Status = XmlRenameStatus.Error,
                    NewProjectCode = null
                };
            }


        }

        // TODO: move in other service
        public static async Task<XmlProjectRenamerResult> RenameProjectFromFile(
            string projectCodeFilePath, string newProjectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var projectPath = Path.Combine(CatrobatContextBase.ProjectsPath,
                        newProjectName);

                    if (counter != 0)
                    {
                        projectPath = Path.Combine(CatrobatContextBase.ProjectsPath,
                            newProjectName + counter);
                    }

                    if (!await storage.DirectoryExistsAsync(projectPath))
                        break;

                    counter++;
                }

                var newProjectNameUnique = newProjectName;

                if (counter != 0)
                    newProjectNameUnique = newProjectName + counter;

                var projectCode = await storage.ReadTextFileAsync(projectCodeFilePath);
                var result = RenameProjectFromCode(projectCode, newProjectNameUnique);

                await storage.WriteTextFileAsync(
                    projectCodeFilePath, result.NewProjectCode);

                result.NewProjectName = newProjectNameUnique;

               return result;
            }
        }
    }



    // TODO: add all project renaming from CatrobatContext here and make this class a service
}
