using System.IO;
using System.Threading.Tasks;
using Catrobat.Core.Storage;
using System;
using System.Reflection;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class ResourcesWindowsStore : IResources
  {
    public Stream OpenResourceStream(Projects project, string uri)
    {
      string fullUri = "ms-appx:///";

      switch (project)
      {
        case Projects.Core:
          fullUri += "Core";
          break;
        case Projects.IdePhone:
          fullUri += "IDEWindowsPhone";
          break;
        case Projects.IdeStore:
          fullUri += "IDEWindowsStroe";
          break;
        case Projects.TestsPhone:
          fullUri += "TestsWindowsPhone";
          break;
        case Projects.TestsStore:
          fullUri += "TestsWindowsStore";
          break;
        default:
          throw new ArgumentOutOfRangeException("project");
      }

      //fullUri += uri;

      //Task<Stream> = 

      //var file = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(fullUri, UriKind.Relative));
      //file.
      

      //var resourceUri = new Uri("Core/" + uri, UriKind.Relative);
      //return Application.GetResourceStream(resourceUri).Stream;

      //uri = uri.Replace('/', '.');
      //uri = uri.Replace('\\', '.');
      //return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceUri);
      return null;
    }
  }
}
