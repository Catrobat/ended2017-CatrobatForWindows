using System.IO;
using Catrobat.Core.Storage;
using System;
using System.Reflection;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class ResourcesWindowsStore : IResources
  {
    public Stream OpenResourceStream(string uri)
    {
      //var resourceUri = new Uri("Core/" + uri, UriKind.Relative);
      //return Application.GetResourceStream(resourceUri).Stream;

      //uri = uri.Replace('/', '.');
      //uri = uri.Replace('\\', '.');
      //return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceUri);
      return null;
    }
  }
}
