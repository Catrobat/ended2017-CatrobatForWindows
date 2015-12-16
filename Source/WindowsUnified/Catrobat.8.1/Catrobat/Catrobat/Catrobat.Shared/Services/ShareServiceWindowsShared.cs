using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Catrobat.IDE.Core.Services;
using Catrobat.Core.Resources.Localization;
using Catrobat.Core.Resources;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ShareServiceWindowsShared : IShareService
    {
        private string _pathToShareFile; 

        public async Task ShareFile(string pathToShareFile)
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
            request.Data.Properties.Title = AppResources.Export_FileTitle;
            request.Data.Properties.Description = ApplicationResources.CATROBAT_URL;
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
