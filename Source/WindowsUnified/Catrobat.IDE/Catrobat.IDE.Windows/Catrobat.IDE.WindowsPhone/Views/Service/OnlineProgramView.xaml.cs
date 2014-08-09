using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.CatrobatObjects;
using Windows.UI.Xaml.Controls;

using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProgramView : ViewPageBase
    {
        private readonly OnlineProgramViewModel _viewModel =
            ServiceLocator.ViewModelLocator.OnlineProgramViewModel;

        private List<DownloadOperation> _activeDownloadOperationList;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _paused;

        public delegate void progressDelegate(DownloadOperation downloadOperation);

        public OnlineProgramView()
        {
            InitializeComponent();
            _activeDownloadOperationList = new List<DownloadOperation>();
            _cancellationTokenSource = new CancellationTokenSource();
            ProgressBarDownload.Maximum = 100;
            ProgressBarDownload.Minimum = 0;
            ProgressBarDownload.Value = 0;
            _paused = false;
        }

        private void ViewPageBase_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _viewModel.OnLoadCommand.Execute((OnlineProgramHeader)DataContext);
        }


        private void ManageDownloadProgress(DownloadOperation downloadOperation)
        {
            string downloadWhoseProgressChanged = downloadOperation.Guid.ToString();
            var downloadProgressStatus = downloadOperation.Progress.Status;

            int receivedPercentage = 100;
            double bytesReceived = downloadOperation.Progress.BytesReceived;
            double bytesLeftToReceive = downloadOperation.Progress.TotalBytesToReceive;
            if (bytesLeftToReceive > 0)
            {
                receivedPercentage = (int)((bytesReceived * 100) / bytesLeftToReceive);
            }
            ProgressBarDownload.Value = receivedPercentage;
        }

        private async void ManageDownloadsAsync(DownloadOperation downloadOperation, bool start)
        {
            // each download gets the same cancellation token so we cancel them all at
            try
            {
                _activeDownloadOperationList.Add(downloadOperation);
                Progress<DownloadOperation> downloadProgressCallback = new Progress<DownloadOperation>(ManageDownloadProgress);
                if (start)
                {
                    await downloadOperation.StartAsync().AsTask(_cancellationTokenSource.Token, downloadProgressCallback);
                }
                //else
                //{
                //    // downlaod was already running on app-start
                //    await downloadOperation.AttachAsync().AsTask(_cancellationTokenSource.Token, downloadProgressCallback);
                //}
                // TODO report that download with ID: downloadOperation.Guid has finished
                ButtonStartDownload.IsEnabled = true;
            }
            catch (TaskCanceledException)
            {
                // Download Canceled
            }
            catch (Exception ex)
            {
                // TODO add error Handling
            }
            finally
            {
                _activeDownloadOperationList.Remove(downloadOperation);
            }
        }

        private async void ButtonStartDownload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string downloadUrl = "download/881.catrobat";
            string projectName = "skypascal";
            try
            {
                Uri downloadSource = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS + downloadUrl);
                string downloadDestination = Path.Combine(StorageConstants.TempProgramImportZipPath, projectName + ApplicationResources.EXTENSION);

                StorageFile destinationFile = await GetFileAsync(downloadDestination);

                BackgroundDownloader backgroundDownloader = new BackgroundDownloader();
                DownloadOperation downloadOperation = backgroundDownloader.CreateDownload(downloadSource, destinationFile);
                ManageDownloadsAsync(downloadOperation, true);
            }
            catch (Exception ex) //TODO add error Handling
            {
                //Error
            }
            ButtonStartDownload.IsEnabled = false;
        }

        private void ButtonCancelDownload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _activeDownloadOperationList = new List<DownloadOperation>();
            _cancellationTokenSource = new CancellationTokenSource();
            ProgressBarDownload.Value = 0;
        }


        private void ButtonPauseDownload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!_paused)
            {
                foreach (DownloadOperation downloadOperation in _activeDownloadOperationList)
                {
                    if (downloadOperation.Progress.Status == BackgroundTransferStatus.Running)
                    {
                        downloadOperation.Pause();
                    }
                }
                ButtonPauseDownload.Content = "Cont.";
                _paused = true;
            }
            else
            {
                foreach (DownloadOperation downloadOperation in _activeDownloadOperationList)
                {
                    if (downloadOperation.Progress.Status == BackgroundTransferStatus.PausedByApplication)
                    {
                        downloadOperation.Resume();
                    }
                }
                ButtonPauseDownload.Content = "Pause";
            }
        }


        #region Helpers from StorageWindowsShared

        public async Task<StorageFolder> CreateFolderPathAsync(string path)
        {
            if (path == "")
                return ApplicationData.Current.LocalFolder;

            var subPath = Path.GetDirectoryName(path);

            var parentFolder = await CreateFolderPathAsync(subPath);

            if (parentFolder == null)
                return null;

            var folderName = Path.GetFileName(path);

            try
            {
                var folder = await parentFolder.GetFolderAsync(folderName);
                return folder;
            }
            catch { }


            var folders = await parentFolder.GetFoldersAsync();

            foreach (var folder in folders)
            {

            }

            await parentFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            var newFolder = await parentFolder.GetFolderAsync(folderName);
            return newFolder;

            //var subPath = Path.GetDirectoryName(path);

            //if (subPath == "")
            //    return;

            //await CreateFolderPath(subPath);

            //var folder = await GetFolderAsync(subPath);
            //var fileName = Path.GetFileName(path);
            //if (folder != null)
            //{
            //    folder.CreateFolderAsync(fileName);
            //}

            //path = subPath;
        }

        public async Task<StorageFolder> GetFolderAsync(string path)
        {
            //await CreateFolderPathAsync(path);

            if (path == "")
                return ApplicationData.Current.LocalFolder;

            var subPath = Path.GetDirectoryName(path);

            var parentFolder = await GetFolderAsync(subPath);

            if (parentFolder == null)
                return null;

            var folderName = Path.GetFileName(path);

            try
            {
                var folder = await parentFolder.GetFolderAsync(folderName);
                return folder;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StorageFile> GetFileAsync(string path, bool createIfNotExists = true)
        {
            var fileName = Path.GetFileName(path);
            var directoryName = Path.GetDirectoryName(path);

            if (createIfNotExists)
                await CreateFolderPathAsync(directoryName);

            var folder = await GetFolderAsync(directoryName);

            if (folder == null)
                return null;

            if (createIfNotExists)
                return await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            return await folder.GetFileAsync(fileName);
        }


        #endregion
    }
}
