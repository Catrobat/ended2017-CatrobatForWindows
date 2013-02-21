using ImageTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.IO;
using ImageTools.IO.Bmp;
using ImageTools.IO.Gif;
using ImageTools.IO.Png;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class ImageFormatHelper
  {
    public static BitmapImage ConvertByteToImage(byte[] image)
    {
      if (image == null)
        return null;

      using (var ms = new MemoryStream(image))
      {
        var bi = new BitmapImage
        {
          CreateOptions = BitmapCreateOptions.None
        };

        bi.SetSource(ms);

        return bi;
      }
    }

    public static byte[] ConvertImageToBytes(BitmapImage image)
    {
      using (var ms = new MemoryStream())
      {
        var btmMap = new WriteableBitmap(image);

        btmMap.SaveJpeg(ms, image.PixelWidth, image.PixelHeight, 0, 85);

        return ms.ToArray();
      }
    }

    public static WriteableBitmap ConvertByteToWriteableImage(byte[] image)
    {
      if (image == null)
        return null;

      using (var ms = new MemoryStream(image))
      {
        var bi = new BitmapImage
        {
          CreateOptions = BitmapCreateOptions.None
        };

        bi.SetSource(ms);
        var wb = new WriteableBitmap(bi);

        return wb;
      }
    }

    public static byte[] ConvertWriteableImageToBytes(WriteableBitmap image)
    {
      using (var ms = new MemoryStream())
      {
        image.SaveJpeg(ms, image.PixelWidth, image.PixelHeight, 0, 85);

        return ms.ToArray();
      }
    }
  }
}
