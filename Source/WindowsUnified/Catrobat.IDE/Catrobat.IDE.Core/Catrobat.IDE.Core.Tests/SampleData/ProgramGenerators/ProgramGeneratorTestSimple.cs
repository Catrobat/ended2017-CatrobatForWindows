using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators
{
    public class ProgramGeneratorTestSimple : ITestProgramGenerator
    {
        public Program GenerateProgram()
        {
            var project = new Program
            {
                Name = "Test Simple",
                UploadHeader = new UploadHeader
                {
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    Url = "http://pocketcode.org/details/871"
                }
            };

            FillSprites(project);

            return project;
        }
 
        private static void FillSprites(Program project)
        {
            var object1 = new Sprite { Name = "Object1" }; 
           
            project.Sprites.Add(object1);
        }
    }
}
