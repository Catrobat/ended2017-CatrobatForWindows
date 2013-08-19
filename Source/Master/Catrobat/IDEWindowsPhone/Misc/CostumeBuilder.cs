using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Storage;
using ToolStackPNGWriterLib;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class CostumeBuilder
    {
        private WriteableBitmap _bitmap;
        private Sprite _sprite;

        public void StartCreateCostumeAsync(Sprite sprite, BitmapImage image)
        {
            _bitmap = new WriteableBitmap(image);
            _sprite = sprite;
        }

        public Costume Save(string name, ImageDimention dimention, string projectPath)
        {
            var resizedImage = _bitmap.Resize(dimention.Width, dimention.Height, WriteableBitmapExtensions.Interpolation.Bilinear);

            var costume = new Costume(name);
            var absoluteFileName = Path.Combine(projectPath, Project.ImagesPath, costume.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                var fileStream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write);

                PNGWriter.WritePNG(resizedImage, fileStream, 90);
                fileStream.Close();
            }

            return costume;
        }

        internal void ReplaceImageInStorage(Costume recievedCostume, BitmapImage newImage)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                recievedCostume.Image = newImage;

                // TODO: replace image and create thumbnail
                //storage.SaveImage(recievedCostume.FileName, newImage);
            }
        }
    }
}