using System.Threading;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class CulturePhone : ICulture
    {
        public string GetToLetterCultureColde()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}