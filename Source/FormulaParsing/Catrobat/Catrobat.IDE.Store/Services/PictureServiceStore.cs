using System;
using System.Collections.Generic;
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
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            foreach (var extension in SupportedFileNames)
                openPicker.FileTypeFilter.Add(extension);

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(filestream);
                var writeableBuitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBuitmap.FromStream(filestream);
                var portableImage = new PortableImage(writeableBuitmap);

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
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(filestream);
                var writeableBuitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBuitmap.FromStream(filestream);
                var portableImage = new PortableImage(writeableBuitmap);

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
