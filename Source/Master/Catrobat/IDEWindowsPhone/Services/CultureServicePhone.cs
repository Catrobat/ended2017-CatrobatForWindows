using System.Threading;
using Catrobat.Core.Services;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class CultureServicePhone : ICultureService
    {
        public string GetToLetterCultureColde()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}