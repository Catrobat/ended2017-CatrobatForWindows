using System.IO;
using System.Threading.Tasks;
using Catrobat.Core.Storage;
using System;
using System.Reflection;
using Windows.Storage;

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
          fullUri += "Core/";
          break;
        case Projects.IdePhone:
          fullUri += "IDEWindowsPhone/";
          break;
        case Projects.IdeStore:
          fullUri += "IDEWindowsStroe/";
          break;
        case Projects.TestsPhone:
          fullUri += "TestsWindowsPhone/";
          break;
        case Projects.TestsStore:
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
      return stream.Result;
    }
  }
}
