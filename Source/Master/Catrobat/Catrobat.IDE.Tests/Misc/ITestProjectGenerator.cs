using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Tests.Misc
{
    public interface ITestProjectGenerator
    {
        Project GenerateProject();
    }
}
