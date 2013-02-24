using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Catrobat.Core.Storage
{
  public enum ResourceScope { Core, IdePhone, IdeStore, TestsPhone, TestsStore, IdeCommon, TestCommon, Resources }

  public interface IResources: IDisposable
  {
    Stream OpenResourceStream(ResourceScope project, string url);
  }
}
