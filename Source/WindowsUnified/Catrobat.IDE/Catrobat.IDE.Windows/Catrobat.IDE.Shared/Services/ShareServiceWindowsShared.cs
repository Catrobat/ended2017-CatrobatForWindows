using System;
using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ShareServiceWindowsShared : IShareService
    {
        private const string TempUploadFolderPath = "TempShare";
        private const string CatrobatFileExtension = ".catrobat";

        private string _projectName;
        public void ShateProject(string projectName) // TODO: this code should probably work, but no UI is showing, maybe bug in WP8.1 preview
        {
            _projectName = projectName;

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += ShareStorageItemsHandler;

            ServiceLocator.DispatcherService.RunOnMainThread(
                DataTransferManager.ShowShareUI);
            
        }

        private async void ShareStorageItemsHandler(DataTransferManager sender,
            DataRequestedEventArgs e)
        {
            var zipService = new ZipService();

            var request = e.Request;
            request.Data.Properties.Title = "Share Catrobat file"; // TODO: localize
            request.Data.Properties.Description = ""; // TODO: localize
            var deferral = request.GetDeferral();

            try
            {
                var projectFolderPath = Path.Combine(StorageConstants.ProgramsPath, _projectName);
                string fileName = _projectName + CatrobatFileExtension;

                var rootFolder = ApplicationData.Current.TemporaryFolder;
                var tempShareFolder = await rootFolder.CreateFolderAsync(TempUploadFolderPath,
                    CreationCollisionOption.OpenIfExists);
                var shareTempFile = await tempShareFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);

                var stream = await shareTempFile.OpenAsync(FileAccessMode.ReadWrite);

                await zipService.ZipCatrobatPackage(stream.AsStreamForWrite(),
                    projectFolderPath);


                var shareTempFile2 = await tempShareFolder.GetFileAsync(fileName);

                var storageItems = new List<IStorageItem> { shareTempFile2 };
                request.Data.SetStorageItems(storageItems);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
