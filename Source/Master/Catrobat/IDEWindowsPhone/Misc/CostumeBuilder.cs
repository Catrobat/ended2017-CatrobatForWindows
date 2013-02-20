using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using ImageTools.IO;
using ImageTools.IO.Png;
//using ImageTools.IO.Jpeg;
using ImageTools.IO.Gif;
using ImageTools.IO.Bmp;
using ImageTools;
using Catrobat.Core.Storage;
using System.Windows.Threading;

namespace Catrobat.IDEWindowsPhone.Misc
{
  #region Delegates
  public delegate void LoadCostumeSuccess();
  public delegate void LoadCostumeFailed();

  public delegate void SaveCostumeSuccess(Costume costume);
  public delegate void SaveCostumeFailed();
  #endregion



  public class CostumeBuilder
  {
    private enum ImageEncoding { Jpg, Other }

    #region Callbacks
    public LoadCostumeSuccess LoadCostumeSuccess;
    public LoadCostumeFailed LoadCostumeFailed;
    #endregion

    private ExtendedImage _image;
    private Sprite _costumeSprite;
    private bool _loadedSuccess = false;
    private MemoryStream _imageMemoryStream = null;
    private BitmapImage _bitmapImage = null;
    private ImageEncoding _encoding = ImageEncoding.Other;

    public void StartCreateCostumeAsync(Sprite sprite, Stream imageStream)
    {
      var buffer = new byte[imageStream.Length];
      imageStream.Read(buffer, 0, (int)imageStream.Length);
      _imageMemoryStream = new MemoryStream(buffer);
      //_imageStream.Write(buffer, 0, buffer.Length);


      _loadedSuccess = false;
      _costumeSprite = sprite;

      if (Decoders.GetAvailableDecoders().Count <= 0)
      {
        //Decoders.AddDecoder<JpegDecoder>(); // TODO: handle jpeg with WP api
        Decoders.AddDecoder<GifDecoder>();
        Decoders.AddDecoder<BmpDecoder>();
        Decoders.AddDecoder<PngDecoder>();
      }

      _image = new ExtendedImage();
      _image.LoadingFailed -= image_LoadingFailed;
      _image.LoadingFailed += image_LoadingFailed;
      _image.LoadingCompleted -= image_LoadingCompleted;
      _image.LoadingCompleted += image_LoadingCompleted;
      _image.SetSource(imageStream);
    }

    private void image_LoadingFailed(object sender, UnhandledExceptionEventArgs e)
    {
      try
      {
        TryLoadingJpg();
        _loadedSuccess = true;
        if (LoadCostumeSuccess != null)
          LoadCostumeSuccess.Invoke();
      }
      catch (Exception)
      {
        _loadedSuccess = false;
        if (LoadCostumeFailed != null)
          LoadCostumeFailed.Invoke();
      }
    }

    private void TryLoadingJpg()
    {
      if (_imageMemoryStream == null)
        throw new Exception("Image stream is null.");

      _encoding = ImageEncoding.Jpg;

      Deployment.Current.Dispatcher.BeginInvoke(() =>
      {
        _bitmapImage = new BitmapImage();
        _bitmapImage.SetSource(_imageMemoryStream);
      });
    }


    private void image_LoadingCompleted(object sender, EventArgs e)
    {
      _loadedSuccess = true;
      if (LoadCostumeSuccess != null)
        LoadCostumeSuccess.Invoke();
    }

    public Costume Save(string name)
    {
      if (!_loadedSuccess)
        return null;

      var costume = new Costume(name, _costumeSprite);
      string absoluteFileName = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath + "/" + costume.FileName;

      if (_encoding == ImageEncoding.Jpg)
      {
        using (var storage = StorageSystem.GetStorage())
        {
          var fileStream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write);
          var wb = new WriteableBitmap(_bitmapImage);
          wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
          fileStream.Close();
        }
      }
      else
      {
        var encoder = new PngEncoder();

        using (var storage = StorageSystem.GetStorage())
        {
          using (var stream = storage.OpenFile(absoluteFileName, StorageFileMode.OpenOrCreate, StorageFileAccess.Write))
          {
            encoder.Encode(_image, stream);
          }
        }
      }


      return costume;
    }
  }
}
