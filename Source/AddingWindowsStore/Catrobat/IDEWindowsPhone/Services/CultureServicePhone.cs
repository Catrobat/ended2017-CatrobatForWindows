using System.Threading;
using Catrobat.IDE.Core.Services;

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