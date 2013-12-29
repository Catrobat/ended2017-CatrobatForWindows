using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Phone.Controls.FormulaControls;

namespace Catrobat.IDE.Phone.Services
{
    public class ImageResizeServicePhone : IImageResizeService
    {
        public Task<PortableImage> ResizeImage(PortableImage image, int maxWidthHeight)
        {
            var bitmapSource = (BitmapSource)image.ImageSource;
            var bitmap = new WriteableBitmap(bitmapSource);
            //bitmap.FromByteArray(image.Data);

            var resizedImage = ResizeImage(bitmap, maxWidthHeight);
            var resizedPortableImage = new PortableImage(resizedImage.PixelWidth, resizedImage.PixelHeight);

            return Task.Run(() => resizedPortableImage);
        }


        private readonly Semaphore _semaphore = new Semaphore(0, 1);
        private PortableImage _resizedImage = null;
        public Task<PortableImage> ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                var bitmapSource = (BitmapSource)image.ImageSource;
                var bitmap = new WriteableBitmap(bitmapSource);

                var resizedImage = bitmap.Resize(newWidth, newHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
                var resizedPortableImage = new PortableImage(resizedImage)
                {
                    Width = resizedImage.PixelWidth,
                    Height = resizedImage.PixelHeight
                };

                _resizedImage = resizedPortableImage;

                _semaphore.Release();
            });

            _semaphore.WaitOne();

            return Task.Run(() => _resizedImage);
        }

        private static WriteableBitmap ResizeImage(WriteableBitmap image, int maxWidthHeight)
        {
            int maxWidth = maxWidthHeight;
            int maxHeight = maxWidthHeight;
            int width, height;

            if (image.PixelWidth > image.PixelHeight)
            {
                width = maxWidth;
                height = (int)((image.PixelHeight / (double)image.PixelWidth) * maxWidth);
            }
            else
            {
                height = maxHeight;
                width = (int)((image.PixelWidth / (double)image.PixelHeight) * maxHeight);
            }

            return image.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
        }
    }
}
