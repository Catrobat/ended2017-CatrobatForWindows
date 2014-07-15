using Catrobat.IDE.Core.UI.PortableUI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface IPictureService
    {
        void ChoosePictureFromLibraryAsync();

        void TakePictureAsync();

        Task DrawPictureAsync(PortableImage imageToEdit = null);

        void RecievedFiles(IEnumerable<object> files);

        IEnumerable<string> SupportedFileExtensions { get; }

        string ImageFileExtensionPrefix { get; }
    }
}
