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
          break;
        case Projects.IdeCommon:
          projectPath = "IDECommon/";
          break;
        case Projects.TestCommon:
          projectPath = "TestsWindowsPhone/";
          break;
        default:
          throw new ArgumentOutOfRangeException("project");
      }

      return File.Open(uri, FileMode.Open, FileAccess.Read);
    }
  }
}
