using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class StorageFactoryPhone : IStorageFactory
  {
    public IStorage CreateStorage()
    {
      var storage = new StoragePhone();

      switch(ResolutionHelper.CurrentResolution)
      {
        case Resolutions.WVGA:
          storage.SetImageMaxThumbnailWidthHeight(150);
          break;
        case Resolutions.WXGA:
          storage.SetImageMaxThumbnailWidthHeight(220);
          break;
        case Resolutions.HD720p:
          storage.SetImageMaxThumbnailWidthHeight(220);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return storage;
    }
  }
}
