using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services.Storage
{
    public interface IResourceLoader : IDisposable
    {
        Stream OpenResourceStream(ResourceScope resourceScope, string uri);

        PortableImage LoadImage(ResourceScope resourceScope, string path);


        Task<Stream> OpenResourceStreamAsync(ResourceScope resourceScope, string uri);

        Task<PortableImage> LoadImageAsync(ResourceScope resourceScope, string path);
    }
}