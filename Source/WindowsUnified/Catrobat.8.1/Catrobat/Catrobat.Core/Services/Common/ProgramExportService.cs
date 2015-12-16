using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml.VersionConverter;
using System.Threading;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramExportService : IProgramExportService
    {
        private CancellationTokenSource _cancellationTokenSource;
        private MessageboxResult _uploadErrorCallbackResult;

        public async Task<Stream> CreateProgramPackageForExport(string programName)
        {
            var streamResult = new MemoryStream();

            var tempPath = Path.Combine(StorageConstants.TempProgramExportPath, programName);
            var programFolderPath = Path.Combine(StorageConstants.ProgramsPath, programName);
            var programCodePath = Path.Combine(tempPath, StorageConstants.ProgramCodePath);

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(tempPath);
                await storage.CopyDirectoryAsync(programFolderPath, tempPath);

                var programCode = await storage.ReadTextFileAsync(programCodePath);

                var converterResult = await CatrobatVersionConverter.
                ConvertToXmlVersion(programCode, XmlConstants.TargetOutputVersion);

                if (converterResult.Error != CatrobatVersionConverter.VersionConverterStatus.NoError)
                    return null;
            }

            var zipService = new ZipService();
            await zipService.ZipCatrobatPackage(streamResult, tempPath);
            streamResult.Seek(0, SeekOrigin.Begin);
            return streamResult;
        }

        public async Task ExportToPocketCodeOrgWithNotifications(string programName, string currentUserName, string currentToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            JSONStatusResponse statusResponse = await Task.Run(() => 
                ServiceLocator.WebCommunicationService.UploadProgramAsync(
                programName, currentUserName, currentToken, _cancellationTokenSource.Token,
                ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName), _cancellationTokenSource.Token);

            if (_cancellationTokenSource.Token.IsCancellationRequested == true)
            {
                ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Export_CanceledText,
                        AppResources.Export_CanceledText,
                        ToastDisplayDuration.Long);
                return;
            }
            switch (statusResponse.statusCode)
            {
                case StatusCodes.ServerResponseOk:
                    break;

                case StatusCodes.HTTPRequestFailed:
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramErrorCaption,
                            AppResources.Main_NoInternetConnection, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;

                default:
                    string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) :
                                           string.Format(AppResources.Main_UploadProgramError, statusResponse.answer);
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramErrorCaption,
                                messageString, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;
            }

            if (ServiceLocator.WebCommunicationService.NoUploadsPending())
            {
                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_NoUploadsPending, ToastDisplayDuration.Short);
            }
        }

        public async Task CancelExport()
        {
            if(_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();
        }

        public async Task CleanUpExport()
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync(StorageConstants.TempProgramExportPath);
                await storage.DeleteDirectoryAsync(StorageConstants.TempProgramExportZipPath);
            }
        }


        #region Callbacks
        private void UploadErrorCallback(MessageboxResult result)
        {
            _uploadErrorCallbackResult = result;
        }
        #endregion
    }
}
