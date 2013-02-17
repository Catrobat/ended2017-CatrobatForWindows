using System.IO;
using System.Windows;
using Catrobat.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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
          {
            projectPath = "Catrobat.Core.";
            var path = projectPath + uri.Replace("/", ".");
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
          }
        case ResourceScope.IdeCommon:
          {
            projectPath = "Catrobat.IDECommon.";
            var path = projectPath + uri.Replace("/", ".");
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
          }
        case ResourceScope.IdePhone:
          {
            projectPath = "";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            return resource != null ? resource.Stream : null;
          }
        case ResourceScope.TestsPhone:
          {
            projectPath = "";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            return resource != null ? resource.Stream : null;
          }
        case ResourceScope.SampleProjects:
          {
            projectPath = "Content/";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            return resource != null ? resource.Stream : null;
          }
        default:
          throw new ArgumentOutOfRangeException("project");
      }


    }
  }
}
