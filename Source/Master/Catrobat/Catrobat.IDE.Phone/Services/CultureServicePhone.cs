using System.Threading;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Services
{
    public class CultureServicePhone : ICultureService
    {
        public string GetToLetterCultureColde()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}