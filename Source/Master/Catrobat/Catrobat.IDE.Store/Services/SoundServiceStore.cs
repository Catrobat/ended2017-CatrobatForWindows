using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.Effects;
using Windows.Storage;
using Windows.Storage.Pickers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Store.Services
{
    public class SoundServiceStore : ISoundService
    {
        private static readonly List<string> SupportedFileNames = new List<string>
        {
            ".mp3", ".wma"
        };

        public async Task<SoundServiceResult> CreateSoundFromRecorder(Core.CatrobatObjects.Sprite sprite)
        {
            //string imageFile = @"images\test.png";

           //var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile);

           // using (var storage = StorageSystem.GetStorage())
           // {
           //     storage.OpenFile("test.mp3", StorageFileMode.Create, StorageFileAccess.Write);

           // }

           // var folder = ApplicationData.Current.LocalFolder;
           // var file = await folder.CreateFileAsync("temp_test.mp3", CreationCollisionOption.OpenIfExists);



           //if (file != null)
           //{
           //   // Set the option to show the picker
           //   var options = new Windows.System.LauncherOptions
           //   {

           //       DisplayApplicationPicker = true
           //   };

           //   // Launch the retrieved file
           //   bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
           //   if (success)
           //   {
           //      // File launched
           //   }
           //   else
           //   {
           //      // File launch failed
           //   }
           //}
           //else
           //{
           //   // Could not find file
           //}

            //var mediaCaptureMgr = new MediaCapture();
            //await mediaCaptureMgr.InitializeAsync();


            //var devices = await DeviceInformation.FindAllAsync(DeviceClass.AudioRender);

            //if(devices.Count == 0)
            //    Debugger.Break();

            //var captureInitSettings = new MediaCaptureInitializationSettings
            //{
            //    AudioDeviceId = devices.ElementAt(0).Id,
            //    VideoDeviceId = "",
            //    StreamingCaptureMode = StreamingCaptureMode.Audio,
            //    PhotoCaptureSource = PhotoCaptureSource.VideoPreview
            //};

            //var folder = ApplicationData.Current.LocalFolder;
            //var file = await folder.CreateFileAsync("temp_test.mp3", CreationCollisionOption.OpenIfExists);

            //var mediaCapture = new Windows.Media.Capture.MediaCapture();

            throw new NotImplementedException();
        }

        public async Task<SoundServiceResult> CreateSoundFromMediaLibrary(Sprite sprite)
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
                var filestream = await file.OpenAsync(FileAccessMode.Read);

                return new SoundServiceResult
                {
                    Status = SoundServiceStatus.Success,
                    Result = filestream.AsStream()
                };
            }
            else
            {
                return new SoundServiceResult
                {
                    Status = SoundServiceStatus.Error,
                    Result = null
                };
            }
        }

        public void Finished(SoundServiceResult result)
        {
            throw new NotImplementedException("Not used in for Windows Store");
        }
    }
}
