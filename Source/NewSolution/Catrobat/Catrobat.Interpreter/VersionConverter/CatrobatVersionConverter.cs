using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Interpreter.Objects;
using Catrobat.Interpreter.Storage;
using Catrobat.Interpreter.VersionConverter.Versions;

namespace Catrobat.Interpreter.VersionConverter
{
    public static class CatrobatVersionConverter
    {
        public enum VersionConverterError { NoError, VersionNotSupported, ProjectCodeNotValid }

        private static Dictionary<CatrobatVersionPair, CatrobatVersion> Converters
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, CatrobatVersion>(new CatrobatVersionPair.EqualityComparer())
                {
                    {new CatrobatVersionPair {InputVersion = "0.8", OutputVersion = "Win0.8"}, new CatrobatVersion08ToWin08()}
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
                        new CatrobatVersionPair {InputVersion = "0.8", OutputVersion = "Win0.8"},

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair {InputVersion = "0.8", OutputVersion = "Win0.8", IsInverse = false}
                        }
                    },
                    {
                        new CatrobatVersionPair {InputVersion = "Win0.8", OutputVersion = "0.8"},

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair {InputVersion = "0.8", OutputVersion = "Win0.8", IsInverse = true}
                        }
                    }
                };
            }
        }

        public static VersionConverterError Convert(string inputVersion, string outputVersion, XDocument document)
        {
            var error = VersionConverterError.NoError;

            var path = ConvertersPaths[new CatrobatVersionPair {InputVersion = inputVersion, OutputVersion = outputVersion}];

            if(path.Count == 0)
                return VersionConverterError.VersionNotSupported;

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
                return VersionConverterError.ProjectCodeNotValid;
            }


            return error;
        }

        public static string Convert(string projectCode)
        {
            var document = XDocument.Load(new StringReader(projectCode));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            var inputVersion = document.Element("program").Element("catrobatLanguageVersion").Value;
            var outputVersion = CatrobatVersionConfig.TargetOutputVersion;

            Convert(inputVersion, outputVersion, document);

            var writer = new XmlStringWriter();
            document.Save(writer, SaveOptions.None);

            var xml = writer.GetStringBuilder().ToString();
            return xml;
        }

        public static VersionConverterError ConvertByProjectName(string projectName)
        {
            try
            {
                var projectCodePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ProjectCodePath);
                string projectCode = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    projectCode = storage.ReadTextFile(projectCodePath);
                }

                if (projectCode != null)
                {
                    var xDocument = XDocument.Load(new StringReader(projectCode));
                    xDocument.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                    var inputVersion = xDocument.Element("program").Element("header").Element("catrobatLanguageVersion").Value;
                    var outputVersion = CatrobatVersionConfig.TargetInputVersion;

                    
                    var error = Convert(inputVersion, outputVersion, xDocument);

                    if (error != VersionConverterError.NoError)
                        return error;

                    using (var storage = StorageSystem.GetStorage())
                    {
                        try
                        {
                            var writer = new XmlStringWriter();
                            xDocument.Save(writer, SaveOptions.None);

                            var xml = writer.GetStringBuilder().ToString();
                            storage.WriteTextFile(projectCodePath, xml);
                        }
                        catch
                        {
                            throw new Exception("Cannot write Project");
                        }
                    }
                }

                return VersionConverterError.NoError;
            }
            catch (Exception)
            {
                return VersionConverterError.ProjectCodeNotValid;
            }
        }
    }
}
