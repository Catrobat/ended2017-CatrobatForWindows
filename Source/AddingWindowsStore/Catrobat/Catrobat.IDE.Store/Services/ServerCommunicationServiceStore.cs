using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Store.Services
{
    public class ServerCommunicationServicePhone : IServerCommunicationService
    {
        public void LoadOnlineProjects(bool append, string filterText, int offset, CatrobatWebCommunicationService.LoadOnlineProjectsEvent callback)
        {
            throw new NotImplementedException();
        }

        public void DownloadAndSaveProject(string downloadUrl, string projectName, CatrobatWebCommunicationService.DownloadAndSaveProjectEvent callback)
        {
            throw new NotImplementedException();
        }
    }
}