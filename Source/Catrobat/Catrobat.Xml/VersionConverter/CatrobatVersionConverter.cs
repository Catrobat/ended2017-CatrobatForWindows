using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Xml.VersionConverter.Versions;

namespace Catrobat.IDE.Core.Xml.VersionConverter
{
    public static class CatrobatVersionConverter
    {
        public enum VersionConverterStatus
        {
            NoError, VersionTooNew, VersionTooOld, ProgramDamaged, ProgramPathNotValid
        }

        private static Dictionary<CatrobatVersionPair, CatrobatVersion> Converters
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, CatrobatVersion>(new CatrobatVersionPair.EqualityComparer())
                {
                    {new CatrobatVersionPair("0.91", "Win0.91"), new CatrobatVersion091ToWin091()},
                    {new CatrobatVersionPair("0.92", "0.91"), new CatrobatVersion092To091()},
                    {new CatrobatVersionPair("0.93", "0.92"), new CatrobatVersion093To092()}
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
                        new CatrobatVersionPair("0.91", "Win0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "Win0.91", false)
                        }
                    },
                    {
                       new CatrobatVersionPair("Win0.91", "0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("Win0.91", "0.91", true)
                        }
                    },


                    {
                        new CatrobatVersionPair("0.92", "Win0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", false),
                            new CatrobatVersionPair("0.91", "Win0.91", false),
                        }
                    },
                    {
                        new CatrobatVersionPair("Win0.91", "0.92"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "Win0.91", true),
                            new CatrobatVersionPair("0.92", "0.91", true),
                        }
                    },

                    {
                        new CatrobatVersionPair("0.93", "Win0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.93", "0.92", false),
                            new CatrobatVersionPair("0.92", "0.91", false),
                            new CatrobatVersionPair("0.91", "Win0.91", false),
                        }
                    },
                    {
                        new CatrobatVersionPair("Win0.91", "0.93"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "Win0.91", true),
                            new CatrobatVersionPair("0.92", "0.91", true),
                            new CatrobatVersionPair("0.93", "0.92", true),
                        }
                    },








                    {
                        new CatrobatVersionPair("0.92", "0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", false)
                        }
                    },
                    {
                       new CatrobatVersionPair("0.91", "0.92"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", true)
                        }
                    },

                    {
                        new CatrobatVersionPair("0.93", "0.92"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.93", "0.92", false)
                        }
                    },
                    {
                       new CatrobatVersionPair("0.92", "0.93"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.93", "0.92", true)
                        }
                    },
                };
            }
        }

        public static VersionConverterStatus ConvertVersions(string inputVersion, string outputVersion, XDocument document)
        {
            if (inputVersion == outputVersion)
            {
                return VersionConverterStatus.NoError;
            }


            var error = VersionConverterStatus.NoError;
            var versionPair = new CatrobatVersionPair(inputVersion, outputVersion);

            if (double.Parse(inputVersion, NumberStyles.Any) < XmlConstants.MinimumCodeVersion)
            {
                return VersionConverterStatus.VersionTooOld;
            }

            if (ConvertersPaths.ContainsKey(versionPair))
            {
                var path = ConvertersPaths[versionPair];

                if (path.Count == 0)
                {
                    error = VersionConverterStatus.VersionTooOld;
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
                        error = VersionConverterStatus.ProgramDamaged;
                    }
                }
            }
            else
            {
                error = VersionConverterStatus.ProgramDamaged;
            }

            return error;
        }

        private static string GetInputVersion(XDocument document)
        {
            var inputVersion = "";
            //var program = document.Element("program");
            var program = document.Element(XmlConstants.Program);

            //if (program != null && program.Element("header") != null)
            if (program != null && program.Element(XmlConstants.Header) != null)
            {
                //var languageVersion = program.Element("header").Element("catrobatLanguageVersion");
                var languageVersion = program.Element(XmlConstants.Header).Element(XmlConstants.CatrobatLanguageVersion);

                if (languageVersion != null)
                {
                    inputVersion = languageVersion.Value;
                }
            }

            return inputVersion;
        }

        public static async Task<VersionConverterResult> ConvertToXmlVersion(
            string projectCode, string targetVersion)
        {
            // TODO XML: move to IDE.Core
            VersionConverterStatus error;
            var xml = "";

            if (!string.IsNullOrEmpty(projectCode))
            {
                //string projectCode;

                //using (var storage = StorageSystem.GetStorage())
                //{
                //    projectCode = await storage.ReadTextFileAsync(projectCodePath);
                //}

                if (projectCode != null)
                {
                    var document = XDocument.Load(new StringReader(projectCode));
                    document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                    var inputVersion = GetInputVersion(document);

                    if (XmlConstants.SupportedXMLVersions.Contains(inputVersion))
                    {
                        return new VersionConverterResult
                        {
                            Error = VersionConverterStatus.NoError,
                            Xml = projectCode
                        };
                    }

                    if (double.Parse(inputVersion) < XmlConstants.MinimumCodeVersion)
                    {
                        return new VersionConverterResult
                        {
                            Error = VersionConverterStatus.VersionTooOld,
                            Xml = null
                        };
                    }

                    error = ConvertVersions(inputVersion, targetVersion, document);

                    if (error == VersionConverterStatus.NoError)
                    {
                        var writer = new XmlStringWriter();
                        document.Save(writer, SaveOptions.None);
                        xml = writer.GetStringBuilder().ToString();


                        //if (overwriteProject)
                        //{
                        //    using (var storage = StorageSystem.GetStorage())
                        //    {
                        //        try
                        //        {
                        //            await storage.WriteTextFileAsync(projectCodePath, xml);
                        //        }
                        //        catch
                        //        {
                        //            error = VersionConverterStatus.ProgramDamaged;
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        error = VersionConverterStatus.ProgramDamaged;
                    }
                }
                else
                {
                    error = VersionConverterStatus.ProgramDamaged;
                }
            }
            else
            {
                error = VersionConverterStatus.ProgramDamaged;
            }

            return new VersionConverterResult { Xml = xml, Error = error };
        }
    }
}
