using Catrobat.IDE.Core.UI.PortableUI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface IPictureService
    {
        IEnumerable<string> SupportedFileTypes { get; }

        string ImageFileExtensionPrefix { get; }


        void ChoosePictureFromLibraryAsync();

        void TakePictureAsync();

        Task DrawPictureAsync(PortableImage imageToEdit = null);

        void RecievedFiles(IEnumerable<object> files);
    }
}
