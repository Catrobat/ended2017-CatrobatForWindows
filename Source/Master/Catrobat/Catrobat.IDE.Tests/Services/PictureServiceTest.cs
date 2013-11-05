using System;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Tests.Services
{
    public class PictureServiceTest : IPictureService
    {
        public PictureServiceStatus NextMethodAction { get; set; }

        public Task<PictureServiceResult> ChoosePictureFromLibraryAsync()
        {
            var result = new PictureServiceResult
            {
                Status = NextMethodAction
            };

            if (result.Status == PictureServiceStatus.Success)
                result.Image = new PortableImage();

            return Task.Run(() => result);
        }

        public Task<PictureServiceResult> TakePictureAsync()
        {
            var result = new PictureServiceResult
            {
                Status = NextMethodAction
            };

            if (result.Status == PictureServiceStatus.Success)
                result.Image = new PortableImage();

            return Task.Run(() => result);
        }

        public Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            var result = new PictureServiceResult
            {
                Status = NextMethodAction
            };

            if (result.Status == PictureServiceStatus.Success)
                result.Image = new PortableImage();

            return Task.Run(() => result);
        }
    }
}
