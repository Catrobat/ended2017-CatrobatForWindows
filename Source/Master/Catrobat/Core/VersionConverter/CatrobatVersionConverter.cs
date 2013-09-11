using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.VersionConverter.Versions;

namespace Catrobat.Core.VersionConverter
{
    public static class CatrobatVersionConverter
    {
        public enum VersionConverterError { NoError, VersionNotSupported, ProjectCodeNotValid, ProjectCodePathNotValid }

        private static Dictionary<CatrobatVersionPair, CatrobatVersion> Converters
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, CatrobatVersion>(new CatrobatVersionPair.EqualityComparer())
                {
                    {new CatrobatVersionPair(CatrobatVersionConfig.TargetInputVersion, CatrobatVersionConfig.TargetOutputVersion), new CatrobatVersion08ToWin08()}
                };
            }
        }

        private static Dictionary<CatrobatVersionPair, List<CatrobatVersionPair>> ConvertersPaths
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, List<CatrobatVersionPair>>(new CatrobatVersionPair.EqualityComparer())
                {
                    {
                        new CatrobatVersionPair(CatrobatVersionConfig.TargetInputVersion, CatrobatVersionConfig.TargetOutputVersion),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair(CatrobatVersionConfig.TargetInputVersion, CatrobatVersionConfig.TargetOutputVersion,false)
                        }
                    },
                    {
                        new CatrobatVersionPair(CatrobatVersionConfig.TargetOutputVersion, CatrobatVersionConfig.TargetInputVersion),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair(CatrobatVersionConfig.TargetInputVersion,CatrobatVersionConfig.TargetOutputVersion, true)
                        }
                    }
                };
            }
        }

        public static VersionConverterError SetConverterDirections(string inputVersion, string outputVersion, XDocument document)
        {
            var error = VersionConverterError.NoError;
            var versionPair = new CatrobatVersionPair(inputVersion, outputVersion);

            if (ConvertersPaths.ContainsKey(versionPair))
            {
                var path = ConvertersPaths[versionPair];

                if (path.Count == 0)
                {
                    error = VersionConverterError.VersionNotSupported;
                }
                else
                {
                    try
                    {
                        foreach (var pair in path)
                        {
                            if (pair.IsInverse)
                            {
                                Converters[pair].ConvertBack(document);
                            }
                            else
                            {
                                Converters[pair].Convert(document);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        error = VersionConverterError.ProjectCodeNotValid;
                    }
                }
            }
            else
            {
                error = VersionConverterError.ProjectCodeNotValid;
            }

            return error;
        }

        private static string GetInputVersion(XDocument document)
        {
            var inputVersion = "";
            var program = document.Element("program");

            if (program != null && program.Element("header") != null)
            {
                var languageVersion = program.Element("header").Element("catrobatLanguageVersion");

                if (languageVersion != null)
                {
                    inputVersion = languageVersion.Value;
                }
            }

            return inputVersion;
        }

        public static string ConvertToInternalXmlVersion(string projectCodePath, out VersionConverterError error, bool overwriteProject = false)
        {
            var xml = "";

            if (!string.IsNullOrEmpty(projectCodePath))
            {
                var projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectCode = storage.ReadTextFile(Path.Combine(CatrobatContextBase.TempProjectImportPath, Project.ProjectCodePath));
                }

                if (projectCode != null)
                {
                    var document = XDocument.Load(new StringReader(projectCode));
                    document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                    var inputVersion = GetInputVersion(document);

                    error = SetConverterDirections(inputVersion, CatrobatVersionConfig.TargetOutputVersion, document);

                    if (error == VersionConverterError.NoError)
                    {
                        var writer = new XmlStringWriter();
                        document.Save(writer, SaveOptions.None);
                        xml = writer.GetStringBuilder().ToString();


                        if (overwriteProject)
                        {
                            using (var storage = StorageSystem.GetStorage())
                            {
                                try
                                {
                                    storage.WriteTextFile(projectCodePath, xml);
                                }
                                catch
                                {
                                    error = VersionConverterError.ProjectCodeNotValid;
                                }
                            }
                        }
                    }
                    else
                    {
                        error = VersionConverterError.VersionNotSupported;
                    }
                }
                else
                {
                    error = VersionConverterError.ProjectCodeNotValid;
                }
            }
            else
            { 
                error = VersionConverterError.ProjectCodePathNotValid;
            }

            return xml;
        }

        public static string ConvertToInternalXmlVersionByProjectName(string projectName, out VersionConverterError error, bool overwriteProject = false)
        {
            var projectCodePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ProjectCodePath);
            string projectCode = null;

            using (var storage = StorageSystem.GetStorage())
            {
                projectCode = storage.ReadTextFile(projectCodePath);
            }

            var xml = ConvertToInternalXmlVersion(projectCode, out error, overwriteProject);
            return xml;
        }
    }
}
