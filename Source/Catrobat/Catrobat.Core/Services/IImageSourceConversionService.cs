using System.IO;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface IImageSourceConversionService
    {
        object ConvertToLocalImageSource(byte[] data, int width, int height);

        void ConvertToBytes(object inputData, out byte[] outputData, out int outputWidth, out int outputHeight);

        Task<object> ConvertFromEncodedStream(Stream encodedStream, int width, int height);
    }
}
