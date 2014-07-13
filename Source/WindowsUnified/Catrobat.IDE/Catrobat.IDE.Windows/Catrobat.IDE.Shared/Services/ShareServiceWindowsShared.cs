using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage.Streams;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ShareServiceWindowsShared : IShareService
    {
        private const string TempUploadFolderPath = "/TempUpload";
        private const string CatrobatFileExtension = ".catrobat";

        //private LiveConnectClient _client;

        public async void ShareProjectWithMail(string projectName, string mailTo, string subject, string message)
        {
            var mailto = new Uri("mailto:?to=recipient@example.com&subject=The subject of an email&body=Hello from a Windows 8 Metro app.");
            await Windows.System.Launcher.LaunchUriAsync(mailto);

            //var emailcomposer = new EmailComposeTask();
            //emailcomposer.To = mailTo;
            //emailcomposer.Subject = subject;
            //emailcomposer.Body = message;
            //emailcomposer.Show();
        }


        private string _shareProgramName;
        private void ShareProgram(string programName)
        {
            _shareProgramName = programName;

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareHtmlHandler);
        }

        private void ShareHtmlHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = _shareProgramName;
            request.Data.Properties.Description =
                "Demonstrates how to share an HTML fragment with a local image.";

            string localImage = "ms-appx:///Assets/Logo.png";
            string htmlExample = "<p>Here is a local image: <img src=\"" + localImage + "\">.</p>";
            string htmlFormat = HtmlFormatHelper.CreateHtmlFormat(htmlExample);
            request.Data.SetHtmlFormat(htmlFormat);

            // Because the HTML contains a local image, we need to add it to the ResourceMap.
            RandomAccessStreamReference streamRef =
                 RandomAccessStreamReference.CreateFromUri(new Uri(localImage));
            request.Data.ResourceMap[localImage] = streamRef;
        }

        public async void UploadProjectToSkydrive(object client, ProjectDummyHeader project,
            Action success, Action error)
        {
            //try
            //{
            //    using (var storage = StorageSystem.GetStorage())
            //    {
            //        if (storage.FileExists(TempUploadFolderPath))
            //            storage.DeleteFile(TempUploadFolderPath);

            //        storage.CreateDirectory(TempUploadFolderPath);

            //        var projectPath = Path.Combine(CatrobatContextBase.ProjectsPath, project.ProjectName);

            //        string fileName = project.ProjectName + CatrobatFileExtension;
            //        var stream = new MemoryStream();


            //            CatrobatZipService.ZipCatrobatPackage(stream, projectPath);

            //            stream.Seek(0, SeekOrigin.Begin);


            //            await ((LiveConnectClient)client).Upload("me/skydrive", fileName, stream, OverwriteOption.Rename);

            //            success();
                    
            //    }
            //}
            //catch (Exception)
            //{
            //    error();
            //}
        }
    }
}
