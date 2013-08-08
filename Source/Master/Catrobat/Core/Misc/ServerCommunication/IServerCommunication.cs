namespace Catrobat.Core.Misc.ServerCommunication
{
    public interface IServerCommunication
    {
        void LoadOnlineProjects(bool append, string filterText, int offset, ServerCommunication.LoadOnlineProjectsEvent callback);

        void DownloadAndSaveProject(string downloadUrl, string projectName, ServerCommunication.DownloadAndSaveProjectEvent callback);
    }
}