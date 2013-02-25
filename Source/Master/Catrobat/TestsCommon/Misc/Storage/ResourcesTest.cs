using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.TestsCommon.Misc.Storage
{
  public class ResourcesTest : IResources
  {
    private readonly List<Stream> _openedStreams = new List<Stream>();

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
        case ResourceScope.Resources:
          projectPath = "IDEWindowsPhone/Content/Resources/";
          break;

        default:
          throw new ArgumentOutOfRangeException("project");
      }
      
      Stream stream = File.Open(basePath + projectPath + uri, FileMode.Open, FileAccess.Read);
      _openedStreams.Add(stream);
      return stream;
    }

    public void Dispose()
    {
      foreach (var stream in _openedStreams)
      {
        stream.Close();
        stream.Dispose();
      }
    }
  }
}
