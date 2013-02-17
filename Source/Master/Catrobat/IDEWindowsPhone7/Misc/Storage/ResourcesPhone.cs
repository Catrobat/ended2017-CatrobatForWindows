using System.IO;
using System.Windows;
using Catrobat.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class ResourcesPhone : IResources
  {
    public Stream OpenResourceStream(ResourceScope project, string uri)
    {
      string projectPath = "";

      switch (project)
      {
        case ResourceScope.Core:
          projectPath = "/Catrobat.Core;component/";
          break;
        case ResourceScope.IdeCommon:
          projectPath = "/Catrobat.IDECommon;component/";
          break;
        case ResourceScope.IdePhone:
          projectPath = "";
          break;
        case ResourceScope.TestsPhone:
          projectPath = "";
          break;
        default:
          throw new ArgumentOutOfRangeException("project");
      }

      var resourceUri = new Uri(projectPath + uri, UriKind.Relative);

      var resource = Application.GetResourceStream(resourceUri);

      return resource != null ? resource.Stream : null;
    }
  }
}
