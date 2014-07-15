using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class SoundServiceWindowsShared : ISoundService
    {
        public List<string> SupportedFileTypes
        {
            get { return new List<string> { "mp3", "wma", "aac" }; }
        }


        public void CreateSoundFromRecorder(Sprite sprite)
        {
            // TODO: open external sound recorder
        }

        public void CreateSoundFromMediaLibrary(Sprite sprite)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };

            foreach (var extension in SupportedFileTypes)
                openPicker.FileTypeFilter.Add("." + extension);

            openPicker.PickSingleFileAndContinue();
        }


        public async void RecievedFiles(IEnumerable<object> files)
        {
            var fileArray = files as object[] ?? files.ToArray();

            if (fileArray.Length == 0)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateTo<NewSoundSourceSelectionViewModel>());
            }

            var file = (StorageFile)fileArray[0];
            var memoryStream = new MemoryStream();
            var inputStream = await file.OpenReadAsync();
            inputStream.AsStream().CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var message = new GenericMessage<Stream>(memoryStream);
            Messenger.Default.Send(message, ViewModelMessagingToken.SoundStreamListener);

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateTo<SoundNameChooserViewModel>());
        }
    }
}
