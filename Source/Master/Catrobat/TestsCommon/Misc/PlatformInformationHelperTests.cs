using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.TestsCommon.Misc
{
    public class PlatformInformationHelperTests : ISystemInformationService
    {
        public string PlatformName
        {
            get
            {
                return "TestEnvironment";
            }
        }

        public string PlatformVersion
        {
            get
            {
                return "1.0";
            }
        }

        public string DeviceName
        {
            get
            {
                return "TestDevice";
            }
        }

        public int ScreenWidth
        {
            get
            {
                return 480;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return 800;
            }
        }


        public string CurrentApplicationVersion
        {
            get
            {
                return "TestApp01";
            }
        }
    }
}
