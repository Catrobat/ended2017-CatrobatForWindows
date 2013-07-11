using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.Core.Misc.Helpers
{
    public class PlatformInformationHelper
    {
        private static IPlatformInformationHelper _platformInformationHelper;

        public static void SetInterface(IPlatformInformationHelper platformInformationHelper)
        {
            _platformInformationHelper = platformInformationHelper;
        }

        public static string GetPlatformName()
        {
            return _platformInformationHelper.GetPlatformName();
        }

        public static string GetPlatformVersion()
        {
            return _platformInformationHelper.GetPlatformVersion();
        }
    }
}
