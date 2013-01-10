using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Catrobat.Core.Storage
{
  public interface IResources
  {
    Stream OpenResourceStream(string url);
  }
}
