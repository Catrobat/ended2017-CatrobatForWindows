using System;
using System.IO;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.Core
{
  #region Delegates
  public delegate void LoadCostumeSuccess();
  public delegate void LoadCostumeFailed();

  public delegate void SaveCostumeSuccess(Costume costume);
  public delegate void SaveCostumeFailed();
  #endregion

  public class CostumeBuilder
  {
    #region Callbacks
    public LoadCostumeSuccess LoadCostumeSuccess;
    public LoadCostumeFailed LoadCostumeFailed;
    #endregion

    //private ExtendedImage image;
    //private Sprite costumeSprite;
    //private bool loadedSuccess = false;

    public void StartCreateCostumeAsync(Sprite sprite, Stream imageStream)
    {
      //loadedSuccess = false;
      //costumeSprite = sprite;

      //if (ImageTools.IO.Decoders.GetAvailableDecoders().Count <= 0)
      //{
      //  ImageTools.IO.Decoders.AddDecoder<JpegDecoder>();
      //  ImageTools.IO.Decoders.AddDecoder<GifDecoder>();
      //  ImageTools.IO.Decoders.AddDecoder<BmpDecoder>();
      //  ImageTools.IO.Decoders.AddDecoder<PngDecoder>();
      //}

      //image = new ExtendedImage();
      //image.LoadingFailed -= image_LoadingFailed;
      //image.LoadingFailed += image_LoadingFailed;
      //image.LoadingCompleted -= image_LoadingCompleted;
      //image.LoadingCompleted += image_LoadingCompleted;
      //image.SetSource(imageStream);

      throw new NotImplementedException();
    }

    //private void image_LoadingFailed(object sender, UnhandledExceptionEventArgs e)
    //{
    //  //loadedSuccess = false;
    //  //if (LoadCostumeFailed != null)
    //  //  LoadCostumeFailed.Invoke();

    //  throw new NotImplementedException();
    //}

    private void image_LoadingCompleted(object sender, EventArgs e)
    {
      //loadedSuccess = true;
      //if (LoadCostumeSuccess != null)
      //  LoadCostumeSuccess.Invoke();

      throw new NotImplementedException();
    }

    public Costume Save(string name)
    {
      //if (!loadedSuccess)
      //  return null;

      //Costume costume = new Costume(name, costumeSprite);

      //string absoluteFileName = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath + "/" + costume.FileName;

      //var encoder = new PngEncoder();

      //using (IStorage storage = StorageSystem.GetStorage())
      //{
      //  using (var stream = storage.OpenFile(absoluteFileName, StorageFileMode.OpenOrCreate, StorageFileAccess.Write))
      //  {
      //    encoder.Encode(image, stream);
      //  }
      //}

      //return costume;


      throw new NotImplementedException();
    }
  }
}
