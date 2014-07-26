using System;
using System.IO;
using System.Threading.Tasks;
// TODO: using System.Windows.Media.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
// TODO: using ImageTools;
// TODO: using ImageTools.IO.Png;
// TODO: using Microsoft.Xna.Framework.Media;

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

                // TODO: s.Close();
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


            // TODO: var img = bitmap.ToImage();
            // TODO: var encoder = new PngEncoder();
            using (var s = await file.OpenStreamForWriteAsync())
            {
                // TODO: encoder.Encode(img, s);
               // TODO s.Close();
            }



            return true;
        }

        public async Task<bool> WriteBitmapToPngMediaLibrary(WriteableBitmap bitmap, string filenameWithEnding)
        {
            await WriteBitmapToPngFileIsolatedStorage(bitmap, filenameWithEnding);
 
            // TODO: var library = new MediaLibrary();
            using (var s =  await _pocketPaintFolder.OpenStreamForReadAsync(filenameWithEnding))
            {
                // TODO: var pic = library.SavePicture(filenameWithEnding, s);
            }

            return true;
        }

        public async Task<WriteableBitmap> ReadPngToBitmapIsolatedStorage(string filenameWithEnding)
        {
 
            using (var s = await _pocketPaintFolder.OpenStreamForReadAsync(filenameWithEnding))
            {
                // TODO: var decoder = new PngDecoder();
                // TODO: var img = new ExtendedImage();
                // TODO: decoder.Decode(img, s);
                // TODO: return new WriteableBitmap(img.ToBitmap());
                return null;
            }
        }
    }
}
