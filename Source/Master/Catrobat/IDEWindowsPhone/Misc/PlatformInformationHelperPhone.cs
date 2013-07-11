using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Misc.Helpers;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.Misc
{
    class PlatformInformationHelperPhone : IPlatformInformationHelper
    {
        public string GetPlatformName()
        {
            return Environment.OSVersion.Platform.ToString();
        }

        public string GetPlatformVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }
    }
}
