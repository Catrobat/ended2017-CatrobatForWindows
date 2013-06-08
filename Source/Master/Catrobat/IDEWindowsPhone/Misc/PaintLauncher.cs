using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Storage;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.IDEWindowsPhone.Misc
{
  public class PaintLauncher
  {
    private static string _imageFolderPath;
    private static string _imageFileName;
    private const string DummyFileExtension = ".catrobatimage";

    public async static Task<bool> LaunchPaint(string pathToImage)
    {
      try
      {
        var splitPath = pathToImage.Split('/');
        _imageFileName = splitPath[splitPath.Length - 1];
        _imageFolderPath = "";

        for(int i = 0; i < splitPath.Length - 1; i++)
        {
          if (_imageFolderPath != "")
            _imageFolderPath += "/";

          _imageFolderPath += splitPath[i];
        }

        using (IStorage storage = StorageSystem.GetStorage())
        {
          storage.DeleteDirectory(CatrobatContext.TempPaintImagePath);
          storage.CopyFile(pathToImage, CatrobatContext.TempPaintImagePath + "/" + _imageFileName + DummyFileExtension);
        }

        var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        var tempFolder = await localFolder.CreateFolderAsync(CatrobatContext.TempPaintImagePath.Split('/')[0], CreationCollisionOption.OpenIfExists);
        var tempImageFolder = await localFolder.CreateFolderAsync(CatrobatContext.TempPaintImagePath.Split('/')[1], CreationCollisionOption.ReplaceExisting);
        var imageFile = await tempImageFolder.CreateFileAsync(_imageFileName + DummyFileExtension, CreationCollisionOption.ReplaceExisting);
        await Windows.System.Launcher.LaunchFileAsync(imageFile);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public async static Task<bool> RecieveImageFromPaint()
    {
      try
      {
        var tempFilePath = CatrobatContext.TempPaintImagePath + "/" + _imageFileName + DummyFileExtension;
        var originalFilePath = _imageFolderPath + "/" + _imageFileName;

        using (IStorage storage = StorageSystem.GetStorage())
        {
          if (storage.FileExists(tempFilePath))
          {
            storage.FileExists(originalFilePath);
              storage.DeleteFile(originalFilePath);

            storage.CopyFile(tempFilePath, originalFilePath);
            storage.DeleteDirectory(CatrobatContext.TempPaintImagePath);
          }
        }

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
