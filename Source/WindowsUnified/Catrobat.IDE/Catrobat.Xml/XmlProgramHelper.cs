using System;
using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities;

namespace Catrobat.IDE.Core.Xml
{
    public enum XmlRenameStatus { Success, Error }

    public class XmlProgramRenamerResult
    {
        public XmlRenameStatus Status { get; set; }

        public string NewProgramName { get; set; }

        public string NewProgramCode { get; set; }
    }

    public class XmlProgramHelper
    {
        public static XmlProgramRenamerResult RenameProgram(
            string programCode, string newProgramName)
        {
            try
            {
                var document = XDocument.Load(new StringReader(programCode));
                document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                var program = document.Element("program");
                var header = program.Element("header");
                var programName = header.Element("programName");
                programName.SetValue(newProgramName);

                var writer = new XmlStringWriter();
                document.Save(writer, SaveOptions.None);
                var newProgramCode = writer.ToString();

                return new XmlProgramRenamerResult
                {
                    Status = XmlRenameStatus.Success,
                    NewProgramCode = newProgramCode
                };
            }
            catch (Exception)
            {
                return new XmlProgramRenamerResult
                {
                    Status = XmlRenameStatus.Error,
                    NewProgramCode = null
                };
            }
        }

        public static string GetProgramVersion(string programCode)
        {
            var document = XDocument.Load(new StringReader(programCode));
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            var program = document.Element("program");
            var header = program.Element("header");
            var programName = header.Element("catrobatLanguageVersion");
            return programName.Value;
        }

        public static string GetProgramName(string programCode)
        {
            try
            {
                var document = XDocument.Load(new StringReader(programCode));
                document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                var program = document.Element("program");
                var header = program.Element("header");
                var programName = header.Element("programName");
                return programName.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
