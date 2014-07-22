using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IProjectImporterService
    {
        Task<ProjectDummyHeader> ImportProject(object systemSpeciticObject);

        Task<string> AcceptTempProject();

        Task CancelImport();
    }
}
