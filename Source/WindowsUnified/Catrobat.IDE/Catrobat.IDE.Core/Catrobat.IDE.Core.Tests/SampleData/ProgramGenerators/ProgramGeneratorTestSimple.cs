using System;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Tests.Misc;

namespace Catrobat.IDE.Core.Services.Common
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
