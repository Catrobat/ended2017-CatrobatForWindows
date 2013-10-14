using System.IO;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class CostumeHelper
    {
        public static Costume Save(PortableImage image, string name, ImageDimension dimension, string projectPath)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var imagePath = Path.Combine(projectPath, Project.ImagesPath);
                if (!storage.DirectoryExists(imagePath))
                    storage.CreateDirectory(imagePath);
            }

            var resizedImage = ServiceLocator.ImageResizeService.ResizeImage(image, dimension.Width, dimension.Height);
            var costume = new Costume(name);
            var absoluteFileName = Path.Combine(projectPath, Project.ImagesPath, costume.FileName);
            resizedImage.WriateAsPng(absoluteFileName);
            costume.Image = resizedImage;

            return costume;
        }

        public static void ReplaceImageInStorage(Project project, Costume costume, PortableImage newImage)
        {
            var path = Path.Combine(project.BasePath, Project.ImagesPath, costume.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                storage.SaveImage(path, newImage, true, ImageFormat.Png);
                costume.Image = storage.CreateThumbnail(newImage);
            }
        }
    }
}