using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Misc.Storage
{
  public class ImageToBytesHelper
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
        var btmMap = new WriteableBitmap(image.PixelWidth, image.PixelHeight);

        btmMap.SaveJpeg(ms, image.PixelWidth, image.PixelHeight, 0, 100);

        return ms.ToArray();
      }
    }
  }
}
