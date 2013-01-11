using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Catrobat.Core.Storage
{
  public class ResourceLoader
  {
    private static IResources _resources;

    public static void SetResourceLoader(IResources resources)
    {
      _resources = resources;
    }

    public static Stream GetResourceStream(string uri)
    {
      uri = uri.Replace('/', '.');
      uri = uri.Replace('\\', '.');
      return Assembly.GetExecutingAssembly().GetManifestResourceStream("Catrobat.Core." + uri);

      //return _resources.OpenResourceStream(uri);
    }
  }
}
