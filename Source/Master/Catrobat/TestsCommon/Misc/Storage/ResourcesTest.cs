using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class ResourcesTest : IResources
  {
    public Stream OpenResourceStream(ResourceScope project, string uri)
    {
      string projectPath = "";

      string basePath = BasePathHelper.GetTestBasePathWithBranch();

      switch (project)
      {
        case ResourceScope.Core:
          projectPath = "Core/";
          break;

        case ResourceScope.IdeCommon:
          projectPath = "IDECommon/";
          break;

        case ResourceScope.TestCommon:
          projectPath = "TestsCommon/";
          break;

        default:
          throw new ArgumentOutOfRangeException("project");
      }

      return File.Open(basePath + projectPath + uri, FileMode.Open, FileAccess.Read);
    }
  }
}
