using System.IO;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Costumes;
using Catrobat.Core.Services;
using Catrobat.Core.Services.Data;
using Catrobat.Core.Services.Storage;

namespace Catrobat.Core.Utilities.Helpers
{
    public static class CostumeHelper
    {
        public static Costume Save(PortableImage image, string name, ImageDimention dimention, string projectPath)
        {
            var resizedImage = ServiceLocator.ImageResizeService.ResizeImage(image, dimention.Width, dimention.Height);
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