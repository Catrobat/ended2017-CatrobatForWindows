using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class PictureServiceStore : IPictureService
    {
        private static readonly List<string> SupportedFileNames = new List<string>
        {
            ".jpg", ".jpeg", ".png"
        };

        public async Task<PictureServiceResult> ChoosePictureFromLibraryAsync()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            foreach (var extension in SupportedFileNames)
                openPicker.FileTypeFilter.Add(extension);

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var fileStream = await file.OpenAsync(FileAccessMode.Read);
                var memoryStream = new MemoryStream();
                fileStream.AsStreamForRead().CopyTo(memoryStream);

                fileStream.Seek(0);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(fileStream);
                var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
                memoryStream.Seek(0, SeekOrigin.Begin);
                var portableImage = new PortableImage(writeableBitmap)
                {
                    Width = writeableBitmap.PixelWidth,
                    Height = writeableBitmap.PixelHeight,
                    EncodedData = memoryStream
                };

                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Success,
                    Image = portableImage
                };
            }
            else
            {
                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Cancelled
                };
            }
        }

        public async Task<PictureServiceResult> TakePictureAsync()
        {
            var cam = new CameraCaptureUI();
            var file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                var fileStream = await file.OpenAsync(FileAccessMode.Read);
                var memoryStream = new MemoryStream();
                fileStream.AsStreamForRead().CopyTo(memoryStream);

                fileStream.Seek(0);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(fileStream);
                var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
                memoryStream.Seek(0, SeekOrigin.Begin);
                var portableImage = new PortableImage(writeableBitmap)
                {
                    Width = writeableBitmap.PixelWidth,
                    Height = writeableBitmap.PixelHeight,
                    EncodedData = memoryStream
                };

                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Success,
                    Image = portableImage
                };
            }
            else
            {
                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Cancelled
                };
            }
        }

        public Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            throw new NotImplementedException();
        }

    }
}
