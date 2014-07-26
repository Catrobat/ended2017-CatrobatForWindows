using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramGenerator
    {
        Task<Program> GenerateProject(string projectName, bool writeToDisk);

        string GetProjectDefaultName();

        int GetOrderId();
    }
}
