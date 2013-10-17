using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface ITestProjectGenerator
    {
        Project GenerateProject(string language2LetterIsoCode);
    }
}
