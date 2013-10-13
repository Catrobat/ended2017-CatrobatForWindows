using System;
using System.IO;
using Catrobat.IDE.Core.Services.Data;

namespace Catrobat.IDE.Core.Services.Storage
{
    public interface IResourceLoader : IDisposable
    {
        Stream OpenResourceStream(ResourceScope project, string url);

        object LoadImage(ResourceScope resourceScope, string path);
    }
}