using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public interface IProjectGenerator
    {
        Task<Project> GenerateProject(string projectName, bool writeToDisk);

        string GetProjectDefaultName();

        int GetOrderId();
    }
}
