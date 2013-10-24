using System;
using System.IO;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services.Storage
{
    public interface IResourceLoader : IDisposable
    {
        Stream OpenResourceStream(ResourceScope resourceScope, string uri);

        object LoadImage(ResourceScope resourceScope, string path);


        Task<Stream> OpenResourceStreamAsync(ResourceScope resourceScope, string uri);

        Task<object> LoadImageAsync(ResourceScope resourceScope, string path);
    }
}