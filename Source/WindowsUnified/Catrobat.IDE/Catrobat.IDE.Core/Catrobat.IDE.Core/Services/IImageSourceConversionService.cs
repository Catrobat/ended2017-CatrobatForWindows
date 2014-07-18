using System.IO;

namespace Catrobat.IDE.Core.Services
{
    public interface IImageSourceConversionService
    {
        object ConvertToLocalImageSource(byte[] data, int width, int height);

        void ConvertToBytes(object inputData, out byte[] outputData, out int outputWidth, out int outputHeight);

        object ConvertFromEncodedStream(Stream encodedStream);
    }
}
