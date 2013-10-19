using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Annotations;

namespace Catrobat.Core.Services
{
    public interface IImageSourceConversionService
    {
        object ConvertToLocalImageSource(byte[] data, int width, int height);

        void ConvertToBytes(object inputData, out byte[] outputData, out int outputWidth, out int outputHeight);
    }
}
