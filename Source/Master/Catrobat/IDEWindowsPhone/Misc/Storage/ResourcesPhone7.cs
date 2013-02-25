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
    private readonly List<Stream> _openedStreams = new List<Stream>();

    public Stream OpenResourceStream(ResourceScope project, string uri)
    {
      string projectPath = "";

      switch (project)
      {
        case ResourceScope.Core:
          {
            projectPath = "Catrobat.Core.";
            var path = projectPath + uri.Replace("/", ".");
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            _openedStreams.Add(stream);
            return stream;
          }
        case ResourceScope.IdeCommon:
          {
            projectPath = "Catrobat.IDECommon.";
            var path = projectPath + uri.Replace("/", ".");
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            _openedStreams.Add(stream);
            return stream;
          }
        case ResourceScope.IdePhone:
          {
            projectPath = "";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            if (resource != null)
            {
              var stream = resource.Stream;
              _openedStreams.Add(stream);
              return stream;
            }
            else
              return null;
          }
        case ResourceScope.TestsPhone:
          {
            projectPath = "";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            if (resource != null)
            {
              var stream = resource.Stream;
              _openedStreams.Add(stream);
              return stream;
            }
            else
              return null;
          }
        case ResourceScope.Resources:
          {
            projectPath = "Content/Resources/";
            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            var resource = Application.GetResourceStream(resourceUri);

            if (resource != null)
            {
              var stream = resource.Stream;
              _openedStreams.Add(stream);
              return stream;
            }
            else
              return null;

          }
        default:
          throw new ArgumentOutOfRangeException("project");
      }
    }

    public void Dispose()
    {
      foreach (var stream in _openedStreams)
        stream.Dispose();
    }
  }
}
