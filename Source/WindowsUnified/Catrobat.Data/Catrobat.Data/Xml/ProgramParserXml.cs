using Catrobat.Data.ProgramParser;
using Catrobat.Data.Xml.XmlObjects;

namespace Catrobat.Data.Xml
{
    class ProgramParserXml : IProgramParser
    {
        public XmlProject ReadProgramFromString(string project)
        {
            var program = new XmlProject(project);
            return program;
        }

        public string WriteProgramToString(XmlProject project)
        {
            var projectString = project.ToString();
            return projectString;
        }
    }
}
