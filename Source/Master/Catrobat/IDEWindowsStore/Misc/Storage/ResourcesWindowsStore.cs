using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Catrobat.Core.Misc.Storage;
using System;
using System.Reflection;
using Windows.Storage;

namespace Catrobat.IDEWindowsStore.Misc.Storage
{
  public class ResourcesWindowsStore : IResourceLoader
  {
    private readonly List<Stream> _openedStreams = new List<Stream>();

    public Stream OpenResourceStream(ResourceScope project, string uri)
    {
      string fullUri = "ms-appx:///";

      switch (project)
      {
        case ResourceScope.Core:
          fullUri += "Core/";
          break;
        case ResourceScope.IdePhone:
          fullUri += "IDEWindowsPhone/";
          break;
        case ResourceScope.IdeStore:
          fullUri += "IDEWindowsStroe/";
          break;
        case ResourceScope.TestsPhone:
          fullUri += "TestsWindowsPhone/";
          break;
        case ResourceScope.TestsStore:
          fullUri += "TestsWindowsStore/";
          break;
        default:
          throw new ArgumentOutOfRangeException("project");
      }

      fullUri += uri;

      // TODO: Find a way to get resources from assembly within Unittest Project
      Func<Task<Stream>> sync = async delegate() 
      {
        var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(fullUri));
        return (await file.OpenAsync(FileAccessMode.Read)).AsStream();
      };

      var stream = sync.Invoke();
      stream.Wait();

      _openedStreams.Add(stream.Result);
      return stream.Result;
    }

    public void Dispose()
    {
      foreach (var stream in _openedStreams)
        stream.Dispose();
    }
  }
}
