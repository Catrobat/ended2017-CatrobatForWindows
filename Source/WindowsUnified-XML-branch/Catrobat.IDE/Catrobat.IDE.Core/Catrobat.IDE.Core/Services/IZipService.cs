using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Core.Services
{
    public interface IZipService
    {
        Task UnzipCatrobatPackageIntoIsolatedStorage(
            Stream zipStream, string localStoragePath);

        Task ZipCatrobatPackage(Stream zipStream, string localStoragePath);
    }
}
