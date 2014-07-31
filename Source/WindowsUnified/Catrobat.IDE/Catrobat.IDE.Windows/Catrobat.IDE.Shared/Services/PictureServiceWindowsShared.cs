using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.WindowsShared.Content.Images.Misc;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.WindowsShared.Services
{

    public class PictureServiceWindowsShared : IPictureService
    {
        public IEnumerable<string> SupportedFileTypes
        {
            get
            {
                return new List<string>{ ".jpg", ".jpeg", ".png" };
            }
        }

        public string ImageFileExtensionPrefix
        {
            get { return "catrobat_ide_"; }
        }



        public void ChoosePictureFromLibraryAsync()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            foreach (var extension in SupportedFileTypes)
                openPicker.FileTypeFilter.Add(extension);

            StorageFile file;

            try
            {
                ServiceLocator.DispatcherService.RunOnMainThread(
                    openPicker.PickSingleFileAndContinue);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

        public void TakePictureAsync()
        {
            //CameraCaptureUI dialog = new CameraCaptureUI();
            //Size aspectRatio = new Size(16, 9);
            //dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            //StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);



            //var cam = new CameraCaptureUI();
            //var file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            //if (file != null)
            //{
            //    var fileStream = await file.OpenAsync(FileAccessMode.Read);
            //    var memoryStream = new MemoryStream();
            //    fileStream.AsStreamForRead().CopyTo(memoryStream);

            //    fileStream.Seek(0);
            //    var imagetobind = new BitmapImage();
            //    imagetobind.SetSource(fileStream);
            //    var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
            //    await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
            //    memoryStream.Seek(0, SeekOrigin.Begin);
            //    var portableImage = new PortableImage(writeableBitmap)
            //    {
            //        Width = writeableBitmap.PixelWidth,
            //        Height = writeableBitmap.PixelHeight,
            //        EncodedData = memoryStream
            //    };

            //    return new PictureServiceResult
            //    {
            //        Status = PictureServiceStatus.Success,
            //        Image = portableImage
            //    };
            //}
            //else
            //{
            //    return new PictureServiceResult
            //    {
            //        Status = PictureServiceStatus.Cancelled
            //    };
            //}
        }

        public async Task DrawPictureAsync(PortableImage imageToEdit = null)
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            if (imageToEdit != null)
            {
                await imageToEdit.WriteAsPng(StorageConstants.TempPaintImagePath);
            }
            else
            {
                throw new NotImplementedException("Create empty image here");
                // TODO: create empty image with the same dimensions as the devices width and heigt

                var imageWidth = ServiceLocator.SystemInformationService.ScreenWidth;
                var imageHeight = ServiceLocator.SystemInformationService.ScreenHeight;
            }

            var paintTempFolderPath = Path.GetDirectoryName(StorageConstants.TempPaintImagePath);
            var paintTempFolderName = Path.GetFileName(StorageConstants.TempPaintImagePath);
            var paintTempFolder = await localFolder.CreateFolderAsync(paintTempFolderPath, CreationCollisionOption.OpenIfExists);
            var file = await paintTempFolder.GetFileAsync(paintTempFolderName);
            

            var options = new Windows.System.LauncherOptions
            {
                DisplayApplicationPicker = true
            };


            try
            {
                // TODO: why does this line thow an exception???
                bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
                if (success)
                {
                    // File launch OK
                }
                else
                {
                    // File launch failed
                }
            }
            catch (Exception)
            {
                if(Debugger.IsAttached)
                    Debugger.Break();
            }
  




            //return new PictureServiceResult(); // TODO: get file from app launch
        }



        public async void RecievedFiles(IEnumerable<object> files)
        {
            var fileArray = files as object[] ?? files.ToArray();

            if (fileArray.Length == 0)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateTo<NewCostumeSourceSelectionViewModel>());
            }

            var file = (StorageFile)fileArray[0];
            var fileStream = await file.OpenReadAsync();
            var memoryStream = new MemoryStream();
            fileStream.AsStreamForRead().CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var imagetobind = new BitmapImage();
            await imagetobind.SetSourceAsync(memoryStream.AsRandomAccessStream());

            var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
            //var writeableBitmap = new WriteableBitmap(100, 100);
            await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
            memoryStream.Seek(0, SeekOrigin.Begin);
            var portableImage = new PortableImage(writeableBitmap)
            {
                Width = writeableBitmap.PixelWidth,
                Height = writeableBitmap.PixelHeight,
                EncodedData = memoryStream
            };

            if (portableImage != null)
            {
                var message = new GenericMessage<PortableImage>(portableImage);
                Messenger.Default.Send(message, ViewModelMessagingToken.CostumeImageListener);

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateTo<CostumeNameChooserViewModel>());
            }
            else
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Editor_MessageBoxWrongImageFormatHeader,
                    AppResources.Editor_MessageBoxWrongImageFormatText, delegate { /* no action */ }, MessageBoxOptions.Ok);
            }
        }
    }
}
