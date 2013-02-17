using Catrobat.Core.Misc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catrobat.IDEWindowsPhone.Misc
{
  public class CulturePhone : ICulture
  {
    public string Get2LetterCultureColde()
    {
      return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }
  }
}
