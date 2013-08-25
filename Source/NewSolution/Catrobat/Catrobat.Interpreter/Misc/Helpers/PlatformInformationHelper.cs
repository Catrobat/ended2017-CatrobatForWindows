using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.Interpreter.Misc.Helpers
{
    public class PlatformInformationHelper
    {
        private static ISystemInformationHelper _platformInformationHelper;

        public static void SetInterface(ISystemInformationHelper platformInformationHelper)
        {
            _platformInformationHelper = platformInformationHelper;
        }

        public static string PlatformName
        {
            get { return _platformInformationHelper.GetPlatformName(); }
        }

        public static string PlatformVersion
        {
            get { return _platformInformationHelper.GetPlatformVersion(); }
        }

        public static string DeviceName
        {
            get { return _platformInformationHelper.GetDeviceName(); }
        }

        public static int ScreenWidth
        {
            get { return _platformInformationHelper.GetScreenWidth(); }
        }

        public static int ScreenHeight
        {
            get { return _platformInformationHelper.GetScreenHeight(); }
        }

        public static string CurrentApplicationVersion
        {
            get { return _platformInformationHelper.GetCurrentApplicationVersion(); }
        }
    }
}
