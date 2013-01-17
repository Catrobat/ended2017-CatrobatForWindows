using System.IO;
using Catrobat.Core.Storage;
using System;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class ResourcesWindowsStore : IResources
  {
    public Stream OpenResourceStream(string uri)
    {
      throw new NotImplementedException();
      //var resourceUri = new Uri("/Catrobat.Core;component/" + uri, UriKind.Relative);
      //return Application.GetResourceStream(resourceUri).Stream;
    }
  }
}
