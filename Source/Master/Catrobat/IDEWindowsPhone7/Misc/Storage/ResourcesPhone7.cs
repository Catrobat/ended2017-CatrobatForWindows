using System.IO;
using System.Windows;
using Catrobat.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class ResourcesPhone7 : IResources
  {
    public Stream OpenResourceStream(Projects project, string uri)
    {
      // TODO: switch project

      var resourceUri = new Uri("/Catrobat.Core;component/" + uri, UriKind.Relative);
      return Application.GetResourceStream(resourceUri).Stream;
    }
  }
}
