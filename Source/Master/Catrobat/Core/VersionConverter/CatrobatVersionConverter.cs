using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Catrobat.Core.Misc;
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
                    {new CatrobatVersionPair("0.8", "Win0.80"), new CatrobatVersion08ToWin080()},
                    {new CatrobatVersionPair("0.91", "0.9"), new CatrobatVersion091To09()},
                    {new CatrobatVersionPair("0.9", "0.8"), new CatrobatVersion09To08()},
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
                        new CatrobatVersionPair("0.8", "Win0.80"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.8", "Win0.80",false)
                        }
                    },
                    {
                       new CatrobatVersionPair("Win0.80", "0.8"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("Win0.80", "0.8",true)
                        }
                    },


                    {
                        new CatrobatVersionPair("0.9", "Win0.80"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.9", "0.8",false),
                            new CatrobatVersionPair("0.8", "Win0.80",false)
                        }
                    },
                    {
                       new CatrobatVersionPair("Win0.80", "0.9"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("Win0.80", "0.8",true),
                            new CatrobatVersionPair("0.8", "0.9",true),
                        }
                    },


                  {
                        new CatrobatVersionPair("0.91", "Win0.80"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "0.9",false),
                            new CatrobatVersionPair("0.9", "0.8",false),
                            new CatrobatVersionPair("0.8", "Win0.80",false)
                        }
                    },
                    {
                       new CatrobatVersionPair("Win0.80", "0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("Win0.80", "0.8",true),
                            new CatrobatVersionPair("0.8", "0.9",true),
                            new CatrobatVersionPair("0.9", "0.91",true),
                        }
                    },
                    
                };
            }
        }

        public static VersionConverterError ConvertVersions(string inputVersion, string outputVersion, XDocument document)
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
                                var inversePath = new CatrobatVersionPair(pair.OutputVersion, pair.InputVersion);
                                Converters[inversePath].ConvertBack(document);
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

        public static string ConvertToXmlVersion(string projectCodePath, string targetVersion, out VersionConverterError error, bool overwriteProject = false)
        {
            var xml = "";

            if (!string.IsNullOrEmpty(projectCodePath))
            {
                var projectCode = "";

                using (var storage = StorageSystem.GetStorage())
                {
                    projectCode = storage.ReadTextFile(projectCodePath);
                }

                if (projectCode != null)
                {
                    var document = XDocument.Load(new StringReader(projectCode));
                    document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                    var inputVersion = GetInputVersion(document);

                    error = ConvertVersions(inputVersion, targetVersion, document);

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

        public static string ConvertToXmlVersionByProjectName(string projectName, string targetVersion, out VersionConverterError error, bool overwriteProject = false)
        {
            var projectCodePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ProjectCodePath);
            //string projectCode = null;

            //using (var storage = StorageSystem.GetStorage())
            //{
            //    projectCode = storage.ReadTextFile(projectCodePath);
            //}

            var xml = ConvertToXmlVersion(projectCodePath, targetVersion, out error, overwriteProject);
            return xml;
        }
    }
}
