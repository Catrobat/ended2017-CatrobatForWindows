using System;
using System.IO;

namespace Catrobat.Core.Storage
{
    public enum ResourceScope
    {
        Core,
        IdePhone,
        IdeStore,
        TestsPhone,
        TestsStore,
        IdeCommon,
        TestCommon,
        Resources
    }

    public interface IResources : IDisposable
    {
        Stream OpenResourceStream(ResourceScope project, string url);
    }
}