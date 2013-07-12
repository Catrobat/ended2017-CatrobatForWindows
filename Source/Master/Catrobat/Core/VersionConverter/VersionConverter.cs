using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.VersionConverter.Versions;

namespace Catrobat.Core.VersionConverter
{
    public static class VersionConverter
    {
        private static Dictionary<CatrobatVersionPair, CatrobatVersion> Converters
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, CatrobatVersion>(new CatrobatVersionPair.EqualityComparer())
                {
                    {new CatrobatVersionPair {InputVersion = "1.0", OutputVersion = "Win1.0"}, new CatrobatVersion10()}
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
                        new CatrobatVersionPair {InputVersion = "1.0", OutputVersion = "Win1.0"},

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair {InputVersion = "1.0", OutputVersion = "Win1.0", IsInverse = false}
                        }
                    },
                    {
                        new CatrobatVersionPair {InputVersion = "Win1.0", OutputVersion = "1.0"},

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair {InputVersion = "1.0", OutputVersion = "Win1.0", IsInverse = true}
                        }
                    }
                };
            }
        }

        public static void Convert(string inputVersion, string outputVersion, XDocument document)
        {
            var path = ConvertersPaths[new CatrobatVersionPair {InputVersion = inputVersion, OutputVersion = outputVersion}];

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

        public static string Convert(string projectCode)
        {
            var document = XDocument.Load(new StringReader(projectCode));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            var inputVersion = document.Element("program").Element("catrobatLanguageVersion").Value;
            var outputVersion = VersionConfig.TargetOutputversion;

            Convert(inputVersion, outputVersion, document);

            var writer = new XmlStringWriter();
            document.Save(writer, SaveOptions.None);

            var xml = writer.GetStringBuilder().ToString();
            return xml;
        }
    }
}
