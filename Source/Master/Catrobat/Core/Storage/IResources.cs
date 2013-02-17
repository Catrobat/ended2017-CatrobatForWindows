using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Catrobat.Core.Storage
{
  public enum ResourceScope { Core, IdePhone, IdeStore, TestsPhone, TestsStore, IdeCommon, TestCommon, SampleProjects }

  public interface IResources
  {
    Stream OpenResourceStream(ResourceScope project, string url);
  }
}
