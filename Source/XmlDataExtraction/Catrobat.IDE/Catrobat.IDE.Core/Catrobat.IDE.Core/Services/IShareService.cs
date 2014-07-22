namespace Catrobat.IDE.Core.Services
{
    public interface IShareService
    {
        //void ShareProjectWithMail(string projectName, string mailTo, string subject, string message);


        //// ### SkyDrive #############################################################################

        //void UploadProjectToSkydrive(object liveConnectSessionChangedEventArgs, ProjectDummyHeader project,
        //    Action success, Action error);

        void ShateProject(string projectName);
    }
}
