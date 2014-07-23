using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class CostumeHelper
    {
        public static async Task<Costume> Save(PortableImage image, string name, ImageDimension dimension, string projectPath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var imagePath = Path.Combine(projectPath, Project.ImagesPath);
                if (!await storage.DirectoryExistsAsync(imagePath))
                    await storage.CreateDirectoryAsync(imagePath);
            }

            var resizedImage = await ServiceLocator.ImageResizeService.ResizeImage(image, dimension.Width, dimension.Height);
            var costume = new Costume(name);
            var absoluteFileName = Path.Combine(projectPath, Project.ImagesPath, costume.FileName);

            await resizedImage.WriteAsPng(absoluteFileName);

            //costume.Image = resizedImage;

            return costume;
        }

        public static async Task ReplaceImageInStorage(Project project, Costume costume, PortableImage newImage)
        {
            var path = Path.Combine(project.BasePath, Project.ImagesPath, costume.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.SaveImageAsync(path, newImage, true, ImageFormat.Png);
                costume.Image = await storage.CreateThumbnailAsync(newImage);
            }
        }
    }
}