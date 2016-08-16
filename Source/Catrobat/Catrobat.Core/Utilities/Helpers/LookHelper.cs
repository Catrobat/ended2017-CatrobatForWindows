using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using System.Diagnostics;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class LookHelper
    {
        public static async Task<Look> Save(PortableImage image, string name, ImageDimension dimension, string projectPath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var imagePath = Path.Combine(projectPath, StorageConstants.ProgramLooksPath);
                if (!await storage.DirectoryExistsAsync(imagePath))
                    await storage.CreateDirectoryAsync(imagePath);
            }

            var resizedImage = await ServiceLocator.ImageResizeService.ResizeImage(image, dimension.Width, dimension.Height);
            var look = new Look(name);
            var absoluteFileName = Path.Combine(projectPath, StorageConstants.ProgramLooksPath, look.FileName);

            await resizedImage.WriteAsPng(absoluteFileName);

            //look.Image = resizedImage;

            return look;
        }

        public static async Task ReplaceImageInStorage(Program project, Look look, PortableImage newImage)
        {
            var path = Path.Combine(project.BasePath, StorageConstants.ProgramLooksPath, look.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.SaveImageAsync(path, newImage, true, ImageFormat.Png);
                look.Image = await storage.LoadImageThumbnailAsync(path);
            }
        }
    }
}