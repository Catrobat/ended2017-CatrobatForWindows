using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Catrobat.Core.Storage
{
  public enum Projects { Core, IdePhone, IdeStore, TestsPhone, TestsStore, IDECommon }

  public interface IResources
  {
    Stream OpenResourceStream(Projects project, string url);
  }
}
