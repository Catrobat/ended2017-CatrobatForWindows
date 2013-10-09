using System;
using System.IO;

namespace Catrobat.Core.Utilities.Storage
{
    public interface IResourceLoader : IDisposable
    {
        Stream OpenResourceStream(ResourceScope project, string url);
    }
}