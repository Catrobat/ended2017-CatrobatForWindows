using System.IO;
using System.Windows;
using Catrobat.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catrobat.IDEWindowsPhone7.Misc.Storage
{
  public class ResourcesPhone7 : IResources
  {
    public Stream OpenResourceStream(string uri)
    {
      var resourceUri = new Uri("/Core;component/" + uri, UriKind.Relative);
      return Application.GetResourceStream(resourceUri).Stream;
    }
  }
}
