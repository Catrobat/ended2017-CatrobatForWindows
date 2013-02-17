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
    public Stream OpenResourceStream(Projects project, string uri)
    {
      string projectPath = "";

      switch (project)
      {
        case Projects.Core:
          projectPath = "/Catrobat.Core;component/";
          break;
        case Projects.IdeCommon:
          projectPath = "/Catrobat.IDECommon;component/";
          break;
        case Projects.IdePhone:
          projectPath = "";
          break;
        case Projects.TestsPhone:
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
