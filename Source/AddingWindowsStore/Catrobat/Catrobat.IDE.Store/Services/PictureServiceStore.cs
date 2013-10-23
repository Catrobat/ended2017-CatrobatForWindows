using System;
using System.Collections.Generic;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class PictureServicePhone : IPictureService
    {
        private static List<string> _supportedFileNames = new List<string>
        {
            ".jpg", ".jpeg", ".png"
        };

        private Action<PortableImage> _successCallback;
        private Action _errorCallback;
        private Action _cancelleCallback;

        public async void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            
            foreach(var extension in _supportedFileNames)
                openPicker.FileTypeFilter.Add(extension);
            
            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(filestream);
                var writeableBuitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBuitmap.FromStream(filestream);
                var portableImage = new PortableImage(writeableBuitmap.ToByteArray(), 
                    writeableBuitmap.PixelWidth, writeableBuitmap.PixelHeight);

                success(portableImage);
            }
            else
            {
                cancelled();
            }
        }

        public async void TakePicture(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var cam = new CameraCaptureUI();
            var file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(filestream);
                var writeableBuitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBuitmap.FromStream(filestream);
                var portableImage = new PortableImage(writeableBuitmap.ToByteArray(),
                    writeableBuitmap.PixelWidth, writeableBuitmap.PixelHeight);

                success(portableImage);
            }
            else
            {
                cancelled();
            }
        }

        public void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            throw new NotImplementedException();
        }
    }
}
