using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.TestsCommon.Misc
{
    public class PlatformInformationHelperTests : IPlatformInformationHelper
    {

        public string GetPlatformName()
        {
            return "TestEnvironment";
        }

        public string GetPlatformVersion()
        {
            return "1.0";
        }


        public string GetDeviceName()
        {
            return "TestDevice";
        }

        public int GetScreenWidth()
        {
            return 480;
        }

        public int GetScreenHeight()
        {
            return 800;
        }
    }
}
