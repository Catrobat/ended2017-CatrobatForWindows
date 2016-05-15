using Catrobat.IDE.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface IPictureService
    {
        IEnumerable<string> SupportedImageFileTypes { get; }

        string ImageFileExtensionPrefix { get; }

        void ChoosePictureFromLibraryAsync();

        void TakePictureAsync();

        Task DrawPictureAsync(Program program = null, Look lookToEdit = null);

        void RecievedFiles(IEnumerable<object> files);
    }
}
