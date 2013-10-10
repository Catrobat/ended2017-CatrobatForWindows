using System;
using System.IO;

namespace Catrobat.IDE.Core.Services.Storage
{
    public interface IResourceLoader : IDisposable
    {
        Stream OpenResourceStream(ResourceScope project, string url);
    }
}