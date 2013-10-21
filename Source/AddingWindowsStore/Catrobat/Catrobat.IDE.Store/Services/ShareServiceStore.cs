using System;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class ShareServicePhone : IShareService
    {
        private const string TempUploadFolderPath = "/TempUpload";
        private const string CatrobatFileExtension = ".catrobat";

        //private LiveConnectClient _client;

        public void ShareProjectWithMail(string projectName, string mailTo, string subject, string message)
        {
            //var emailcomposer = new EmailComposeTask();
            //emailcomposer.To = mailTo;
            //emailcomposer.Subject = subject;
            //emailcomposer.Body = message;
            //emailcomposer.Show();
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
