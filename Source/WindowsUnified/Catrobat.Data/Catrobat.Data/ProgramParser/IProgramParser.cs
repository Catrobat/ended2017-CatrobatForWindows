using Catrobat.Data.Xml.XmlObjects;

namespace Catrobat.Data.ProgramParser
{
    public interface IProgramParser
    {
        XmlProject ReadProgramFromString(string project);

        string WriteProgramToString(XmlProject project);
    }
}
