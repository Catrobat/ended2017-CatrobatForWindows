using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Storage;
using ImageTools;
using ImageTools.IO.Png;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.Paint.Phone.Data
{
    public class StorageIo
    {
        private readonly StorageFolder _local = ApplicationData.Current.LocalFolder;
        private const String Foldername = "PocketPaint";
        private StorageFolder _pocketPaintFolder;

        public StorageIo()
        {
            CreateFolder();

        }

        private async void CreateFolder()
        {

            _pocketPaintFolder = await _local.CreateFolderAsync(Foldername,
                CreationCollisionOption.OpenIfExists);
        }

        public async Task<bool> WriteFileIsolaredStorage(byte[] bytes, String filenameWithEnding)
        {

            var file = await _pocketPaintFolder.CreateFileAsync(filenameWithEnding,
            CreationCollisionOption.ReplaceExisting);

            if (file == null)
            {
                return false;
            }

            //todo und im Fehlerfall?
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(bytes, 0, bytes.Length);

                if (s.Length != bytes.Length)
                {
                    return false;
                }

                s.Close();
            }

            return true;
        }

        public async Task<bool> WriteBitmapToPngFileIsolatedStorage(WriteableBitmap bitmap, String filenameWithEnding)
        {

            var file = await _pocketPaintFolder.CreateFileAsync(filenameWithEnding,
            CreationCollisionOption.ReplaceExisting);

            if (file == null)
            {
                return false;
            }


            var img = bitmap.ToImage();
            var encoder = new PngEncoder();
            using (var s = await file.OpenStreamForWriteAsync())
            {
                encoder.Encode(img, s);
                s.Close();
            }



            return true;
        }

        public async Task<bool> WriteBitmapToPngMediaLibrary(WriteableBitmap bitmap, string filenameWithEnding)
        {
            await WriteBitmapToPngFileIsolatedStorage(bitmap, filenameWithEnding);
 
            var library = new MediaLibrary();
            using (var s =  await _pocketPaintFolder.OpenStreamForReadAsync(filenameWithEnding))
            {
                var pic = library.SavePicture(filenameWithEnding, s);
            }

            return true;
        }

        public async Task<WriteableBitmap> ReadPngToBitmapIsolatedStorage(string filenameWithEnding)
        {
 
            using (var s = await _pocketPaintFolder.OpenStreamForReadAsync(filenameWithEnding))
            {
                var decoder = new PngDecoder();
                var img = new ExtendedImage();
                decoder.Decode(img, s);
                return new WriteableBitmap(img.ToBitmap());
            }
        }
    }
}
