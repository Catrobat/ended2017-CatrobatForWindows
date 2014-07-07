using System;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public enum PictureServiceStatus
    {
        Success,
        Cancelled,
        Error
    }

    public class PictureServiceResult
    {
        public PictureServiceStatus Status { get; set; }
        public PortableImage Image { get; set; }
    }

    public interface IPictureService
    {
        Task<PictureServiceResult> ChoosePictureFromLibraryAsync();

        Task<PictureServiceResult> TakePictureAsync();

        Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null);
    }
}
