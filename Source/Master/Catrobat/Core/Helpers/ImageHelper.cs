namespace Catrobat.Core.Helpers
{
    public static class ImageHelper
    {
        public static byte[] CreateThumbnailImage(byte[] bi, int width)
        {
            //#if SILVERLIGHT
            //  if (bi == null)
            //    return null;

            //  double cx = width;
            //  double cy = bi.PixelHeight * (cx / bi.PixelWidth);

            //  Image image = new Image();
            //  image.Source = bi;

            //  byte[] wb1 = new WriteableBitmap((int)cx, (int)cy);
            //  ScaleTransform transform = new ScaleTransform();
            //  transform.ScaleX = cx / bi.PixelWidth;
            //  transform.ScaleY = cy / bi.PixelHeight;
            //  wb1.Render(image, transform);
            //  wb1.Invalidate();

            //  byte[] wb2 = new WriteableBitmap((int)cx, (int)cy);
            //  for (int i = 0; i < wb2.Pixels.Length; i++)
            //    wb2.Pixels[i] = wb1.Pixels[i];
            //  wb2.Invalidate();

            //  return wb2;
            //#else
            // TODO: implement me
            return new byte[0];
            //#endif
        }
    }
}