using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class PictureServiceTest : IPictureService
    {

        public System.Collections.Generic.IEnumerable<string> SupportedFileTypes
        {
            get { throw new System.NotImplementedException(); }
        }

        public string ImageFileExtensionPrefix
        {
            get { throw new System.NotImplementedException(); }
        }

        public void ChoosePictureFromLibraryAsync()
        {
            throw new System.NotImplementedException();
        }

        public void TakePictureAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task DrawPictureAsync(PortableImage imageToEdit = null)
        {
            throw new System.NotImplementedException();
        }

        public void RecievedFiles(System.Collections.Generic.IEnumerable<object> files)
        {
            throw new System.NotImplementedException();
        }
    }
}
