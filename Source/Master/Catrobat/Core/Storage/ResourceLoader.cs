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
    private static IResourceLoaderFactory _resourceFactory;

    public static void SetResourceLoaderFactory(IResourceLoaderFactory resourceFactory)
    {
      _resourceFactory = resourceFactory;
    }

    public static IResources CreateResourceLoader()
    {
      return _resourceFactory.CreateResoucreLoader();
    }
  }
}
