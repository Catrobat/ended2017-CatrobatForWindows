using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class ResourcesTest : IResources
  {
    public Stream OpenResourceStream(Projects project, string uri)
    {
      string projectPath = "";

      string basePath = BasePathHelper.GetTestBasePathWithBranch();

      switch (project)
      {
        case Projects.Core:
          projectPath = "Core/";
          return File.Open(basePath + projectPath + uri, FileMode.Open, FileAccess.Read);
        case Projects.IdeCommon:
          projectPath = "IDECommon/";
          return File.Open(basePath + projectPath + uri, FileMode.Open, FileAccess.Read);
        case Projects.TestCommon:
          projectPath = "TestsWindowsPhone/";
          return File.Open(uri, FileMode.Open, FileAccess.Read);
        default:
          throw new ArgumentOutOfRangeException("project");
      }
    }
  }
}
