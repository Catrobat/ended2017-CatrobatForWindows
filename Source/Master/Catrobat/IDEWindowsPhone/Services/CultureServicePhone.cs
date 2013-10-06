using System.Threading;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Services;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class CulturePhone : ICultureService
    {
        public string GetToLetterCultureColde()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}