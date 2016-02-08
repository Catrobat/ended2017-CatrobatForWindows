using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramGenerator
    {
        Task<Program> GenerateProgram(string projectName, bool writeToDisk);

        string GetProgramDefaultName();

        int GetOrderId();
    }
}
