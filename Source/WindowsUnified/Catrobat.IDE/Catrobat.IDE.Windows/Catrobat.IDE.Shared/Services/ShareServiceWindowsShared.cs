using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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
        private string _pathToShareFile; 

        public async Task ShareFile(string pathToShareFile) // TODO: this code should probably work, but no UI is showing, maybe bug in WP8.1 preview
        {
            _pathToShareFile = pathToShareFile;

            _pathToShareFile = _pathToShareFile.Replace("/", "\\");

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += ShareStorageItemsHandler;

            DataTransferManager.ShowShareUI();
        }

        private async void ShareStorageItemsHandler(DataTransferManager sender,
            DataRequestedEventArgs e)
        {
            var request = e.Request;
            request.Data.Properties.Title = "Share Catrobat file"; // TODO: localize
            request.Data.Properties.Description = ""; // TODO: localize
            var deferral = request.GetDeferral();

            try
            {
                var rootFolder = ApplicationData.Current.LocalFolder;
                var fileToShare = await rootFolder.GetFileAsync(_pathToShareFile);
                var storageItems = new List<IStorageItem> { fileToShare };
                request.Data.SetStorageItems(storageItems);
            }
            catch
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
