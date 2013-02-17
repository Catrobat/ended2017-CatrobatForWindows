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

    public static Stream GetResourceStream(ResourceScope project, string uri)
    {
      return _resources.OpenResourceStream(project, uri);
    }
  }
}
